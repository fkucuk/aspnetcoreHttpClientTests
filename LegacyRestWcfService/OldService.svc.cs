using System.ServiceModel.Activation;

namespace LegacyRestWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class OldService : IOldService
    {
        public GetWeatherResponse GetWeather(GetWeatherRequest request)
        {
            return new GetWeatherResponse
            {
                Temp = 23
            };
        }
    }

    public class GetWeatherRequest
    {
        public string City { get; set; }
    }

    public class GetWeatherResponse
    {
        public int Temp { get; set; }
    }
}
