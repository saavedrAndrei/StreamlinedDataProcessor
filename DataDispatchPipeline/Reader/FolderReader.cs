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

            // Initialize the file system watcher
            fileSystemWatcher = new FileSystemWatcher(folderPath);

            // Set up event handlers for file system events
            fileSystemWatcher.Created += (sender, e) =>
            {
                string newFilePath = e.FullPath;

                if (!IsFileProcessed(newFilePath))
                {
                    Console.WriteLine($"New file detected: {newFilePath}");
                    ProcessFile(newFilePath);
                    processedFiles.Add(newFilePath); // Track the processed file
                }
            };

            // Enable the file system watcher
            fileSystemWatcher.EnableRaisingEvents = true;

            // Keep the application running
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private bool IsFileProcessed(string filePath)
        {
            return processedFiles.Contains(filePath);
        }

        private void ProcessFile(string filePath)
        {
            // Check if the file is a CSV file
            if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                string jsonData = csvTransformer.ConvertCsvToJson(filePath); // Use the transformer

                if (jsonData != null)
                {
                    // Publish jsonData to RabbitMQ
                    // Add your RabbitMQ publishing logic here
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


