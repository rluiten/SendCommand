using System;
using Nimbus.MessageContracts;

namespace SendCommand
{
    public class CreatedEvent : IBusEvent
    {
        public CreatedEvent()
        {
        }

        public CreatedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}