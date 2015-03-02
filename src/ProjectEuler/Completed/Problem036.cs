using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem036 : EulerProblem
  {
    public override int Number { get { return 36; } }

    public override object Solve()
    {
      long sum = 0;
      for (int i = 1; i < 1000000; ++i)
      {
        string base10 = i.ToString();
        if (base10.IsPalindrome())
        {
          string base02 = Convert.ToString(i, 2);
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
