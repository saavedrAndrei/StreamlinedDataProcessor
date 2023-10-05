using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DataProducer.Reader
{
    public class ServiceReader 
    {
        public async Task ListenerFileAsync(string filePath)
        {
            // Implement your file processing logic here
            // You can use async methods for file processing operations

            await Task.Delay(TimeSpan.FromSeconds(5)); // Simulating some asynchronous processing
            Console.WriteLine($"Processed file: {filePath}");
        }
    }
}
