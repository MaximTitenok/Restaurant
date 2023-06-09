﻿using System;
using Restaurant;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant.Models {
    public static class Files {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            TextWriter writer = null;
            try {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally {
                if (writer != null) { writer.Close(); }
            }
        }

        public static T ReadFromJsonFile<T>(string filePath) where T : new() {
            TextReader reader = null;
            try {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally {
                if (reader != null) { reader.Close(); }
            }
        }
    }
}
