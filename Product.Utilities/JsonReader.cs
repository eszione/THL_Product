﻿using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Product.Utilities
{
    public static class JsonReader
    {
        public async static Task<T> GetFromFile<T>(string folderName, string fileName)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var directoryPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

                using var stream = File.OpenRead(Path.Combine(directoryPath, folderName, fileName));

                return await JsonSerializer.DeserializeAsync<T>(stream, options);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
