using Amazon.SQS;
using Amazon.SQS.Model;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.DataAccess.Models.Configurations;
using Inventory.Products.DataAccess.Models.Entites;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Inventory.Products.Api.Worker
{
    public class ProductWorker : BackgroundService
    {
        private readonly IProductServices _productServices;
        public AmazonSQSClient clientSqs = new();
        public LogicConfiguration logicConfiguration { get; set; }
        public ProductWorker(IProductServices productServices, IOptions<LogicConfiguration> options)
        {
            _productServices = productServices;
            logicConfiguration = options.Value;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTime startProcess;
            DateTime endProcess;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var receiveMessageRequest = new ReceiveMessageRequest
                    {
                        QueueUrl = logicConfiguration.QueueBulk,
                        MaxNumberOfMessages = 10,
                    };

                    // Realiza la llamada para recibir mensajes
                    var receiveMessageResponse = await clientSqs.ReceiveMessageAsync(receiveMessageRequest);
                    startProcess = DateTime.Now;
                    // Procesa los mensajes recibidos
                    if (receiveMessageResponse.Messages.Any())
                    {
                        foreach (var message in receiveMessageResponse.Messages)
                        {
                            Console.WriteLine($"Mensaje recibido: {message.Body}");

                            try
                            {
                                var messageP = JsonConvert.DeserializeObject<ProductEntity>(message.Body);

                                await _productServices.CreateProduct(messageP);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                            await clientSqs.DeleteMessageAsync(logicConfiguration.QueueBulk, message.ReceiptHandle);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No hay mensajes: {DateTime.Now}");
                        await Task.Delay(10000, stoppingToken);
                    }

                    // Control de tps
                    endProcess = DateTime.Now;
                    var diff = endProcess - startProcess;
                    var delay = 1000 - diff.Milliseconds;
                    if (delay > 0)
                    {
                        await Task.Delay(delay, stoppingToken);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
