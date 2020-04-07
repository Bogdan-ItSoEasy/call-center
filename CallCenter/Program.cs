using System;
using System.Threading;

namespace CallCenter
{
    class Program
    {
        static void Main()
        {
            var builder = new CallCenterBuilder();
            var callCenter = builder.Build();

            Thread thread = new Thread(() => CreateRings(callCenter));
            thread.Start();
            
            callCenter.Handle();
        }

        private static void CreateRings(CallCenter callCenter)
        {
            ulong clientId = 0;
            
            while (true)
            {
                Delay.AwaitClient();
                callCenter.AddRingInOrder(new Ring(++clientId));
            } 
        }
    }
}

