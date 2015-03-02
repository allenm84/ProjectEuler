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
  public class Problem088 : EulerProblem
  {
    private class ProductSum
    {
      public int n;
      public int k;
    }

    const int MaxK = 12000;
    const int MaxN = MaxK << 1;

    public override int Number { get { return 88; } }

    public override object Solve()
    {
      // genereate a list of all the factors for a given number
      var table = new List<ProductSum>(MaxN);
      for (short n = 2; n <= MaxK; ++n)
      {
        var factors = new List<short>(MaxN);
        factors.Add(n);
        Multiply(n, n, ref factors, ref table);
      }

      // create an array to hold the k values
      var kValues = Enumerable
        .Repeat(0, MaxK + 1)
        .Select(i => new Dictionary<int, bool>())
        .ToArray();

      // go through the entries
      foreach (var entry in table)
      {
        int k = entry.k;
        int n = entry.n;
        kValues[k][n] = true;
      }

      // finally, choose the lowest value
      return kValues
        .Where(l => l.Count > 0)
        .Select(l => GetMin(l.Keys))
        .Distinct()
        .Sum();
    }

    private int GetMin(IEnumerable<int> sequence)
    {
      int min = int.MaxValue;
      foreach (var s in sequence)
      {
        if (s < min)
          min = s;
      }
      return min;
    }

    private void Multiply(int product, int sum, ref List<short> factors, ref List<ProductSum> table)
    {
      for (short n = 2; n <= MaxK; ++n)
      {
        // check the product.  There is no reason to continue if the product gets too big
        int productValue = product * n;
        if (productValue > MaxN) break;

        // check the sum. There is no reason to continue if the sum gets too big
        int sumValue = sum + n;
        if (sumValue > MaxN) break;

        // add the factor
        factors.Add(n);

        // if the sumValue is less than or equal to the productValue, then add
        if ((sumValue <= productValue))
        {
          int num = productValue;

          // the 'k' value is calculated by taking the number of factors (which we multiply together)
          // and adding on the number of ones needed.  For example, if we have the factors {3,2}, then
          // the product is 6. Meaning our N=6.  But the sum is 5.  Although we have 2 factors, we would
          // need one 1 in order for our sum to be 6. So, (2+1) = 3. Meaning, one of the values for k=3 is 6.
          int k = factors.Count + (num - sumValue);
          if (k <= MaxK)
          {
            table.Add(new ProductSum { k = k, n = num });
          }
        }

        // do the other multiplications
        Multiply(productValue, sumValue, ref factors, ref table);
        factors.RemoveAt(factors.Count - 1);
      }
    }
  }
}
