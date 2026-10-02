using Microsoft.EntityFrameworkCore;
using TelegramFinanceBot.Data;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Models;
using System.Security.Cryptography;

namespace TelegramFinanceBot.Services;

public class ChatService : IChatService
{
    private readonly ApplicationDbContext _context;

    public ChatService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Chat> RegisterChatAsync(long chatId)
    {
        var existingChat = await _context.Chats.FirstOrDefaultAsync(chat => chat.Id == chatId);

        if (existingChat is not null)
        {
            return existingChat;
        }

        var chat = new Chat
        {
            Id = chatId,
            ReportToken = Convert.ToHexString(
                RandomNumberGenerator.GetBytes(16)),
            StartedAt = DateTime.UtcNow
        };

        _context.Add(chat);

        await _context.SaveChangesAsync();

        return chat;
    }

    public async Task<bool> ChatExistsAsync(long chatId)
    {
        return await _context.Chats.AnyAsync(chat => chat.Id == chatId);
    }

    public async Task<List<Chat>> GetAllChatsAsync()
    {
        return await _context.Chats.ToListAsync();
    }
}