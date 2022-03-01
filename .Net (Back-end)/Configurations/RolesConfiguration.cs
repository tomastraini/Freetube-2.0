using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST.Configurations
{
    public partial class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> entity)
        {
            entity.HasKey(e => e.id_rol)
                .HasName("PK__roles__6ABCB5E0D648E2F1");

            entity.ToTable("roles");

            entity.Property(e => e.id_rol).HasColumnName("id_rol");

            entity.Property(e => e.nombre_rol).HasColumnName("nombre_rol");



            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Roles> entity);
    }
}
