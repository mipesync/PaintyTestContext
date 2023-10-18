using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaintyTestContext.Domain;

namespace PaintyTestContext.Persistence.EntityConfigurations;

/// <summary>
/// Конфигурация таблицы прав пользователя
/// </summary>
public class PermissionConfiguration: IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(p => new
        {
            p.UserId,
            p.FriendId
        });
    }
}