using System;
using Azure.Storage.Blobs;

namespace IntroductionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BlobClient blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=sdkdemo876;AccountKey=VdnDFmBWjlHShowrDhmAi13aiTYaJsZAhmn+/CJYBdJL/RlWYKbjKmFrUje3GAw3DdVqLOdACFHAq+9Q7UWXfQ==;EndpointSuffix=core.windows.net","demoContainer","demo.json");

            Console.WriteLine("Hello World!");
        }
    }
}
