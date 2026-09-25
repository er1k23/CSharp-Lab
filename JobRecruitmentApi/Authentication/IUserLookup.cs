using JobRecruitmentApi.Models;

namespace JobRecruitmentApi.Authentication;

public interface IUserLookup
{
    Task<User?> FindByCredentialsAsync(string userName, string password);
}