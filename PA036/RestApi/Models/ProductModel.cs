namespace RestApi.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public uint StockCount { get; set; }

        public decimal UnitCost { get; set; }
    }
}