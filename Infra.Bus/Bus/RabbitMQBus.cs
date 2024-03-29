﻿using Application.Email.Contract;
using Application.MessageBroker.Bus;
using Application.MessageBroker.Events;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Infra.Bus.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IMailActions _mailActions;

        public RabbitMQBus(IMailActions mailActions)
        {
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _mailActions = mailActions;
        }
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var eventName = @event.GetType().Name;
                    channel.QueueDeclare(eventName, false, false, false);
                    var content = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(content);
                    channel.BasicPublish("", eventName, null, body);
                }
            }
        }
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);
            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));
            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());
            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"HAndler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
            }
            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }
        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(eventName, true, consumer);
        }
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var eventName = @event.RoutingKey;
            var message = Encoding.UTF8.GetString(@event.Body.ToArray());
            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];
                foreach (var item in subscriptions)
                {
                    var handler = Activator.CreateInstance(item,_mailActions);
                    if (handler == null) continue;
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
