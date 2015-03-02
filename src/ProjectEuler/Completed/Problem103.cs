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
using System.Data;

namespace ProjectEuler
{
  public class Problem103 : EulerProblem
  {
    protected class SumSet
    {
      public int Sum;
      public HashSet<int> Set;
      public int Count;

      public bool Overlaps(SumSet other)
      {
        return Set.Overlaps(other.Set);
      }
    }

    public override int Number { get { return 103; } }

    public override object Solve()
    {
      var n = new int[] { 11, 18, 19, 20, 22, 25 };
      var optimum = new int[n.Length + 1];
      var mid = n[n.Length >> 1];
      optimum[0] = mid;
      for (int i = 1; i < optimum.Length; ++i)
        optimum[i] = n[i - 1] + mid;

      for (int d = -3; d <= 3; ++d)
      {
        var set = optimum.Select(v => v + d).ToArray();
        if (IsSpecialSumSet(set))
        {
          return string.Join("", set);
        }
      }

      return "<NONE>";
    }

    protected IEnumerable<SumSet> GenerateSubsets(int[] set)
    {
      // cycle through the subset sizes
      for (int c = set.Length - 1; c > 0; --c)
      {
        // create another combinations to generate the subsets
        var subsetsComb = new Combinations<int>(set, c);

        // go through the subsets
        foreach (var B in subsetsComb)
        {
          // create the subset
          int sum = 0;
          int[] arr = new int[B.Count];
          for (int i = 0; i < arr.Length; ++i)
          {
            arr[i] = B[i];
            sum += B[i];
          }

          // add the subset
          yield return new SumSet
          {
            Count = arr.Length,
            Set = new HashSet<int>(arr),
            Sum = sum,
          };
        }
      }
    }

    protected bool IsSpecialSumSet(int[] set)
    {
       // retrieve the subsets
      var subsets = GenerateSubsets(set).ToArray();

      // create a boolean which holds true for each subset
      bool trueForAll = true;

      // go through the subsets
      for (int i = 0; trueForAll && i < subsets.Length; ++i)
      {
        var B = subsets[i];
        for (int j = i + 1; trueForAll && j < subsets.Length; ++j)
        {
          var C = subsets[j];
          trueForAll &= (B.Sum != C.Sum) &&
            ((B.Count > C.Count && B.Sum > C.Sum) ||
            (C.Count > B.Count && C.Sum > B.Sum) ||
            (B.Count == C.Count));
        }
      }

      // return the result
      return trueForAll;
    }
  }
}
