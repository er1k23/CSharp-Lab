using JobRecruitmentApi.Authentication;
using JobRecruitmentApi.Data;
using JobRecruitmentApi.Services;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using JobRecruitmentApi.Middleware;
using Microsoft.AspNetCore.Identity;
using JobRecruitmentApi.Authentication;
using JobRecruitmentApi.Models;
using JobRecruitmentApi.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication("Basic")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
        "Basic", null);

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserLookup, UserLookup>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Job Recruitment API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();