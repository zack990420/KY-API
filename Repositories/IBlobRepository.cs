using MyWebApi.Entities;

namespace MyWebApi.Repositories;

public interface IBlobRepository
{
    Task AddAsync(BlobFileEntity entity);
    Task DeleteAsync(BlobFileEntity entity);
    Task<BlobFileEntity?> GetBySystemFileNameAsync(string systemFileName);
}
