
namespace Shared.Modifiers
{
    public class ProductModifier : BaseModifier
    {
        public string NameFilter { get; set; }

        public decimal? UnitCostFilter { get; set; }

        public int? StockCountFilter { get; set; }
    }
}
