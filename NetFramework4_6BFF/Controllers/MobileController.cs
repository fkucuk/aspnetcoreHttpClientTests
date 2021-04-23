using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace NetFrameworkBFF.Controllers
{

    public class MobileController : ApiController
    {

        static HttpClient _client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["OldServiceUrl"].ToString()) };

        private readonly string oldServiceUrl;
        public MobileController()
        {
            this.oldServiceUrl = ConfigurationManager.AppSettings["OldServiceUrl"].ToString();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost]
        [Route("mobile/getlabelinfo")]
        public IHttpActionResult Post([FromBody] object requestObj)
        {
            try
            {
                var client = new RestClient(oldServiceUrl);
                var request = new RestRequest("getlabelinfo", Method.POST);
                request.AddHeader("Content-Type", "application/json");

                string body = JsonConvert.SerializeObject(requestObj);

                request.AddParameter("application / json; charset = utf - 8", body, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    return Content(HttpStatusCode.OK, JsonConvert.DeserializeObject(response.Content.ToString()));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Content(HttpStatusCode.BadRequest, "hata");
        }

        [HttpPost]
        [Route("mobile/v2/getlabelinfo")]
        public async Task<IHttpActionResult> Post_v2([FromBody] object requestObj)
        {
            try
            {
                var client = new RestClient(oldServiceUrl);
                var request = new RestRequest("getlabelinfo", Method.POST);
                request.AddHeader("Content-Type", "application/json");

                string body = JsonConvert.SerializeObject(requestObj);

                request.AddParameter("application / json; charset = utf - 8", body, ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return Content(HttpStatusCode.OK, JsonConvert.DeserializeObject(response.Content.ToString()));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Content(HttpStatusCode.BadRequest, "hata");
        }


        [HttpPost]
        [Route("mobile/v3/getlabelinfo")]
        public async Task<IHttpActionResult> Post_v3([FromBody] object requestObj)
        {
            try
            {


                string body = JsonConvert.SerializeObject(requestObj);


                HttpResponseMessage response = await _client.PostAsJsonAsync("getlabelinfo", requestObj);

                response.EnsureSuccessStatusCode();


                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject(content);

                return Content(HttpStatusCode.OK, result);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Content(HttpStatusCode.NotAcceptable, "deneme");
            }


        }
    }
}