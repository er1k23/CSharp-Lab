using JobRecruitmentApi.Data;
using JobRecruitmentApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitmentApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public JobsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetJobs()
    {
        var jobs = await _context.Jobs.ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJob([FromRoute] int id)
    {
        var job = await _context.Jobs.FindAsync(id);

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
    public async Task<IActionResult> CreateJob([FromBody] CreateJobRequest request)
    {
        var job = new Job
        {
            Title = request.Title,
            Description = request.Description,
            Salary = request.Salary
        };

        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetJob),
            new { id = job.Id },
            job);
    }
}