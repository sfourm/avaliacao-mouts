using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
