﻿using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;
using Service.DTO.TestScenariosConfigs;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario7Controller : ApiController
    {
        private ITestScenarioService _instance;

        public async Task<ITestResult> Get(bool useCloudDatabase = false)
        {
            var config = new Scenario1Config()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance = new Scenario7Service(config);
            return await _instance.ExecuteTest();
        }
    }
}
