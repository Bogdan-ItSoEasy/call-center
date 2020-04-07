using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text.Json;
using CallCenter.CallCenterWorkers;
using Microsoft.Extensions.Configuration;

namespace CallCenter
{
    class CallCenterBuilder
    {
        public CallCenterBuilder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            try
            {
                var config = builder.Build();
                var parameters = config.GetSection("parameters");

                _managersCount = uint.Parse(parameters["managersCount"]);
                _operatorsCount = uint.Parse(parameters["operatorsCount"]);
            }
            catch (FormatException)
            {
                throw new JsonException("Couldn't parse appsettings.json file. Check that this file is correct.");
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Parameters have to be positive");
            }
        }

        private readonly uint _managersCount;
        private readonly uint _operatorsCount;

        public CallCenter Build()
        {
            return new CallCenter
            {
                Managers =  new ConcurrentQueue<Manager>(Enumerable.Range(1, (int)_managersCount).Select(id => new Manager {Id = id})),
                Operators = new ConcurrentQueue<Operator>(Enumerable.Range(1, (int)_operatorsCount).Select(id => new Operator { Id = id })),
                Directors = new ConcurrentQueue<Director>(new[]{ new Director { Id = 1 }})
            };
            
        }
    }
}
