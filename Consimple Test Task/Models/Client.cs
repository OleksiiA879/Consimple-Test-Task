using System.ComponentModel.DataAnnotations;

namespace Consimple_Test_Task.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
