using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WinHttpHandlerController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Uri _uri;
        public WinHttpHandlerController(IHttpClientFactory clientFactory, IConfiguration conf)
        {
            _clientFactory = clientFactory;
            _uri = new Uri(conf["LegacyServiceUrl"]);
        }
        [HttpPost, Route("getlabelinfo")]
        public async Task<IActionResult> CallOldServiceAsync(object requestObj, string endpoint)
        {
            var httpClient = _clientFactory.CreateClient("WinHttp");
            httpClient.BaseAddress = _uri;
            HttpResponseMessage response = null;

            StringContent data = new StringContent(requestObj.ToString(), Encoding.UTF8, "application/json");

            response = await httpClient.PostAsync("getlabelinfo", data);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(content);
                response.Dispose();
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode);

        }

    }
}
