using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0031 : euler
  {
    public override void Run()
    {
      // 1a + 2b + 5c + 10d + 20e + 50f + 100g + 200h = 200
      int[] S = { 1, 2, 5, 10, 20, 50, 100, 200 };
      var n = 200;
      Console.WriteLine(math.coinChangeCount(n, S));
    }
  }
}
