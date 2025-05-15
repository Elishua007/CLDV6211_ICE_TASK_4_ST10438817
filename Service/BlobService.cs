using Azure.Storage.Blobs;


namespace BlobImage.Service


{

    public interface IBlobService
    {
        Task<string> UploadAsync(Stream file, string fileName, string contentType);
    }

    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _cotainer;

        public BlobService(IConfiguration cfg)
        {
            var conn = cfg["Storage:ConnectionString"];
            var containerName = cfg["Storage:Container"];
            _cotainer = new BlobContainerClient(conn, containerName);
            _cotainer.CreateIfNotExists(Azure.Storage.Blobs.Models.PublicAccessType.Blob);


        }

        public async Task<string> UploadAsync(Stream file, string fileName, string contentType)
        {
            var blob = _cotainer.GetBlobClient(fileName);
            await blob.UploadAsync(file, new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = contentType });
            return blob.Uri.ToString();

        }
    }

    }



