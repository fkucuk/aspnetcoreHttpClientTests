using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaticMobileController : ControllerBase
    {
        private static readonly HttpClient httpClient;

        static StaticMobileController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new System.Uri("http://nagras-local.lcwaikiki.com/TemaMobileServices/JsonRestDataService.svc/");
        }

        [HttpPost, Route("getlabelinfo")]
        public async Task<IActionResult> GetProductAsync(object requestObject)
        {

            HttpResponseMessage response = null;
            var data = JsonConvert.SerializeObject(requestObject);

            StringContent jsonObject = new StringContent(data, Encoding.UTF8, "application/json");
            try
            {

                response = await httpClient.PostAsync("getlabelinfo", jsonObject);

            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.BadGateway;
                StringBuilder errorMesagge = new StringBuilder();
                errorMesagge.Append("RequestURI:");
                errorMesagge.Append(httpClient.BaseAddress);
                errorMesagge.Append("getlabelinfo");
                response.Content = new StringContent(errorMesagge.ToString());
            }


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content);
                return Ok(result);
            }


            return StatusCode(501);

        }
    }
}
