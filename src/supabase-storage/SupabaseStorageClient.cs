using Supabase.Storage;

namespace UserProvisioning3;

internal class SupabaseStorageClient
{
    private readonly Supabase.Client _client;

    public SupabaseStorageClient(string baseUrl, string token)
    {
        _client = new Supabase.Client(baseUrl, token);
    }

    public async Task CreateBucket(string bucketName)
    {
        try
        {
            await _client.Storage.CreateBucket(bucketName);
            Console.WriteLine($"Bucket {bucketName} created");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task AddObject(string bucketName)
    {
        try
        {
            Console.Write("Enter Image Path: ");
            var insertImagePath = Console.ReadLine();
            var imagePath3 = Path.Combine("Folder3", insertImagePath);

            Console.Write("Give storagePath: ");
            var storagePath = Console.ReadLine();

            await _client.Storage
                .From(bucketName)
                .Upload(imagePath3, storagePath);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<Bucket> GetBucket(string bucketName)
    {
        try
        {

            var bucket = await _client.Storage.GetBucket(bucketName);
            return bucket!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null!;
    }

    public async Task<Bucket> UpdateBucket(string bucketName, bool publicBucket)
    {
        try
        {

            var bucket = await _client.Storage.UpdateBucket(bucketName, new BucketUpsertOptions { Public = publicBucket });
            return bucket!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null!;
    }

    public async Task<List<Bucket>> ListBuckets(string bucketName)
    {
        try
        {

            var buckets = await _client.Storage.ListBuckets();
            return buckets!;

            //Console.WriteLine("The number of bucket is: "+ bucket[0].Count);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null!;

    }

    public async Task<GenericResponse> EmptyBucket(string bucketName)
    {
        try
        {

            var response = await _client.Storage.EmptyBucket(bucketName);
            return response!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null!;
    }

    public  async Task DeleteBucket()
    {
        try
        {          
            Console.Write("Enter bucket name:");
            var bucketName = Console.ReadLine();
            var bucket = await _client.Storage.DeleteBucket(bucketName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
    }

    public async Task<List<FileObject>> ListFileBucket(string bucketName, string folderPath)
    {
        try
        {
            
            var objects = await _client.Storage.From(bucketName).List(folderPath);

            return objects!;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null!;
    }


}
