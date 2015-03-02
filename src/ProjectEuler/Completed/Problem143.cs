using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem143 : EulerProblem
  {
    public override int Number
    {
      get { return 143; }
    }

    public override object Solve()
    {
      const int L = 120000;
      const int LSQ = 347;

      // create a make pairs functions
      Func<int, int, Pair> make_pair = (i, j) => new Pair {first = i, second = j};

      // Store pairs in here
      var pairs = new List<Pair>();

      // Use the parameterization
      for (var u = 1; u < LSQ; u++)
      {
        for (var v = 1; v < u; v++)
        {
          if (MathHelper.GCD(u, v) != 1) { continue; }
          if ((u - v) % 3 == 0) { continue; }
          var a = 2 * u * v + v * v;
          var b = u * u - v * v;
          if (a + b > L) { break; }

          // From coprime pairs make composite pairs
          for (var k = 1; k * (a + b) < L; k++)
          {
            pairs.Add(make_pair(k * a, k * b));
            pairs.Add(make_pair(k * b, k * a));
          }
        }
      }

      // Sort pairs list
      pairs.Sort();

      // Create index
      var index = new int[L];
      for (var i = 0; i < L; i++) { index[i] = -1; }
      for (var i = 0; i < pairs.Count; i++)
      {
        if (index[pairs[i].first] == -1)
        {
          index[pairs[i].first] = i;
        }
      }

      // Which sums have been reached?
      var sums = new bool[L];
      for (var i = 0; i < L; i++) { sums[i] = false; }

      // Iterate through all pairs
      for (var i = 0; i < pairs.Count; i++)
      {
        var a = pairs[i].first;
        var b = pairs[i].second;

        // Construct vectors for indices
        var va = new List<int>();
        var vb = new List<int>();

        // Fetch indices
        var ia = index[a];
        var ib = index[b];

        while (ia < pairs.Count)
        {
          var next = pairs[ia];
          if (next.first != a) { break; }
          va.Add(next.second);
          ia++;
        }

        while (ib < pairs.Count)
        {
          var next = pairs[ib];
          if (next.first != b) { break; }
          vb.Add(next.second);
          ib++;
        }

        // Find common objects between va and vb
        for (var ja = 0; ja < va.Count; ja++)
        {
          for (var jb = 0; jb < vb.Count; jb++)
          {
            if (va[ja] == vb[jb])
            {
              // Potential c found
              var c = va[ja];
              if (a + b + c < L) { sums[a + b + c] = true; }
            }
          }
        }
      }

      // Tally up sums
      BigInteger s = 0;
      for (var i = 0; i < L; i++)
      {
        if (sums[i]) { s += i; }
      }
      return s;
    }

    private object v1Solution()
    {
      const int Limit = 120000;
      var sums = new HashSet<int>();

      // generate all pairs (x,y) under the limit (with x+y < 120000 and x^2 + xy + y^2 being a square)
      var pairs = new List<Pair>();
      for (var x = 1; x < Limit; ++x)
      {
        for (var y = x + 1; y < Limit; ++y)
        {
          if ((x + y) >= Limit) { continue; }

          long lx = x;
          long ly = y;
          var cand = (lx * lx) + (lx * ly) + (ly * ly);
          if (!MathHelper.IsPerfectSquare(cand)) { continue; }
          pairs.Add(new Pair {first = x, second = y});
        }
      }
      pairs.Sort();

      // for each pair (a,b) in the list, search the list to check if there exists some c where (a,b)
      // is a pair and (b,c) is a pair.
      for (var i = 0; i < pairs.Count; ++i)
      {
        var pair = pairs[i];
        for (var c = 1;; ++c)
        {
          var s = pair.first + pair.second + c;
          if (s > Limit) { break; }

          var m = new Pair {first = pair.first, second = c};
          if (pairs.BinarySearch(m) < 0) { continue; }

          var n = new Pair {first = pair.second, second = c};
          if (pairs.BinarySearch(n) < 0) { continue; }

          // If an (a,b,c) triple is found and a+b+c <= 120000, then
          // mark a+b+c as found.
          sums.Add(s);
        }
      }

      // create a variable to hold the sum
      BigInteger sum = 0;
      foreach (var s in sums)
      {
        sum += s;
      }
      return sum;
    }

    #region Nested type: Pair

    private class Pair : IComparable<Pair>
    {
      public int first;
      public int second;

      #region IComparable<Pair> Members

      public int CompareTo(Pair other)
      {
        var comparison = first.CompareTo(other.first);
        if (comparison == 0)
        {
          comparison = second.CompareTo(other.second);
        }
        return comparison;
      }

      #endregion

      public override string ToString()
      {
        return string.Format("({0},{1})", first, second);
      }
    }

    #endregion
  }
}