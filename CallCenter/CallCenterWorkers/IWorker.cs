using System.Threading.Tasks;

namespace CallCenter.CallCenterWorkers
{
    interface IWorker
    { 
        int Id { get; set; }

        Task HandleRingAsync(Ring name);
    }
}
