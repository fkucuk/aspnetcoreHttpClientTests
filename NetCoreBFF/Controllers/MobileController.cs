using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreBFF.Services;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace NetCoreBFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MobileController : ControllerBase
    {
        private readonly IOldService _oldService;
        private readonly IConfiguration _configuration;

        public MobileController(IOldService oldService, IConfiguration configuration)
        {
            _oldService = oldService;
            _configuration = configuration;
        }
        [HttpPost, Route("weather")]
        public async Task<IActionResult> GetWeather(object request)
        {
            var response = await _oldService.CallOldServiceAsync(request, "weather");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode);
        }


        [HttpPost, Route("v2/weather")]
        public  IActionResult GetWeather_v2(object request)
        {
            WebClient client = new WebClient();

            client.Headers.Add("Content-Type", "application/json");

            BinaryFormatter bf = new BinaryFormatter();
            var postData = JsonConvert.SerializeObject(request);

            var response = client.UploadString($"{_configuration["LegacyServiceUrl"]}/weather", postData);

            

            return Ok(response);
        }


        [HttpPost, Route("v3/weather")]
        public IActionResult GetWeather_v3(object requestObj)
        {
            var client = new RestClient(_configuration["LegacyServiceUrl"]);
            var request = new RestRequest("weather", Method.POST);
            request.AddHeader("Content-Type", "application/json");

            string body = JsonConvert.SerializeObject(requestObj);

            request.AddParameter("application / json; charset = utf - 8", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            var unserializedContent = JsonConvert.DeserializeObject(response.Content.ToString());
            return Ok(unserializedContent);
        }

        [HttpPost, Route("v4/weather")]
        public IActionResult GetWeather_v4(object request)
        {
            var response = _oldService.CallOldService(request, "weather");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(content);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpPost, Route("v5/weather")]
        public IActionResult GetWeather_v5(object request)
        {
            var postData = JsonConvert.SerializeObject(request);
            string response = GetDataFromWebClient($"{_configuration["LegacyServiceUrl"]}/weather", postData);

            return Ok(response);
        }

        public static string GetDataFromWebClient(string url ,string data)
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Content-Type", "application/json");
                webClient.BaseAddress = url;
                return webClient.UploadString(url, data);
            }
        }
    }
}
