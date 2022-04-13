using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using REST.Models;

namespace REST.Configurations
{
    public partial class viewedVideosConfiguration : IEntityTypeConfiguration<viewedVideos>
    {
        public void Configure(EntityTypeBuilder<viewedVideos> entity)
        {
            entity.HasKey(e => e.id_view)
                .HasName("PK__viewedVi__5CB912ADEFE5A9E3");

            entity.ToTable("viewedVideos");

            entity.Property(e => e.id_view).HasColumnName("id_view");

            entity.Property(e => e.id_video).HasColumnName("id_video");

            entity.Property(e => e.id_user).HasColumnName("id_user");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<viewedVideos> entity);
    }
}
