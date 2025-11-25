using Microsoft.EntityFrameworkCore;
using KYAPI.Data;
using KYAPI.Entities;
using KYAPI.Services;

namespace KYAPI.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly AppDbContext _context;
    private readonly IIdHasher _idHasher;
    
    public StaffRepository(AppDbContext context, IIdHasher idHasher)
    {
        _context = context;
        _idHasher = idHasher;
    }

   public async Task CreateStaffAccount (RegisterRequestDto registerRequestDto)
   {
    
   }
}