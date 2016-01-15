using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0036 : euler
  {
    public override void Run()
    {
      long sum = 0;
      for (var i = 1; i < 1000000; ++i)
      {
        if (math.isPalindrome(i))
        {
          var base02 = Convert.ToString(i, 2);
          if (math.isPalindrome(base02))
          {
            sum += i;
          }
        }
      }
      Console.WriteLine(sum);
    }
  }
}
