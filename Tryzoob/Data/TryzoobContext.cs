using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tryzoob.Models;

namespace Tryzoob.Data
{
    public class TryzoobContext : DbContext
    {
        public TryzoobContext (DbContextOptions<TryzoobContext> options)
            : base(options)
        {
        }

        public DbSet<Tryzoob.Models.Author> Author { get; set; } = default!;
        public DbSet<Tryzoob.Models.Publication> Publication { get; set; } = default!;
        public DbSet<Tryzoob.Models.Like> Like { get; set; } = default!;
        public DbSet<Tryzoob.Models.DizLike> DizLike { get; set; } = default!;
    }
}
