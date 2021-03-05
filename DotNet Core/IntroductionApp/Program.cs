using System;
using Azure.Storage.Blobs;

namespace IntroductionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "ravi";

            string result = name.ChangeStringCase();
           // string result = StringHelper.ChangeStringCase(name);

            Console.WriteLine(result);
        }
    }
}
