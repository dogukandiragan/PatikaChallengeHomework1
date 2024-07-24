using RestSharp;
using System.Text.Json;
using PatikaChallenge.Application.Domain;
using PatikaChallenge.Application.Domain.Entities;
using System.Configuration;


namespace PatikaChallenge.Application.Process.acquire_session_token
{
    public class GetUserToken
    {
        string? apiKey = Convert.ToString(ConfigurationManager.AppSettings["API_KEY"]);
        string? pathUT = Convert.ToString(ConfigurationManager.AppSettings["PATH_USER_TOKEN"]);
        
        public Root Get(string userID)
        {              
            Root Result = new Root();
            try
            {
                var client = new RestClient(pathUT);
                var request = new RestRequest("/", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + apiKey);
                var body = new { userId = userID };
                request.AddJsonBody(body);
                var response = client.ExecutePost(request);

                Result = JsonSerializer.Deserialize<Root>(response.Content!)!;
            }
            catch (Exception ex)
            {
                // log
               
            }
            return Result;

        }



    }
}
