using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem036 : EulerProblem
  {
    public override int Number
    {
      get { return 36; }
    }

    public override object Solve()
    {
      long sum = 0;
      for (var i = 1; i < 1000000; ++i)
      {
        var base10 = i.ToString();
        if (base10.IsPalindrome())
        {
          var base02 = Convert.ToString(i, 2);
          if (base02.IsPalindrome())
          {
            sum += i;
          }
        }
      }
      return sum;
    }
  }
}