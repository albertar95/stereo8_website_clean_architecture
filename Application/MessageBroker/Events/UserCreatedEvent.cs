using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MessageBroker.Events
{
    public class UserCreatedEvent : Event
    {
        public UserCreatedEvent(Guid userId, string username)
        {
            UserId = userId;
            Username = username;
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
