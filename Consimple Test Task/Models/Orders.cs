using System.ComponentModel.DataAnnotations;

namespace Consimple_Test_Task.Models
{
    public class Orders
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<OrderProduct> PurchaseProducts { get; set; } = new List<OrderProduct>();
    }
}
