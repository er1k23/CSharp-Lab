using JobRecruitmentApi.Models;

namespace JobRecruitmentApi.Services;

public interface IJobService
{
    Task<List<JobResponse>> GetJobsAsync();

    Task<JobResponse?> GetJobByIdAsync(int id);

    Task<JobResponse> CreateJobAsync(CreateJobRequest request);
    
    Task<JobResponse?> UpdateJobAsync(int id, UpdateJobRequest request);

    Task<JobResponse?> PatchJobAsync(int id, PatchJobRequest request);
    
    Task<bool> DeleteJobAsync(int id);

    Task<List<JobResponse>> SearchJobsAsync(string? title);
}