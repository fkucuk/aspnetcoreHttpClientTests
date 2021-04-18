using System.ServiceModel;
using System.ServiceModel.Web;

namespace LegacyRestWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOldService
    {

        [OperationContract(Name="GetWeather")]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "weather")]
        GetWeatherResponse GetWeather(GetWeatherRequest request);
    }
}
