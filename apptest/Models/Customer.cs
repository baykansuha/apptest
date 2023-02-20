using System.ComponentModel.DataAnnotations;

namespace apptest.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Product> Orders { get; set; }
    }
}
