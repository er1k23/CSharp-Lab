using Microsoft.EntityFrameworkCore;
using JobRecruitmentApi.Models;

namespace JobRecruitmentApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<User> Users { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Job>()
            .HasOne(job => job.Owner)
            .WithMany()
            .HasForeignKey(job => job.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}