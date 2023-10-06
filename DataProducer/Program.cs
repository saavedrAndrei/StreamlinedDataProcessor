using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using DataProducer.Reader;
using DataProducer.Transformer;
using DataProducer.Publisher;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DataProducer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string folderPath = @"C:\Users\asaavedra\Documents\Software\HealthCareDataset";

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<FileWatcherService>(provider =>
                        new FileWatcherService(folderPath));
                });

            await builder.RunConsoleAsync();
        }
    }
}

