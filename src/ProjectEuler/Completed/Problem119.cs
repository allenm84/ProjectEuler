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
using System.IO;
using System.Collections;

namespace ProjectEuler
{
  public class Problem119 : EulerProblem
  {
    public override int Number { get { return 119; } }

    public override object Solve()
    {
      var aValues = new List<BigInteger>();
      for (int n = 2; n <= 100; ++n)
      {
        for (int p = 2; p <= 10; ++p)
        {
          var value = BigInteger.Pow(n, p);
          var sum = value.ToString().Sum(c => c - 48);
          if (sum == n)
          {
            if (aValues.BinarySearch(value) < 0)
            {
              aValues.Add(value);
              aValues.Sort();
            }
          }
        }
      }

      return string.Join("\r\n",
        aValues.Select((v, i) => string.Format("a{0:00} = {1}", i + 1, v)));
    }
  }
}
