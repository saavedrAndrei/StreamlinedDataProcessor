using System;
using System.Collections.Generic;
using System.IO;
using CsvProcessor;
using RabbitMQExporter.Transformer;

namespace RabbitMQExporter.Reader
{
    public class FolderReader
    {
        private string folderPath;
        private List<string> processedFiles = new List<string>();
        private FileSystemWatcher fileSystemWatcher;
        private CsvToJsonTransformer csvTransformer;

        public FolderReader(string folderPath, CsvToJsonTransformer transformer)
        {
            this.folderPath = folderPath;
            this.csvTransformer = transformer;

            fileSystemWatcher = new FileSystemWatcher(folderPath);

            fileSystemWatcher.Created += (sender, e) =>
            {
                string newFilePath = e.FullPath;

                if (!IsFileProcessed(newFilePath))
                {
                    Console.WriteLine($"New file detected: {newFilePath}");
                    ProcessFile(newFilePath);
                    processedFiles.Add(newFilePath);
                }

                // Keep the application running
                Console.WriteLine("Press any key to exit. Andrei");
                Console.ReadKey();
            };

            // Enable the file system watcher
            fileSystemWatcher.EnableRaisingEvents = true;
        }
        
        private bool IsFileProcessed(string filePath)
        {
            return processedFiles.Contains(filePath);
        }

        private void ProcessFile(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                string jsonData = csvTransformer.ConvertCsvToJson(filePath); // Use the transformer

                if (jsonData != null)
                {
                    Console.WriteLine($"Converted and published JSON data for file: {filePath}");
                }
            }
            else
            {
                Console.WriteLine($"Skipping non-CSV file: {filePath}");
            }
        }
    }
}


