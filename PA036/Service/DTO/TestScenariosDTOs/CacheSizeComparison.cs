namespace Service.DTO.TestScenariosDTOs
{
    public class CacheSizeComparison
    {
        public int BeforeQueryExecution { get; set; }

        public int AfterQueryExecution { get; set; }

        public int NoOfObjectsReturnedInQuery { get; set; }
    }
}