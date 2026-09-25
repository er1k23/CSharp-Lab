using JobRecruitmentApi.Models;
using JobRecruitmentApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitmentApi.Authentication;

public class UserLookup: IUserLookup
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserLookup(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> FindByCredentialsAsync(string userName, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        if (user is null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }
}