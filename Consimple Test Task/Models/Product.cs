using System.ComponentModel.DataAnnotations;

namespace Consimple_Test_Task.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }

        public int Price { get; set; }

        public ICollection<OrderProduct> PurchaseProducts { get; set; } = new List<OrderProduct>();
    }
}
