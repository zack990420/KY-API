using Microsoft.EntityFrameworkCore;
using KYAPI.Data;
using KYAPI.Entities;

namespace KYAPI.Repositories;

public class EmailConfigRepository : IEmailConfigRepository
{
    private readonly AppDbContext _context;

    public EmailConfigRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmailConfigEntity?> GetActiveConfigAsync()
    {
        return await _context.EmailConfigs
            .FirstOrDefaultAsync(c => c.IsActive);
    }
}
