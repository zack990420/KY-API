using Azure.Storage.Blobs;
using KYAPI.Entities;
using KYAPI.Enums;
using KYAPI.Repositories;

namespace KYAPI.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IBlobRepository _blobRepository;
    private const string ContainerName = "uploads"; // Default container

    public BlobStorageService(IConfiguration configuration, IBlobRepository blobRepository)
    {
        _blobRepository = blobRepository;
        var connectionString = configuration["AzureStorage:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(
                "AzureStorage:ConnectionString",
                "Azure Storage Connection String is missing in configuration."
            );
        }
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<string> UploadAsync(
        string folderName,
        BlobContentType contentType,
        string fileName,
        Stream content,
        BlobType blobType,
        long userId
    )
    {
        // Construct path: folderName/blobType/fileName
        // Generate a unique system file name to prevent overwrites and track in DB
        var systemFileName = $"{Guid.NewGuid()}_{fileName}";
        var blobPath = $"{folderName}/{blobType}/{systemFileName}";

        var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(blobPath);

        // Upload (overwrite if exists)
        await blobClient.UploadAsync(content, true);

        // Save metadata to DB
        var entity = new BlobFileEntity
        {
            FileName = fileName,
            SystemFileName = systemFileName,
            BlobType = blobType.ToString(),
            BlobPath = blobPath,
            ContentType = contentType,
            EntryBy = userId,
        };

        await _blobRepository.AddAsync(entity);

        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string folderName, string blobType, string fileName)
    {
        // Note: fileName here is expected to be the SystemFileName if we want to delete by specific file
        // However, the interface signature implies we might be deleting by the original name or path.
        // Given the requirement "Upload async , delete async", and the new table structure,
        // we should probably delete by SystemFileName to ensure we delete the correct record.
        // But for now, I will assume the 'fileName' passed to DeleteAsync IS the SystemFileName
        // because that's what uniquely identifies the blob in storage.

        var blobPath = $"{folderName}/{blobType}/{fileName}";
        var containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient(blobPath);

        if (await blobClient.DeleteIfExistsAsync())
        {
            // Remove metadata from DB
            var entity = await _blobRepository.GetBySystemFileNameAsync(fileName);
            if (entity != null)
            {
                await _blobRepository.DeleteAsync(entity);
            }
        }
    }
}
