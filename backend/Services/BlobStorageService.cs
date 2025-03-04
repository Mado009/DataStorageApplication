using Azure.Storage.Blobs;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class BlobStorageService {
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public BlobStorageService(IConfiguration config)
    {
        _blobServiceClient = new BlobServiceClient(config["AzureBlobStorage:ConnectionString"]);
        _containerName = config["AzureBlobStorage:ContainerName"];
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_containerName);
        await container.CreateIfNotExistsAsync();

        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var blob = container.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            await blob.UploadAsync(stream, true);
        }

        return fileName;
    }

    public async Task UploadMetadataAsync(string fileName, object metadata)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_containerName);
        var metadataBlob = container.GetBlobClient($"{fileName}.json");

        using (var jsonStream = new MemoryStream(JsonSerializer.SerializeToUtf8Bytes(metadata)))
        {
            await metadataBlob.UploadAsync(jsonStream, true);
        }
    }
}
