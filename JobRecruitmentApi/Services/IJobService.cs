using JobRecruitmentApi.Models;

namespace JobRecruitmentApi.Services;

public interface IJobService
{
    Task<List<Job>> GetJobsAsync();

    Task<Job?> GetJobByIdAsync(int id);

    Task<Job> CreateJobAsync(CreateJobRequest request);
    
    Task<Job?> UpdateJobAsync(int id, UpdateJobRequest request);
}