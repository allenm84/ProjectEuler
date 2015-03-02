using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem187 : EulerProblem
  {
    public override int Number
    {
      get { return 187; }
    }

    public override object Solve()
    {
      const int max = 100000000;
      var sqrt = (int)Math.Sqrt(max) + 1;

      var index = 0;
      long count = 0;

      uint limit = max >> 1;
      var p = new Eratosthenes(limit).ToArray();

      uint target = 0;
      for (var i = 0; p[i] < sqrt; i++)
      {
        target = max / p[i];
        if ((target & 0x01) == 0)
        {
          target--;
        }
        while (true)
        {
          index = Array.BinarySearch(p, target);
          if (index > 0)
          {
            count += index - i + 1;
            break;
          }
          target -= 2;
        }
      }
      return count;
    }
  }
}