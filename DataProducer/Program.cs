using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProducer.Reader;

namespace DataProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\asaavedra\Documents\Software\HealthCareDataset";

            FolderReader reader = new FolderReader(folderPath);

            // Keep the application running
            while (true)
            {
                // Do something

                // Wait for a key to be pressed
                Console.ReadKey();
            }

            //reader.InitializeWatcher();

            //var csvTransformer = new CsvToJsonTransformer(); // Create an instance of the transformer
            //var folderReader = new FolderReader(folderPath, csvTransformer); // Pass it to FolderReader

            //// Rest of your code remains the same...
        }

    }
}
