using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Store : IDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}
