using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryzoob.Models
{
    public class Publication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        [ForeignKey("FK_Author_123")]
        public int AuthorId { get; set; }
        [MaxLength(255)]
        public string DataUrl { get; set; }
        public int Likes { get; set; }
        public int DisLikes { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IsPublic { get; set; }
        public Author Author { get; set; }
    }
}
