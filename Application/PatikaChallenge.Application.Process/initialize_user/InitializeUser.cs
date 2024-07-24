using RestSharp;
using System.Text.Json;
using PatikaChallenge.Application.Domain;
using PatikaChallenge.Application.Domain.Entities;
using System.Configuration;

namespace PatikaChallenge.Application.Process.initialize_user
{
    public class InitializeUser
    {
        string? apiKey = Convert.ToString(ConfigurationManager.AppSettings["API_KEY"]); 
        string? pathIU = Convert.ToString(ConfigurationManager.AppSettings["PATH_INITIAL_USER"]);
        public Root Get(string userToken)
        {
            Root Result = new Root();
            try
            {
                var client = new RestClient(pathIU);
                var request = new RestRequest("/", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + apiKey);
                request.AddHeader("X-User-Token", userToken);
                string idempotencyKey = Guid.NewGuid().ToString();
                var requestBody = new
                {
                    idempotencyKey,
                    blockchains = new string[] { "MATIC-AMOY" }
                };
                request.AddJsonBody(requestBody);
                var response = client.ExecutePost(request);


                Result = JsonSerializer.Deserialize<Root>(response.Content!)!;
            }
            catch (Exception ex) { 
            //log
            }
            return Result;

        }



    }
}
