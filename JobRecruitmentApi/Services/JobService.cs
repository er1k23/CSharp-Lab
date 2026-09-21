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

    private static JobResponse ToResponse(Job job)
    {
        return new JobResponse
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            Salary = job.Salary
        };
    }
    
    
    public async Task<List<JobResponse>> GetJobsAsync()
    {
        var jobs =  await _context.Jobs.AsNoTracking().ToListAsync();
        return jobs.Select(ToResponse).ToList();
    }

    public async Task<JobResponse?> GetJobByIdAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        return job is null ? null : ToResponse(job);
    }

    public async Task<JobResponse> CreateJobAsync(CreateJobRequest request)
    {
        var job = new Job
        {
            Title = request.Title,
            Description = request.Description,
            Salary = request.Salary
        };

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return ToResponse(job);
    }

    public async Task<JobResponse?> UpdateJobAsync(int id, UpdateJobRequest request)
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

        return ToResponse(job);
    }

    public async Task<JobResponse?> PatchJobAsync(int id, PatchJobRequest request)
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

        return ToResponse(job);
    }


    public async Task<bool> DeleteJobAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);

        if (job is null)
        {
            return false;
        }

        _context.Jobs.Remove(job);

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<List<JobResponse>> SearchJobsAsync(string? title)
    {
        var query =  _context.Jobs.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(job => job.Title.Contains(title));
        }

        var jobs = await query.ToListAsync();
        return jobs.Select(ToResponse).ToList();
    }
}