using System.Configuration;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json;
using RestSharp;

namespace NetFramework4_6BFF.Controllers
{
    public class MobileController : ApiController
    {
        private readonly string oldServiceUrl;
        public MobileController()
        {
            this.oldServiceUrl = ConfigurationManager.AppSettings["OldServiceUrl"].ToString();
        }
        
        
        public IHttpActionResult Post([FromBody]object requestObj)
        {
            //object response = CallInternalService(request, "weather");


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

            return Content(HttpStatusCode.BadRequest, "hata");
        }

      
    }
}