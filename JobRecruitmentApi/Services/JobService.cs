using JobRecruitmentApi.Data;
using JobRecruitmentApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitmentApi.Services;

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;

    public JobService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Job>> GetJobsAsync()
    {
        return await _context.Jobs.ToListAsync();
    }

    public async Task<Job?> GetJobByIdAsync(int id)
    {
        return await _context.Jobs.FindAsync(id);
    }

    public async Task<Job> CreateJobAsync(CreateJobRequest request)
    {
        var job = new Job
        {
            Title = request.Title,
            Description = request.Description,
            Salary = request.Salary
        };

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return job;
    }

    public async Task<Job?> UpdateJobAsync(int id, UpdateJobRequest request)
    {

        var job = await _context.Jobs.FindAsync(id);

        if (job is null)
        {
            return null;
        }

        job.Title = request.Title;
        job.Description = request.Description;
        job.Salary = request.Salary;

        await _context.SaveChangesAsync();

        return job;
    }

    public async Task<Job?> PatchJobAsync(int id, PatchJobRequest request)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job is null)
        {
            return null;
        }

        if (request.Title is not null)
        {
            job.Title = request.Title;
        }

        if (request.Description is not null)
        {
            job.Description = request.Description;
        }

        if (request.Salary is not null)
        {
            job.Salary = request.Salary.Value;
        }

        await _context.SaveChangesAsync();

        return job;
    }
}