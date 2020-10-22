using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace BFYOC
{
    public static class ProcessSales
    {
        [FunctionName("ProcessSales")]
        public static async Task Run([EventHubTrigger("salesteam4", Connection = "POShub")] EventData[] events, 
        [CosmosDB(
            databaseName: "bfyocteam4",
            collectionName: "Sales",
            CreateIfNotExists = true,
            ConnectionStringSetting = "CosmosDBConnection")]
            IAsyncCollector<String> salesOut,
        ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    await salesOut.AddAsync(messageBody);
                    //await Task.Yield();
                }
                catch (Exception e)
                {
                    //log.LogInformation(e.InnerException.ToString());
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    throw e;
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

        }
    }
}
