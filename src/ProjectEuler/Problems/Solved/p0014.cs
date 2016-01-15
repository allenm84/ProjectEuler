using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;
using System.Numerics;

namespace ProjectEuler
{
  public class p0014 : euler
  {
    public override void Run()
    {
      var maxChainCount = 0;
      long startingNumber = 0;

      for (long i = 2; i < 1000000; ++i)
      {
        var chainCount = ntheory.collatz(i).Count();
        if (chainCount > maxChainCount)
        {
          startingNumber = i;
          maxChainCount = chainCount;
        }
      }

      Console.WriteLine(startingNumber);
    }
  }
}
