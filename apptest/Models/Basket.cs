using System.ComponentModel.DataAnnotations;

namespace apptest.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Count { get; set; }
    }
}
