using MediatR;

namespace JobProcessing.Api.Mediator.Events
{
    public class JobCreatedEvent : INotification
    {
        public string JobId { get; set; }
    }
}
