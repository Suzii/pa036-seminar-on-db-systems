using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestApi
{
    public class JsonConfig
    {
        public static void ConfigureJsonFormatter()
        {
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}