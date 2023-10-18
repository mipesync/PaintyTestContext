using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы пользователей
/// </summary>
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Id).IsUnique();
        
        builder.Property(x => x.Email).IsRequired();
    }
}