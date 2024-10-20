using BankSystem.Domain.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ExportTool
{
    public class ExportService
    {
        public void ExportClientsToCsv(IEnumerable<Client> clients, string filePath)
        {
            if (clients == null)
                throw new Exception("The clients collection cannot be null.");

            using (var streamWriter = new StreamWriter(filePath))
            {
                using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<Client>();
                    csv.NextRecord();

                    foreach (var client in clients)
                    {
                        csv.WriteRecord(client);
                        csv.NextRecord();
                    }
                }
            }
        }

        public IEnumerable<Client> ImportClientsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception("The specified file was not found.");

            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        var clients = csvReader.GetRecords<Client>().ToList();
                        return clients;
                    }
                }
            }
            catch (CsvHelper.TypeConversion.TypeConverterException ex)
            {
                throw new Exception($"Ошибка формата данных в CSV-файле: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Произошла ошибка при импорте клиентов из CSV.", ex);
            }
        }
    }
}
