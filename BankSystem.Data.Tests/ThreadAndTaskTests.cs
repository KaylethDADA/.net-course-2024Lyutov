using BankSystem.Application.Services;
using BankSystem.Domain.Models;
using ExportTool;
using System.Collections.Concurrent;
using System.Text.Json;

namespace BankSystem.Data.Tests
{
    public class ThreadAndTaskTests
    {
        private const int _maxFileSize = 1024 * 15;
        private const string _filePrefix = "ClientsFile_";
        private readonly string _testDirectory = "TestThreadJson";
        private readonly object _fileLock = new();
        private int _fileCounter = 0;
        private readonly SemaphoreSlim _semaphore = new(3);
        private readonly TestDataGenerator _testDataGenerator;
        private readonly ExportService<Client> _exportService;
        private readonly ConcurrentQueue<Client> _clientQueue = new();

        public ThreadAndTaskTests()
        {
            _testDataGenerator = new TestDataGenerator();
            _exportService = new ExportService<Client>();

            if (!Directory.Exists(_testDirectory))
                Directory.CreateDirectory(_testDirectory);
        }

        [Fact]
        public void PipelineProcessingTest()
        {
            // Arrange
            var clients = _testDataGenerator.GenerateClients(100);
            foreach (var client in clients)
            {
                _clientQueue.Enqueue(client);
            }

            // Act
            var threads = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                var thread = new Thread(ProcessClients);
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // Assert
            var files = Directory.GetFiles(_testDirectory, $"{_filePrefix}*.json");
            Assert.NotEmpty(files);
            
            var listId = new HashSet<Guid>();
            foreach (var file in files)
            {
                var clientsFromFile = _exportService.ImportEntitiesFromJson(file);
                Assert.NotNull(clientsFromFile);

                foreach (var client in clientsFromFile)
                {
                    listId.Add(client.Id);
                }
            }

            Assert.Equal(clients.Select(x => x.Id).Count(), listId.Count);
            Assert.True(clients.Select(x => x.Id).All(x => listId.Contains(x)));
        }

        private void ProcessClients()
        {
            while (_clientQueue.TryDequeue(out var client))
            {
                _semaphore.Wait();
                try
                {
                    WriteClientToFile(client);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }

        private void WriteClientToFile(Client client)
        {
            var filePath = Path.Combine(_testDirectory, $"{_filePrefix}{_fileCounter}.json");
            long currentFileSize = File.Exists(filePath) ? new FileInfo(filePath).Length : 0;

            if (currentFileSize >= _maxFileSize)
            {
                _fileCounter++;
                filePath = Path.Combine(_testDirectory, $"{_filePrefix}{_fileCounter}.json");
            }

            AppendClientToJsonArray(client, filePath);
        }

        private void AppendClientToJsonArray(Client client, string filePath)
        {
            lock (_fileLock)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var clientJson = JsonSerializer.Serialize(client, options);

                if (!File.Exists(filePath))
                {
                    using (var streamWriter = new StreamWriter(filePath, append: false))
                    {
                        streamWriter.WriteLine("[" + clientJson + "]");
                    }
                }
                else
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        using (var streamReader = new StreamReader(fileStream))
                        using (var streamWriter = new StreamWriter(fileStream))
                        {
                            fileStream.Seek(-1, SeekOrigin.End);

                            while (fileStream.Position > 0)
                            {
                                fileStream.Seek(-1, SeekOrigin.Current);
                                if (streamReader.Read() == ']')
                                {
                                    fileStream.Seek(-1, SeekOrigin.Current);
                                    break;
                                }
                                fileStream.Seek(-1, SeekOrigin.Current);
                            }

                            if (fileStream.Position > 1)
                            {
                                streamWriter.Write(",");
                            }

                            streamWriter.WriteLine();
                            streamWriter.Write(clientJson);
                            streamWriter.WriteLine();
                            streamWriter.Write("]");
                        }
                    }
                }   
            }
        }

        [Fact]
        public void ParallelDepositTest()
        {
            // Arrange
            var account = _testDataGenerator.GenerateAccounts(1, _testDataGenerator.GenerateCurrencies(1)).First();
            account.Amount = 0;
            decimal amountToDeposit = 100m;
            int depositCount = 10;

            // Act
            var thread1 = new Thread(() => DepositMoney(account, amountToDeposit, depositCount));
            var thread2 = new Thread(() => DepositMoney(account, amountToDeposit, depositCount));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            // Assert
            decimal expectedAmount = amountToDeposit * depositCount * 2;
            Assert.Equal(expectedAmount, account.Amount);
        }

        private void DepositMoney(Account account, decimal amount, int count)
        {
            for (int i = 0; i < count; i++)
            {
                lock (_fileLock)
                {
                    account.Amount += amount;
                }
            }
        }
    }
}
