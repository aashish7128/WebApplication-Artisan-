using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication_Artisan_.Areas.Identity.Data;

namespace WebApplication_Artisan_.Areas.Identity.Data;

public class WebApplication_Artisan_ContextDB : IdentityDbContext<Artisan_User>
{
    public WebApplication_Artisan_ContextDB(DbContextOptions<WebApplication_Artisan_ContextDB> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
