using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class FizzBuzzGenerator
    {
        public static string[] GetFizzBuzzArray(int numberItems)
        {
            if (numberItems < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var result = new string[numberItems];
            for (int i = 0; i < numberItems; i++)
            {
                var value = i + 1;

                if((value % 3) == 0 && (value % 5) == 0)
                {
                    result[i] = "FizzBuzz";
                }
                else if ((value % 3) == 0)
                {
                    result[i] = "Fizz";
                }
                else if ((value % 5) == 0)
                {
                    result[i] = "Buzz";
                }
                else
                {
                    result[i] = value.ToString();
                }
            }

            return result;
        }
    }
}
