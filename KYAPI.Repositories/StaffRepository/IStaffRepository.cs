using KYAPI.DTOs;
using KYAPI.Entities;

namespace KYAPI.Repositories;

public interface IStaffRepository : IBaseRepository<StaffInfo>
{
    Task<GlobalApiResponse<string>> CreateStaffAccount(CreateStaffRequestDto request, long createdBy);
}