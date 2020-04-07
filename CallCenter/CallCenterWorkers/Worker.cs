using System;
using System.Threading.Tasks;

namespace CallCenter.CallCenterWorkers
{
    class Worker: IWorker
    {
        public async Task HandleRingAsync(Ring name)
        {
            Console.WriteLine($"{name} - {this}");
            await Delay.HandleClientAsync();

        }

        public int Id { get; set; }
    }
}
