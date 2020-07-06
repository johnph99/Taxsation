using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;


namespace Taxsation.Web
{
    public class APIcaller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private static string _apiAddress = "https://localhost:44394/tax";

       

        public APIcaller(IConfiguration configuration ,HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public APIcaller()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Item>> GetTaxTypes()
        {
            var result = _httpClient.GetAsync($"{_apiAddress}/GetTaxTypes").Result;
            
            var retVal = JsonSerializer.Deserialize<List<Item>>
               (result.Content.ReadAsStringAsync().Result);

            return retVal;
        }

        public async Task<List<Item>> GetPostalCodes()
        {
            var result = _httpClient.GetAsync($"{_apiAddress}/GetPostalCodes").Result;
            
            var retVal = JsonSerializer.Deserialize<List<Item>>
               (result.Content.ReadAsStringAsync().Result);

            return retVal;
           
        }

        public async Task<List<Item>> GetPostalCodeTaxTypes()
        {
            var result = _httpClient.GetAsync($"{_apiAddress}/GetPostalCodeTaxTypes").Result;
           
            var retVal = JsonSerializer.Deserialize<List<Item>>
               (result.Content.ReadAsStringAsync().Result);

            return retVal;
        }

        public async Task<decimal> CalculateTax(string zipCode, decimal taxableAmount)
        {
            var result = _httpClient.GetAsync($"{_apiAddress}/CalculateTax/{zipCode}/{taxableAmount}").Result;
            
            var retVal = JsonSerializer.Deserialize<decimal>
               (result.Content.ReadAsStringAsync().Result);

            return retVal;
        }

        internal async Task<List<Requests>> GetPreviousRequests()
        {
            var result = _httpClient.GetAsync($"{_apiAddress}/GetRequests").Result;
            
            var retVal = JsonSerializer.Deserialize<List<Requests>>
               (result.Content.ReadAsStringAsync().Result);

            return retVal;
        }


    }
}
