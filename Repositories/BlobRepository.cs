using KYAPI.Data;
using KYAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace KYAPI.Repositories;

public class BlobRepository : IBlobRepository
{
    private readonly AppDbContext _context;

    public BlobRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BlobFileEntity entity)
    {
        await _context.BlobFiles.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(BlobFileEntity entity)
    {
        _context.BlobFiles.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<BlobFileEntity?> GetBySystemFileNameAsync(string systemFileName)
    {
        return await _context.BlobFiles.FirstOrDefaultAsync(b => b.SystemFileName == systemFileName);
    }
}
