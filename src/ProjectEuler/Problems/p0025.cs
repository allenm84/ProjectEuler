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
  public class p0025 : euler
  {
    public override void Run()
    {
      var f1 = BigInteger.One;
      var f2 = BigInteger.One;
      var sum = BigInteger.Zero;

      var keepGoing = true;
      var n = 2;

      while (keepGoing)
      {
        sum = f1 + f2;
        f1 = f2;
        f2 = sum;
        ++n;

        var text = sum.ToString();
        if (text.Length == 1000)
        {
          keepGoing = false;
        }
      }

      Console.WriteLine(n);
    }
  }
}
