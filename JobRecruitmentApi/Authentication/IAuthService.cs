using JobRecruitmentApi.Models;

namespace JobRecruitmentApi.Authentication;

public interface IAuthService
{
    Task<User?> RegisterAsync(string userName, string password);
}