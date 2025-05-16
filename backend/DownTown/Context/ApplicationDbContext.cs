using DownTown.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DownTown.Context;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        var role = new Role
        {
            Id = 1,
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };
        
        var user = new User
        {
            Id = 1,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            PasswordHash = new PasswordHasher<User>().HashPassword(null, "admin123!"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        builder.Entity<Role>().HasData(role);
        builder.Entity<User>().HasData(user);
        
        builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            }
        );
    }
    
    public DbSet<Image> Images { get; set; }
    public DbSet<Decor> Menus { get; set; }
    public DbSet<AcilisTedbirleri> AcilisTedbirleris { get; set; }
    public DbSet<AdGunleri> AdGunleris { get; set; }
    public DbSet<BoyOrGirl> BoyOrGirls { get; set; }
    public DbSet<CapIsleri> CapIsleris { get; set; }
    public DbSet<FotoXidmetler> FotoXidmetlers { get; set; }
    public DbSet<MasaBezedilmesi> MasaBezedilmesis { get; set; }
    public DbSet<SokoladCapi> SokoladCapis { get; set; }
    public XestexanaCixisi XestexanaCixisi { get; set; }
}