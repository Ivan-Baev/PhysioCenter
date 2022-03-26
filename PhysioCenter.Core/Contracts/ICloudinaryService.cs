namespace PhysioCenter.Core.Contracts
{
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadFileAsync(IFormFile file, string fileName);

        Task DeleteFileAsync(string url);
    }
}