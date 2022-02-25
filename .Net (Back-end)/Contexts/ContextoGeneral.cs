using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Practise.Configurations;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Contexts
{
    public partial class ContextoGeneral : DbContext
    {
        public ContextoGeneral()
        {

        }

        public ContextoGeneral(DbContextOptions<ContextoGeneral> options) : base(options)
        {

        }
        public DbSet<comments> comments {get; set;}
        public DbSet<users> users { get; set; }
        public DbSet<videos> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new VideosConfiguration());
            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
