using System.Threading.Tasks;
using Nimbus.InfrastructureContracts.Handlers;
using Serilog;

namespace SendCommand
{
    public class WhenCreatedEventArrives : IHandleCompetingEvent<CreatedEvent>
    {
        public Task Handle(CreatedEvent busEvent)
        {
            Log.Warning(
                "Handling command {EventName} id {Id} name {Name}", 
                nameof(CreatedEvent), 
                busEvent.Id,
                busEvent.Name);
            return Task.CompletedTask;
        }
    }
}