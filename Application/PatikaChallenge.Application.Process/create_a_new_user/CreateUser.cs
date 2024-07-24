using RestSharp;
using System.Text.Json;
using PatikaChallenge.Application.Domain;
using PatikaChallenge.Application.Domain.Entities;
using System.Configuration;

namespace PatikaChallenge.Application.Process.create_a_new_user
{
    public class CreateUser
    {
        string? apiKey = Convert.ToString(ConfigurationManager.AppSettings["API_KEY"]);
        string? pathCU = Convert.ToString(ConfigurationManager.AppSettings["PATH_CREATE_USER"]);
        public Root Create()
        {   Root Result = new Root();
            try
            {
                var client = new RestClient(pathCU);
                var request = new RestRequest("/", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + apiKey);
                string gu_id = Guid.NewGuid().ToString();
                var body = new { userId = gu_id };
                request.AddJsonBody(body);
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
