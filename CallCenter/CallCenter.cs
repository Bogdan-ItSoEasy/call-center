using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using CallCenter.CallCenterWorkers;

namespace CallCenter
{
    class CallCenter
    {
        public ConcurrentQueue<Operator> Operators { get; set; } = new ConcurrentQueue<Operator>();
        public ConcurrentQueue<Manager> Managers{ get; set; } = new ConcurrentQueue<Manager>();
        public ConcurrentQueue<Director> Directors { get; set; } = new ConcurrentQueue<Director>();

        private readonly Order _order = new Order();
        private readonly ConcurrentQueue<RingInOrder> _ringsInOrder = new ConcurrentQueue<RingInOrder>();

        public void AddRingInOrder(Ring ring)
        {
            var order = Interlocked.Increment(ref _order.OrderLength);
            var wait = new RingInOrder(ring, order, _order);
            wait.StartWaiting();
            _ringsInOrder.Enqueue(wait);
        }

        public bool TryGetFreeWorker(out IWorker worker)
        {
            if (Operators.TryDequeue(out Operator @operator))
            {
                worker = @operator;
                return true;
            }

            if (Managers.TryDequeue(out Manager manager))
            {
                worker = manager;
                return true;
            }

            if (Directors.TryDequeue(out Director director))
            {
                worker = director;
                return true;
            }

            worker = null;
            return false;
        }

        public void FreeWorker(IWorker worker)
        {
            switch (worker)
            {
                case Operator @operator:
                    Operators.Enqueue(@operator);
                    break;
                case Manager manager:
                    Managers.Enqueue(manager);
                    break;
                case Director director:
                    Directors.Enqueue(director);
                    break;
                default:
                    throw new ArgumentException("Invalid type of worker");
            }
        }

        public void Handle()
        {
            while (true)
            {
                if (_ringsInOrder.Count != 0 && TryGetFreeWorker(out IWorker worker))
                {
                    if(!_ringsInOrder.TryDequeue(out RingInOrder ring)){
                        throw new InvalidOperationException("Couldn't dequeue Ring from queue while queue isn't empty");
                    }

                    Interlocked.Increment(ref _order.OrderHandled);
                    ring.StopWaiting();
                    worker.HandleRingAsync(ring.Ring).ContinueWith((t) => { FreeWorker(worker);}, 
                        TaskScheduler.Default).ConfigureAwait(true);
                }
            }
        }
    }
}
