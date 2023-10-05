﻿using System;
using System.Collections.Generic;
using System.IO;

namespace DataProducer.Reader
{
    public delegate void NewFileDetectedEventHandler(object sender, string filePath);

    public class FolderReader
    {
        private string folderPath;
        private FileSystemWatcher fileSystemWatcher;
        private List<string> processedFiles = new List<string>();
        
        // Event to notify when a new file is detected
        public event NewFileDetectedEventHandler NewFileDetected;

        public FolderReader(string folderPath)
        {
            this.folderPath = folderPath;

            // Create a FileSystemWatcher to monitor the folder
            fileSystemWatcher = new FileSystemWatcher(folderPath);
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            string newFullPath = e.FullPath;

            if (!IsFileProcessed(newFullPath))
            {
                // Process the newly created file here
                Console.WriteLine($"New file detected: {e.FullPath}");
                processedFiles.Add(newFullPath);

                // Raise the event to notify the main program
                NewFileDetected?.Invoke(this, newFullPath);
            }
        }

        private bool IsFileProcessed(string path)
        {
            return processedFiles.Contains(path);
        }

    }
}



