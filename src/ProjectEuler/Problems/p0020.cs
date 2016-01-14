using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0020 : euler
  {
    public override void Run()
    {
      var prod = BigInteger.One;
      for (var i = 100; i > 0; --i)
      {
        var n = new BigInteger(i);
        prod *= n;
      }

      Console.WriteLine(prod
        .ToString()
        .Select(c => ((int)c) - 48)
        .Sum());
    }
  }
}
