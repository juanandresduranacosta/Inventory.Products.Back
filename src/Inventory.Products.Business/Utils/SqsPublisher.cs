using Amazon.SQS;
using Inventory.Products.Business.Utils.IUilts;
using Inventory.Products.DataAccess.Models.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Inventory.Products.Business.Utils
{
    public class SqsPublisher : ISqsPublisher
    {
        public AmazonSQSClient clientSqs = new();
        public LogicConfiguration logicConfiguration { get; set; }
        public ILogger<SqsPublisher> _logger { get; set; }
        public SqsPublisher(IOptions<LogicConfiguration> options, ILogger<SqsPublisher> logger)
        {
            _logger = logger;
            logicConfiguration = options.Value;
        }
        public async Task<string> PublishMessage(dynamic obj)
        {
            try
            {
                await clientSqs.SendMessageAsync(logicConfiguration.QueueBulk, JsonConvert.SerializeObject(obj));
                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex + " in file HandlerException and method ExceptionH");
                throw new Exception(ex.Message);
            }
        }
    }
}
