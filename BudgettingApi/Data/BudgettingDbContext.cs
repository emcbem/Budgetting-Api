using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BudgettingApi.Data;

public class BudgettingDbContext : DbContext
{
    public BudgettingDbContext() { }
    public BudgettingDbContext(DbContextOptions<BudgettingDbContext> options) : base(options) { }

    public virtual DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity => 
        {
           entity.HasKey(e => e.Id).HasName("user_pkey");
           entity.ToTable("user_account");

           entity.Property(e => e.Id).HasColumnName("id").UseIdentityAlwaysColumn(); 
           entity.Property(e => e.Email).HasColumnName("email");
           entity.Property(e => e.Name).HasColumnName("name");
           entity.Property(e => e.ProviderId).HasColumnName("provider_id");
        });
    }
}