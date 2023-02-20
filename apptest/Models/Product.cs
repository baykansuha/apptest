using System.ComponentModel.DataAnnotations;

namespace apptest.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
