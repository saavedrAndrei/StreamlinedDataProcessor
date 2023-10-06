using DataProducer.Publisher;
using DataProducer.Reader;
using DataProducer.Transformer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProducer
{
    public class FileWatcherService : BackgroundService
    {
        private readonly string folderPath;

        public FileWatcherService(string folderPath)
        {
            this.folderPath = folderPath;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (FileReader reader = new FileReader(folderPath))
            {
                // Subscribe to the NewFileDetected event
                reader.NewFileDetected += Reader_NewFileDetected;

                while (!stoppingToken.IsCancellationRequested)
                {
                    // Keep the service running until it's canceled
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
        }

        private void Reader_NewFileDetected(object sender, string filePath)
        {
            // Process the detected file here
            Console.WriteLine($"New file detected: {filePath}");

            List<string> jsonFiles = new List<string>();
            CsvToJsonTransformer transformer = new CsvToJsonTransformer();
            string jsonFile = transformer.ConvertCsvToJson(filePath);
            jsonFiles.Add(jsonFile);

            DataPublisher publisher = new DataPublisher();
            publisher.DataSender(jsonFiles);
        }
    }
}
