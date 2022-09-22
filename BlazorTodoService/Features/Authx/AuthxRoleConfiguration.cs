using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorTodoService.Features.Authx;

public class AuthxRoleConfiguration : IEntityTypeConfiguration<AuthxRole>
{
    public const string VisitorRole = "Visitor";
    public const string AdministratorRole = "Administrator";

    public void Configure(EntityTypeBuilder<AuthxRole> builder)
    {
        Console.WriteLine("Adding identity roles");
        builder.HasData(
            new AuthxRole
            {
                Id = Guid.NewGuid(),
                Name = VisitorRole,
                NormalizedName = VisitorRole.ToUpperInvariant()
            },
            new AuthxRole
            {
                Id = Guid.NewGuid(),
                Name = AdministratorRole,
                NormalizedName = AdministratorRole.ToUpperInvariant()
            });
    }
}