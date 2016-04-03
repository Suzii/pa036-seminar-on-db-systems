using DataAccess.Model;

namespace Service.DTO
{
    public class ProductDTO
    {
        public ProductDTO() { }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            StockCount = product.StockCount;
            UnitCost = product.UnitCost;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int StockCount { get; set; }

        public decimal UnitCost { get; set; }
    }
}