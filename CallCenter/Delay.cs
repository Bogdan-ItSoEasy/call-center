using System;
using System.Threading;
using System.Threading.Tasks;

namespace CallCenter
{
    static class Delay
    {
        static Delay()
        {
            _random = new Random(255);
        }

        private static Random _random;

        public static async Task HandleClientAsync()
        {
            var random = new Random().Next() % 1000;
            await Task.Delay(1000 + random );
        }

        public static void AwaitClient()
        {
            var random = new Random().Next() % 500;
            Thread.Sleep(random);
        }
    }
}
