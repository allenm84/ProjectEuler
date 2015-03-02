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
  public class Problem047 : EulerProblem
  {
    public override int Number { get { return 47; } }

    public override object Solve()
    {
      int N = 0;
      int i = 647;
      int distinctCount = 0;
      while (true)
      {
        // reset the count
        distinctCount = 0;

        // set N to be i
        N = i;

        // retrieve the factors
        bool distinct = true;
        Dictionary<int, bool> table = new Dictionary<int, bool>();
        do
        {
          // retrieve the prime factors of i
          var factors = (from f in (i++).PrimeFactors()
                         group f by f into fg
                         select fg.Key * fg.Count())
                         .ToArray();

          // if there aren't 4 factors, then continue on
          if (factors.Length < 4)
          {
            break;
          }

          // go through the factors and see if they've been added before
          foreach (var f in factors)
          {
            if (table.ContainsKey(f))
            {
              distinct = false;
              break;
            }
            table.Add(f, true);
          }

          // if distinct
          if (distinct)
          {
            // increment the count
            ++distinctCount;
          }
        }
        while (distinct);

        // if the count is 4, then stop
        if (distinctCount == 4)
        {
          break;
        }
      }

      return N;
    }
  }
}
