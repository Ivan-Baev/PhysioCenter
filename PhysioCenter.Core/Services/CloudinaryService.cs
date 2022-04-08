namespace PhysioCenter.Core.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    using PhysioCenter.Core.Contracts;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this._cloudinary = cloudinary;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileName)
        {
            byte[] destinationFile;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationFile = memoryStream.ToArray();
            }

            ImageUploadResult uploadResult;
            using (var ms = new MemoryStream(destinationFile))
            {
                var uploadParams = new ImageUploadParams()
                {
                    Folder = "PhysioCenter Images",
                    File = new FileDescription(fileName, ms),
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task DeleteFileAsync(string url)
        {
            var shortUrl = "PhysioCenter Images/" + GetShortenedUrl(url);

            var deletionParams = new DeletionParams(shortUrl);
            await _cloudinary.DestroyAsync(deletionParams);
        }

        private static string GetShortenedUrl(string url)
        {
            var startIndex = url.LastIndexOf('/') + 1;
            var length = url.LastIndexOf('.') - startIndex;
            var shortUrl = url.Substring(startIndex, length);

            return shortUrl;
        }
    }
}