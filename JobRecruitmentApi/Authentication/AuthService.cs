using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using JobRecruitmentApi.Models;
using JobRecruitmentApi.Data;

namespace JobRecruitmentApi.Authentication;

public class AuthService: IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> RegisterAsync(string userName, string password)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        if (existingUser is not null)
        {
            return null;
        }

        var user = new User
        {
            UserName = userName
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, password);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }
}