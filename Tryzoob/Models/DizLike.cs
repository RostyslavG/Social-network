﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryzoob.Models
{
    public class DizLike
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_Author_123")]
        public int AuthorId { get; set; }
        [ForeignKey("FK_Publication_123")]
        public int PublicationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
