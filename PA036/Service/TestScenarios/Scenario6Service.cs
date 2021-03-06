﻿using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;
using Shared.Enums;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Service.TestScenarios
{
    public class Scenario6Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private CancellationTokenSource cancelationToken = new CancellationTokenSource();

        public Scenario6Service(ScenarioConfig config)
        {
            var dbSettings = new DbSettings { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();

            var productFilter = new ProductFilter {Take = 1};
            _instance.Get(productFilter);
            _instance.Get(productFilter);
            _databaseService.InvalidateCache();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return  await GetMaxCacheSize();
        }

        /// <summary> 
        /// Maximazing number of elements in cache
        /// </summary>
        /// <returns>Number of elements in cache</returns>
        private async Task<Scenario6Results> GetMaxCacheSize()
        {
            var totalCount = await _instance.TotalCountAsync();

            _databaseService.InvalidateCache();
            var message = "OK";
            try
            {
                var tasks = new List<Task>();
                for (var i = 1; i < totalCount; i++)
                {
                    for (var j = 0; j <= totalCount - i; j++)
                    {
                        Task task = Task.Run(async () =>
                        {
                            var filter = new ProductFilter() { Take = i, Skip = j, OrderDesc = true };
                            await _instance.GetAsync(filter);
                        }, cancelationToken.Token);
                    }
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                _databaseService.InvalidateCache();
                message = e.Message;
                cancelationToken.Cancel();
            }

            var result = new Scenario6Results
            {
                Message = message
            };

            return result;
        }
    }
}