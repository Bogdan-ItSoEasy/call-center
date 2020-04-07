using System;

namespace CallCenter
{
    class Ring
    {
        public Ring(UInt64 id)
        {
            Id = id;
        }

        public UInt64 Id { get; set; }

        public override string ToString()
        {
            return $"Звонок {Id}";
        }
    }
}
