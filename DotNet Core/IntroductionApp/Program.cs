using System;
using Azure.Storage.Blobs;

namespace IntroductionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BlobClient blobClient = new BlobClient("","demoContainer","demo.json");

            Console.WriteLine("Hello World!");
        }
    }
}
