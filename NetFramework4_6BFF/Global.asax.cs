using System.Web.Http;

namespace NetFramework4_6BFF
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
        }
    }
}
