using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileWCFService;
using NetCoreBFF.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;

namespace NetCoreBFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MobileController : ControllerBase
    {
        private readonly ICoreService _coreService;
        private readonly IOldService _oldService;
        private readonly IConfiguration _configuration;

        

        public MobileController(IOldService oldService,ICoreService coreService, IConfiguration configuration)
        {
            _coreService = coreService;
            _oldService = oldService;
            _configuration = configuration;
        }
        [HttpPost, Route("getlabelinfo")]
        public async Task<IActionResult> GetWeather(object request)
        {
            var response = await _oldService.CallOldServiceAsync(request, "getlabelinfo");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode);
        }

        [HttpPost, Route("v8/getlabelinfo")]
        public async Task<IActionResult> GetWeather_v8(object request)
        {
            using (HttpResponseMessage response = await _oldService.CallOldServiceAsync(request, "getlabelinfo"))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    if (response.Content is object)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject(content);
                        return Ok(result);
                    }

                    return BadRequest();
                }
                catch
                {

                    return StatusCode(501);
                }
            }

        }


        [HttpPost, Route("v2/getlabelinfo")]
        public IActionResult GetWeather_v2(object request)
        {
            using var client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            var postData = JsonConvert.SerializeObject(request);
            var response = client.UploadString($"{_configuration["LegacyServiceUrl"]}/getlabelinfo", postData);

            return Ok(response);
        }


        [HttpPost, Route("v3/getlabelinfo")]
        public IActionResult GetWeather_v3(object requestObj)
        {
            var client = new RestClient(_configuration["LegacyServiceUrl"]);
            var request = new RestRequest("getlabelinfo", Method.POST);
            request.AddHeader("Content-Type", "application/json");

            string body = JsonConvert.SerializeObject(requestObj);

            request.AddParameter("application / json; charset = utf - 8", body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);


            if (response.IsSuccessful)
            {
                var unserializedContent = JsonConvert.DeserializeObject(response.Content.ToString());
                return Ok(unserializedContent);
            }

            return StatusCode((int)response.StatusCode, "The server encountered an error processing the request.");

        }

        [HttpPost, Route("v6/getlabelinfo")]
        public IActionResult GetWeather_v6(GetLabelInfoRequest request)
        {
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            var address = new EndpointAddress("http://nagras-local.lcwaikiki.com/TemaMobileServices/JsonRestDataService.svc/wcf");

            //var factory = new ChannelFactory<IJsonRestDataService>(basicHttpBinding, address);
            //var channel = factory.CreateChannel();
            ////var channel = new ChannelFactory<MobileWCFService.IJsonRestDataService>(new BasicHttpBinding(), new EndpointAddress("http://nagras-local.lcwaikiki.com/TemaMobileServices/JsonRestDataService.svc/wcf")).CreateChannel();


            MobileWCFService.JsonRestDataServiceClient client = new JsonRestDataServiceClient(basicHttpBinding, address);

            try
            {
                var result = client.GetLabelInfoAsync(request);
                return Ok(result);
            }
            catch(Exception e) { 
                return BadRequest(); 
            }

        }



        [HttpPost, Route("coreclient")]
        public async Task<IActionResult> coreclient(object requestObj, string endpoint)
        {
            var response = await _coreService.CallOldServiceAsync(requestObj, "getlabelinfo");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode);

        }

    }
}
