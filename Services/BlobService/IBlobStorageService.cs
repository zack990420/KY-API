using MyWebApi.Enums;

namespace MyWebApi.Services;

public interface IBlobStorageService
{
    Task<string> UploadAsync(string folderName, BlobContentType contentType, string fileName, Stream content, BlobType blobType, long userId);
    Task DeleteAsync(string folderName, string blobType, string fileName);
}
