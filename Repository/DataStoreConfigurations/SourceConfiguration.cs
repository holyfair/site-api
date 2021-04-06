using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.DatabaseModels;

namespace Repository.DataStoreConfigurations
{
    public class SourceConfiguration : IEntityTypeConfiguration<SourceRecord>
    {
        public void Configure(EntityTypeBuilder<SourceRecord> builder)
        {
            builder.ToTable("Sources");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Content).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
