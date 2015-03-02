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

namespace ProjectEuler
{
  public class Problem057 : EulerProblem
  {
    public override int Number { get { return 57; } }

    public override object Solve()
    {
      /*
       * 3 7 17 41 99 239 577 1393
       * - - -- -- -- --- --- ----
       * 2 5 12 29 70 169 408 0985
       * 
       */

      int result = 0;
      int expansions = 1;

      BigInteger numCurrent = 3;
      BigInteger denCurrent = 2;

      while (expansions <= 1000)
      {
        var denominator = numCurrent + denCurrent;
        var numerator = denominator + denCurrent;

        if (numerator.ToString().Length > denominator.ToString().Length)
        {
          ++result;
        }

        numCurrent = numerator;
        denCurrent = denominator;

        ++expansions;
      }

      return result;
    }
  }
}
