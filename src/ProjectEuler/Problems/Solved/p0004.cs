using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0004 : euler
  {
    const int Max = 999;
    const int Min = 99;

    public override void Run()
    {
      var result = 0;
      var l = 0;
      var r = 0;

      for (var a = Max; a > Min; --a)
      {
        for (var b = a; b > Min; --b)
        {
          var p = (a * b);
          if (math.isPalindrome(p) && p > result)
          {
            result = p;
            l = a;
            r = b;
          }
        }
      }

      Console.WriteLine("{0} x {1} = {2}", l, r, result);
    }
  }
}
