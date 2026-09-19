using JobRecruitmentApi.Models;
using JobRecruitmentApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobRecruitmentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs()
    {
        var jobs = await _jobService.GetJobsAsync();

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob([FromRoute] int id)
    {
        var job = await _jobService.GetJobByIdAsync(id);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpGet("search")]
    public IActionResult SearchJobs([FromQuery] string? title)
    {
        return Ok($"Searching for: {title}");
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob(
        [FromBody] CreateJobRequest request)
    {
        var job = await _jobService.CreateJobAsync(request);

        return CreatedAtAction(
            nameof(GetJob),
            new { id = job.Id },
            job);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob([FromRoute] int id, [FromBody] UpdateJobRequest request)
    {
        var job = await _jobService.UpdateJobAsync(id, request);
        
        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchJob([FromRoute] int id, [FromBody] PatchJobRequest request)
    {
        var job = await _jobService.PatchJobAsync(id, request);

        if (job is null)
        {
            return NotFound();
        }

        return Ok(job);
    }
}