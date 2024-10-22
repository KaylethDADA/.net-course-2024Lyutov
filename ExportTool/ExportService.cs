using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text.Json;

namespace ExportTool
{
    public class ExportService<T> 
        where T : class
    {
        public void ExportEntitiesToCsv(IEnumerable<T> entities, string filePath)
        {
            if (entities == null)
                throw new Exception("The collection cannot be null.");

            using (var streamWriter = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<T>();
                    csv.NextRecord();

                    foreach (var entity in entities)
                    {
                        csv.WriteRecord(entity);
                        csv.NextRecord();
                    }
                }
            }
        }

        public ICollection<T> ImportEntitiesFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("The specified file was not found.");

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        var entities = csvReader.GetRecords<T>().ToList();
                        return entities;
                    }
                }
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                throw new Exception($"Data format error in the CSV file: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while importing entities from CSV.", ex);
            }
        }

        public void ExportEntitiesToJson(IEnumerable<T> entities, string filePath)
        {
            if (entities == null)
                throw new Exception("The collection cannot be null.");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(entities, options);

            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(json);
            }
        }

        public ICollection<T> ImportEntitiesFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("The specified file was not found.");

            using (var streamReader = new StreamReader(filePath))
            {
                var json = streamReader.ReadToEnd();
                var entities = JsonSerializer.Deserialize<ICollection<T>>(json);

                return entities;
            }
        }

        public void ExportEntityToJson(T entity, string filePath)
        {
            if (entity == null)
                throw new Exception("The entity cannot be null.");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(entity, options);

            using (var streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(json);
            }
        }

        public T ImportEntityFromJson(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("The specified file was not found.");

            using (var streamReader = new StreamReader(filePath))
            {
                var json = streamReader.ReadToEnd();

                try
                {
                    var entity = JsonSerializer.Deserialize<T>(json);
                    if (entity != null)
                        return entity;
                }
                catch(JsonException)
                {
                    var entities = JsonSerializer.Deserialize<IEnumerable<T>>(json);
                    if (entities != null && entities.Any())
                        return entities.First();
                }
                catch(Exception ex)
                {
                    throw new Exception("An error occurred while importing entities from JSON.", ex);
                }
            }

            throw new Exception("Deserialization returned null or the file is empty.");
        }
    }
}
