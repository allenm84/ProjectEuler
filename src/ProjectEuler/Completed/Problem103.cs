using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem103 : EulerProblem
  {
    public override int Number
    {
      get { return 103; }
    }

    public override object Solve()
    {
      var n = new[] {11, 18, 19, 20, 22, 25};
      var optimum = new int[n.Length + 1];
      var mid = n[n.Length >> 1];
      optimum[0] = mid;
      for (var i = 1; i < optimum.Length; ++i)
      {
        optimum[i] = n[i - 1] + mid;
      }

      for (var d = -3; d <= 3; ++d)
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
      for (var c = set.Length - 1; c > 0; --c)
      {
        // create another combinations to generate the subsets
        var subsetsComb = new Combinations<int>(set, c);

        // go through the subsets
        foreach (var B in subsetsComb)
        {
          // create the subset
          var sum = 0;
          var arr = new int[B.Count];
          for (var i = 0; i < arr.Length; ++i)
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
      var trueForAll = true;

      // go through the subsets
      for (var i = 0; trueForAll && i < subsets.Length; ++i)
      {
        var B = subsets[i];
        for (var j = i + 1; trueForAll && j < subsets.Length; ++j)
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

    #region Nested type: SumSet

    protected class SumSet
    {
      public int Count;
      public HashSet<int> Set;
      public int Sum;

      public bool Overlaps(SumSet other)
      {
        return Set.Overlaps(other.Set);
      }
    }

    #endregion
  }
}