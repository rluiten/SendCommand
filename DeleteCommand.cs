using System;
using Nimbus.MessageContracts;

namespace SendCommand
{
    public class DeleteCommand: IBusCommand
    {
        public DeleteCommand()
        {
        }

        public DeleteCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}