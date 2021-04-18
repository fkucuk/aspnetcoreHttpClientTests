using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBFF.Services
{
    public interface IOldService
    {
        Task<HttpResponseMessage> CallOldServiceAsync(object requestObj, string endpoint);
        HttpResponseMessage CallOldService(object request, string endpoint);
    }
    public class OldService : IOldService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Uri _uri;
        public OldService(HttpClient client, IConfiguration conf, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _uri = new Uri(conf["LegacyServiceUrl"]);
        }

        public async Task<HttpResponseMessage> CallOldServiceAsync(object requestObj, string endpoint)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = _uri;
            HttpResponseMessage response = new HttpResponseMessage();

            StringContent data = new StringContent(requestObj.ToString(), Encoding.UTF8, "application/json");
            try
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();
                response = await client.PostAsync(@endpoint, data);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadGateway;
                StringBuilder errorMesagge = new StringBuilder();
                errorMesagge.Append("RequestURI:");
                errorMesagge.Append(client.BaseAddress);
                errorMesagge.Append(endpoint);
                response.Content = new StringContent(errorMesagge.ToString());
            }
            return response;
        }

        public HttpResponseMessage CallOldService(object requestObj, string endpoint)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = _uri;
            HttpResponseMessage response = new HttpResponseMessage();

            StringContent data = new StringContent(requestObj.ToString(), Encoding.UTF8, "application/json");
            try
            {

                var watch = System.Diagnostics.Stopwatch.StartNew();
                response = client.PostAsync(@endpoint, data).Result;
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadGateway;
                StringBuilder errorMesagge = new StringBuilder();
                errorMesagge.Append("RequestURI:");
                errorMesagge.Append(client.BaseAddress);
                errorMesagge.Append(endpoint);
                response.Content = new StringContent(errorMesagge.ToString());
            }
            return response;
        }
    }
}
