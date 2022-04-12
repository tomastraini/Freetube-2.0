using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Practise;
using System;
using REST.Models;

namespace REST.Configurations
{
    public partial class LikesConfiguration : IEntityTypeConfiguration<likes>
    {
        public void Configure(EntityTypeBuilder<likes> entity)
        {
            entity.HasKey(e => e.id_like)
                .HasName("PK__likes__998412E8174A2A6D");

            entity.ToTable("likes");

            entity.Property(e => e.id_like).HasColumnName("id_like");

            entity.Property(e => e.id_video).HasColumnName("id_video");

            entity.Property(e => e.id_user).HasColumnName("id_user");

            entity.Property(e => e.liked).HasColumnName("liked");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<likes> entity);
    }
}
