using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using DataProducer.Reader;
using DataProducer.Transformer;

namespace DataProducer
{
    class Program
    {
        // Create a list to store the detected file paths
        private static List<string> detectedFiles = new List<string>();

        static void Main(string[] args)
        {
            try
            {
                string folderPath = @"C:\Users\asaavedra\Documents\Software\HealthCareDataset";

                using (FileReader reader = new FileReader(folderPath))
                {
                    // Subscribe to the NewFileDetected event
                    reader.NewFileDetected += Reader_NewFileDetected;

                    // Keep the program running
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);  
            }

            List<string> jsonFiles = new List<string>();
            CsvToJsonTransformer transformer = new CsvToJsonTransformer();
            foreach (var file in detectedFiles)
            {
                string jsonFile= transformer.ConvertCsvToJson(file);
                jsonFiles.Add(jsonFile);
            }


        }

        // Event handler for when a new file is detected
        private static void Reader_NewFileDetected(object sender, string filePath)
        {
            // Add the detected file path to the list
            detectedFiles.Add(filePath);
        }
    }
}

