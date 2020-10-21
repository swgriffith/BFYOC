using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BFYOC
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "bfyocteam4",
                collectionName: "Ratings",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT * FROM c order by c._ts desc")]
                IEnumerable<RatingOutput> ratings,
            ILogger log)
        {
            log.LogInformation("GetRatings Request Recieved");

            if(ratings==null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(ratings);
        }
    }

}
