using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBFF.Services
{
    public interface ICoreService
    {
        Task<HttpResponseMessage> CallOldServiceAsync(object requestObj, string endpoint);
    }
    public class CoreService : ICoreService
    {

        private readonly Uri _uri;
        private readonly HttpClient _client;
        public CoreService(HttpClient client, IConfiguration conf)
        {
            _client = client;
            _uri = new Uri(conf["CoreServiceUrl"]);
        }

        public async Task<HttpResponseMessage> CallOldServiceAsync(object requestObj, string endpoint)
        {
            _client.BaseAddress = _uri;
            HttpResponseMessage response = null;

            StringContent data = new StringContent(requestObj.ToString(), Encoding.UTF8, "application/json");
            try
            {
                response = await _client.PostAsync(@endpoint, data);
            }
            catch
            {
                response.StatusCode = HttpStatusCode.BadGateway;
                StringBuilder errorMesagge = new StringBuilder();
                errorMesagge.Append("RequestURI:");
                errorMesagge.Append(_client.BaseAddress);
                errorMesagge.Append(endpoint);
                response.Content = new StringContent(errorMesagge.ToString());
            }
            return response;
        }
    }
}
