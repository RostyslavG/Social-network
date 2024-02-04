using System.ComponentModel.DataAnnotations;

namespace Tryzoob.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public double Rate { get; set; }
        public string? Avatar { get; set; }
        public List<Publication>? Publications { get; set; }
    }
}
