using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Product2 : IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int StockCount { get; set; }

        public decimal UnitCost { get; set; }
    }
}