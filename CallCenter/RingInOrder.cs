using System;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter
{
    class RingInOrder
    {
        public RingInOrder(Ring ring, int orderNumber, Order order)
        {
            Ring = ring;
            Order = order;
            OrderNumber = orderNumber;
        }
        
        public int OrderNumber { get; }
        public Ring Ring { get; }

        private Order _order;
        private CancellationTokenSource _ts;

        public void StartWaiting()
        {
            _ts = new CancellationTokenSource();
            CancellationToken ct = _ts.Token;
            Task.Run(async ()=>
            {
                int sec = 0;
                while (true)
                {
                    await Task.Delay(1000, ct);
                    if (ct.IsCancellationRequested)
                        break;
                    Console.WriteLine($"{Ring} в ожидании ({++sec} секунда, {OrderNumber - Order.OrderHandled}й в очереди)");
                }
            }, ct);
        }

        public void StopWaiting()
        {
            _ts?.Cancel();
        }
    }
}
