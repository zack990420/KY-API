using MyWebApi.Entities;

namespace MyWebApi.Repositories;

public interface IEmailConfigRepository
{
    Task<EmailConfigEntity?> GetActiveConfigAsync();
}
