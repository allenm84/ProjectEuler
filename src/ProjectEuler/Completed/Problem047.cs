using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem047 : EulerProblem
  {
    public override int Number
    {
      get { return 47; }
    }

    public override object Solve()
    {
      var N = 0;
      var i = 647;
      var distinctCount = 0;
      while (true)
      {
        // reset the count
        distinctCount = 0;

        // set N to be i
        N = i;

        // retrieve the factors
        var distinct = true;
        var table = new Dictionary<int, bool>();
        do
        {
          // retrieve the prime factors of i
          var factors = (from f in (i++).PrimeFactors()
            group f by f
            into fg
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
        } while (distinct);

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