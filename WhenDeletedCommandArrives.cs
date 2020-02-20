using System.Threading.Tasks;
using Nimbus.InfrastructureContracts.Handlers;
using Serilog;

namespace SendCommand
{
    public class WhenDeletedCommandArrives : IHandleCommand<DeleteCommand>
    {
        public Task Handle(DeleteCommand busCommand)
        {
            Log.Warning(
                "Handling command {CommandName} id {Id} name {Name}", 
                nameof(DeleteCommand), 
                busCommand.Id,
                busCommand.Name);
            return Task.CompletedTask;
        }
    }
}