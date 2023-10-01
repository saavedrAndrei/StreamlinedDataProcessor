using RabbitMQExporter.Transformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQExporter.Reader;

namespace RabbitMQExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\asaavedra\Documents\Software\HealthCareDataset";

            var csvTransformer = new CsvToJsonTransformer(); // Create an instance of the transformer
            var folderReader = new FolderReader(folderPath, csvTransformer); // Pass it to FolderReader

            // Rest of your code remains the same...
        }

    }
}
