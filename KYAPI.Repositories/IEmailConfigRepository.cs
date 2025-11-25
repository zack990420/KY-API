using KYAPI.Entities;

namespace KYAPI.Repositories;

public interface IEmailConfigRepository
{
    Task<EmailConfigEntity?> GetActiveConfigAsync();
}
