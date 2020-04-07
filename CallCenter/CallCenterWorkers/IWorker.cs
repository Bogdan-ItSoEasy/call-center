using System.Threading.Tasks;

namespace CallCenter.CallCenterWorkers
{
    interface IWorker
    {
        Task HandleRingAsync(Ring name);

        int Id { get; set; }
    }
}
