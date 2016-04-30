using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Product : IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int StockCount { get; set; }

        public decimal UnitCost { get; set; }

        [ForeignKey("Store")]
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}
