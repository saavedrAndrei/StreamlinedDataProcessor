using System;
using System.Collections.Generic;
using System.IO;

namespace DataProducer.Reader
{
    public class FolderReader
    {
        private string folderPath;
        private List<string> processedFiles = new List<string>();
        private FileSystemWatcher fileSystemWatcher;

        public FolderReader(string folderPath)
        {
            this.folderPath = folderPath;
            fileSystemWatcher = new FileSystemWatcher(folderPath);

            fileSystemWatcher.Created += (sender, e) =>
            {
                string newFilePath = e.FullPath;

                if (!IsFileProcessed(newFilePath))
                {
                    Console.WriteLine($"New file detected: {newFilePath}");
                    processedFiles.Add(newFilePath);
                }

                // Keep the application running
                Console.WriteLine("Press any key to exit. Andrei");
                Console.ReadKey();
            };

            // Enable the file system watcher
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void InitializeWatcher()
        {
            fileSystemWatcher.Created += (sender, e) =>
            {
                string newFilePath = e.FullPath;

                if (!IsFileProcessed(newFilePath))
                {
                    Console.WriteLine($"New file detected: {newFilePath}");
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
        
    }
}


