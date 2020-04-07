using System;
using System.Threading.Tasks;

namespace CallCenter.CallCenterWorkers
{
    class Worker: IWorker
    {
        public int Id { get; set; }

        public async Task HandleRingAsync(Ring name)
        {
            Console.WriteLine($"{name} - {this}");
            await Delay.HandleClientAsync();
        }
    }
}
