using RestSharp;
using System.Text.Json;
using PatikaChallenge.Application.Domain;
using PatikaChallenge.Application.Domain.Entities;
using System.Configuration;

namespace PatikaChallenge.Application.Process.get_app_id
{
    public class GetAppId
    {

        string? apiKey = Convert.ToString(ConfigurationManager.AppSettings["API_KEY"]);
        string? pathGID = Convert.ToString(ConfigurationManager.AppSettings["PATH_GET_APIID"]);
        public Root Get()
        {
            Root Result = new Root();
            try
            {
                var client = new RestClient(pathGID);
                var request = new RestRequest();
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + apiKey);
                var response = client.ExecuteGet(request);

                Result = JsonSerializer.Deserialize<Root>(response.Content!)!;
            }
            catch (Exception ex)
            {
                //log
            }
            return Result;

        }



    }
}