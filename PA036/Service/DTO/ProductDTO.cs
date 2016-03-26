using DataAccess.Model;

namespace Service.DTO
{
    public class ProductDTO
    {
        public ProductDTO() { }

        public ProductDTO(Product product)
        {
            id = product.Id;
            Name = product.Name;
            StockCount = product.StockCount;
            UnitCost = product.UnitCost;
        }

        public int id { get; set; }

        public string Name { get; set; }

        public uint StockCount { get; set; }

        public decimal UnitCost { get; set; }
    }
}