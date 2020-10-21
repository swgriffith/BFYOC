using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "bfyocteam4",
                collectionName: "Ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{Query.ratingId}")] RatingOutput ratingsOut,
            ILogger log)
        {
            log.LogInformation("GetRating Request Recieved");
            log.LogInformation($"DocID: {req.Query["ratingId"]}");

                if (ratingsOut is null)
                {
                    return new NotFoundResult();
                }
                
                return new OkObjectResult(ratingsOut);
        }
    }
}
