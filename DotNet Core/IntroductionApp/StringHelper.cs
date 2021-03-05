using System;
using System.Collections.Generic;
using System.Text;

namespace IntroductionApp
{
    public static class StringHelper
    {
        public static string ChangeStringCase(this string input)
        {
            if(input.Length > 0)
            {
                char[] charArray = input.ToCharArray();
                charArray[0] = char.IsUpper(charArray[0]) ? char.ToLower(charArray[0]) : char.ToUpper(charArray[0]);
                return new string(charArray);


            }

            return input;
        }
    }
}
