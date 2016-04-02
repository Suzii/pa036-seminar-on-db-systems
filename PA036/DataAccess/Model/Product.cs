using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Product : IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public uint StockCount { get; set; }

        public decimal UnitCost { get; set; }
    }
}