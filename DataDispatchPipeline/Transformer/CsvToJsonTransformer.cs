using System;
using CsvHelper.Configuration;
using CsvHelper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RabbitMQExporter.Transformer
{
    public class CsvToJsonTransformer
    {
        public string ConvertCsvToJson(string csvFilePath)
        {
            try
            {
                using (var reader = new StreamReader(csvFilePath, Encoding.Default))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<dynamic>();
                    return JsonConvert.SerializeObject(records);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while converting CSV to JSON: {ex.Message}");
                return null;
            }
        }
    }
}
