using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.DatabaseModels;
using System;

namespace Repository.DataStoreConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Password)
                .IsRequired();

            builder.Property(p => p.Email)
                .IsRequired();

            builder.HasData(
                new User()
                {
                    Id = 1,
                    Email = "test@gmail.com",
                    Password = "password"
                });
        }
    }
}
