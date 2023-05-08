using Supabase;
using Supabase.Interfaces;
using Supabase.Storage;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;
using UserProvisioning3;

namespace MyNamespace
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            const string url = "";
            const string token = "";
            var clientStorage = new SupabaseStorageClient(url, token);

            Console.WriteLine("Welcome to My Program!");

            Console.WriteLine("1. Create a bucket");
            Console.WriteLine("2. Use existing bucket name");
            var bucketChoice = Console.ReadLine();

            Console.WriteLine("Enter Bucket Name");
            var bucketName = Console.ReadLine();

            if (bucketChoice!.Equals("1"))
            {
                
                await clientStorage.CreateBucket(bucketName);
            }

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("2. Get a bucket");
            Console.WriteLine("3. Update a bucket");
            Console.WriteLine("4. List all buckets");
            Console.WriteLine("5. Delete a bucket");
            Console.WriteLine("6. Empty a bucket");
            Console.WriteLine("7. List all file in a bucket");

            var choice = Console.ReadLine();
            Bucket bucket;

            switch (choice)
            {
                case "2":
                    bucket = await clientStorage.GetBucket(bucketName!);
                    if (bucket != null)
                    {
                        //show bucket info from bucket object
                    }
                    break;
                case "3":
                    bucket = await clientStorage.UpdateBucket(bucketName!, false);
                    break;
                case "4":
                    var buckets = await clientStorage.ListBuckets(bucketName);
                    Console.WriteLine($"Total buckets found {buckets.Count()}");
                    var currentIndex = 1;

                    foreach (var item in buckets)
                    {
                        Console.WriteLine($"{currentIndex}. {item.Id}");
                        currentIndex++;
                    }
                    break;
                case "5":
                    await clientStorage.DeleteBucket();
                    break;
                case "6":
                    var response = await clientStorage.EmptyBucket(bucketName!);
                    Console.WriteLine(response.Message);
                    break;
                case "7":
                    Console.WriteLine("Please enter folder path:");
                    var folderPath = Console.ReadLine();
                    var fileObjects = await clientStorage.ListFileBucket(bucketName!, folderPath);
                    Console.WriteLine($"Total file objects found {fileObjects.Count()}");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}

