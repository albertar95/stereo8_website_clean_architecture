using Application.Email.Contract;
using Application.MessageBroker.Bus;
using Application.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Bus.Handler
{
    public class UserCreatedHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IMailActions _mailActions;

        public UserCreatedHandler(IMailActions mailActions)
        {
            _mailActions = mailActions;
        }
        public Task Handle(UserCreatedEvent @event)
        {
            _mailActions.VerificationMail(@event.UserId, @event.Username);
            return Task.CompletedTask;
        }
    }
}
