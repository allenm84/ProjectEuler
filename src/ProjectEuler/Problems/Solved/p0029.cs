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
  public class p0029 : euler
  {
    public override void Run()
    {
      int a, b;

      var values = new HashSet<BigInteger>();
      for (a = 2; a <= 100; ++a)
      {
        for (b = 2; b <= 100; ++b)
        {
          values.Add(BigInteger.Pow(a, b));
        }
      }

      Console.WriteLine(values.Count);
    }
  }
}
