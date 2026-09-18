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
}