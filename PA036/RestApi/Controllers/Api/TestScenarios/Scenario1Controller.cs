using System.Web.Http;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario1Controller : ApiController
    {
        public string Get()
        {
            return
                "This is an example of a TestScenario controller that executes the test and returns statistics as a json object.";
        }

    }
}
