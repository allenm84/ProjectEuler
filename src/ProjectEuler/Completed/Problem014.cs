using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem014 : EulerProblem
  {
    public override int Number
    {
      get { return 14; }
    }

    public override object Solve()
    {
      var maxChainCount = 0;
      long startingNumber = 0;

      for (long i = 2; i < 1000000; ++i)
      {
        var chainCount = i.CollatzChain().Count();
        if (chainCount > maxChainCount)
        {
          startingNumber = i;
          maxChainCount = chainCount;
        }
      }

      return startingNumber;
    }
  }
}