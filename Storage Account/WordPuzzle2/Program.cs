using System;

namespace WordPuzzle2
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = Console.ReadLine();
            bool flag = true;
            for(int i=0; i<=word.Length-1;i++)
            {
                if(i == word.Length - i - 1)
                {
                    break;
                }
                if(word[i] != word[word.Length-i-1])
                {
                    flag = false;
                    break;
                }
               
            }

            if(flag)
            {
                Console.WriteLine("Word is same in reverse order!");
            }
            else
            {
                Console.WriteLine("Word is not same in reverse order!");
            }
            Console.ReadLine();


        }
    }
}
