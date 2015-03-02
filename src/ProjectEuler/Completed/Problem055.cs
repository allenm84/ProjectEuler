using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem055 : EulerProblem
  {
    public override int Number
    {
      get { return 55; }
    }

    public override object Solve()
    {
      var count = 0;
      for (var n = 0; n < 10000; ++n)
      {
        var num = new BigInteger(n);
        var isPalindrome = false;

        for (var i = 0; !isPalindrome && i < 50; ++i)
        {
          var b = BigInteger.Parse(new string(num.ToString().Reverse().ToArray()));
          num += b;
          isPalindrome = num.ToString().IsPalindrome();
        }

        if (!isPalindrome)
        {
          ++count;
        }
      }

      return count;
    }
  }
}