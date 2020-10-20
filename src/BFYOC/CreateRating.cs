using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;


namespace BFYOC
{

    public static class CreateRating
    {
        
    private static readonly HttpClient HttpClient = new HttpClient();

        [FunctionName("CreateRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "bfyocteam4",
                collectionName: "Ratings",
                CreateIfNotExists = true,
                ConnectionStringSetting = "CosmosDBConnection")]
                IAsyncCollector<RatingOutput> ratingsOut,
            ILogger log)
        {
            log.LogInformation("CreateRating Request Recieved");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Boolean userValid = await ValidateUser(data);
            Boolean productValid = await ValidateProduct(data);

            if(!userValid||!productValid)
            {
                return new NotFoundObjectResult("User or Product Not Found");
            };

            RatingInput input = JsonConvert.DeserializeObject<RatingInput>(requestBody);

            if(input.rating<0||input.rating>5)
            {
                return new BadRequestObjectResult("Rating must be between 0 & 5");
            }

            RatingOutput output = new RatingOutput();
            output.id = System.Guid.NewGuid().ToString();
            output.userId = input.userId;
            output.productId = input.productId;
            output.locationName = input.locationName;
            output.rating = input.rating;
            output.timestamp = DateTime.Now.ToString();
            output.userNotes = input.userNotes;

            //Persist to store
            await ratingsOut.AddAsync(output);
                
            
            return new OkObjectResult(output);
        }

static async Task<Boolean> ValidateUser(dynamic input)
    {
        //We will make a GET request to a really cool website...
       
        string baseUrl = "https://serverlessohuser.trafficmanager.net/api/GetUser?userId=";   
        var response = await HttpClient.GetAsync(baseUrl + input?.userId);
        string data = await response.Content.ReadAsStringAsync();
        
        if(response.StatusCode != HttpStatusCode.OK)
        {
            return false;
        }
        else{
            return true;
        }
    }

    static async Task<Boolean> ValidateProduct(dynamic input)
    {
        //We will make a GET request to a really cool website...
       
        string baseUrl = "https://serverlessohproduct.trafficmanager.net/api/GetProduct?productId=";   
        var response = await HttpClient.GetAsync(baseUrl + input?.productId);
        string data = await response.Content.ReadAsStringAsync();
        
        if(response.StatusCode != HttpStatusCode.OK)
        {
            return false;
        }
        else{
            return true;
        }
    }


    }


}
