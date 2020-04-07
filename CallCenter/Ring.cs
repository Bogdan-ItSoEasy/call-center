using System;

namespace CallCenter
{
    class Ring
    {
        public Ring(ulong id)
        {
            Id = id;
        }

        public ulong Id { get; set; }

        public override string ToString()
        {
            return $"Звонок {Id}";
        }
    }
}
