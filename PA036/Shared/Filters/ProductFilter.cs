
namespace Shared.Filters
{
    public class ProductFilter : BaseFilter
    {
        public string NameFilter { get; set; }

        public decimal? UnitCostFilter { get; set; }

        public int? StockCountFilter { get; set; }
    }
}
