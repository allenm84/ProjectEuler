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
  public class Problem143 : EulerProblem
  {
    private class Pair : IComparable<Pair>
    {
      public int first;
      public int second;

      public int CompareTo(Pair other)
      {
        int comparison = first.CompareTo(other.first);
        if (comparison == 0)
        {
          comparison = second.CompareTo(other.second);
        }
        return comparison;
      }

      public override string ToString()
      {
        return string.Format("({0},{1})", first, second);
      }
    }

    public override int Number { get { return 143; } }

    public override object Solve()
    {
      const int L = 120000;
      const int LSQ = 347;

      // create a make pairs functions
      Func<int, int, Pair> make_pair = (i, j) => new Pair { first = i, second = j };

      // Store pairs in here
      List<Pair> pairs = new List<Pair>();

      // Use the parameterization
      for (var u = 1; u < LSQ; u++)
      {
        for (var v = 1; v < u; v++)
        {
          if (MathHelper.GCD(u, v) != 1) continue;
          if ((u - v) % 3 == 0) continue;
          var a = 2 * u * v + v * v;
          var b = u * u - v * v;
          if (a + b > L) break;

          // From coprime pairs make composite pairs
          for (int k = 1; k * (a + b) < L; k++)
          {
            pairs.Add(make_pair(k * a, k * b));
            pairs.Add(make_pair(k * b, k * a));
          }
        }
      }

      // Sort pairs list
      pairs.Sort();

      // Create index
      int[] index = new int[L];
      for (int i = 0; i < L; i++) index[i] = -1;
      for (int i = 0; i < pairs.Count; i++)
      {
        if (index[pairs[i].first] == -1)
          index[pairs[i].first] = i;
      }

      // Which sums have been reached?
      bool[] sums = new bool[L];
      for (int i = 0; i < L; i++) sums[i] = false;

      // Iterate through all pairs
      for (int i = 0; i < pairs.Count; i++)
      {
        var a = pairs[i].first;
        var b = pairs[i].second;

        // Construct vectors for indices
        List<int> va = new List<int>();
        List<int> vb = new List<int>();

        // Fetch indices
        int ia = index[a];
        int ib = index[b];

        while (ia < pairs.Count)
        {
          var next = pairs[ia];
          if (next.first != a) break;
          va.Add(next.second);
          ia++;
        }

        while (ib < pairs.Count)
        {
          var next = pairs[ib];
          if (next.first != b) break;
          vb.Add(next.second);
          ib++;
        }

        // Find common objects between va and vb
        for (int ja = 0; ja < va.Count; ja++)
        {
          for (int jb = 0; jb < vb.Count; jb++)
          {
            if (va[ja] == vb[jb])
            {
              // Potential c found
              var c = va[ja];
              if (a + b + c < L) sums[a + b + c] = true;
            }
          }
        }
      }

      // Tally up sums
      BigInteger s = 0;
      for (int i = 0; i < L; i++)
        if (sums[i]) s += i;
      return s;
    }

    private object v1Solution()
    {
      const int Limit = 120000;
      var sums = new HashSet<int>();

      // generate all pairs (x,y) under the limit (with x+y < 120000 and x^2 + xy + y^2 being a square)
      var pairs = new List<Pair>();
      for (int x = 1; x < Limit; ++x)
      {
        for (int y = x + 1; y < Limit; ++y)
        {
          if ((x + y) >= Limit) continue;

          long lx = x;
          long ly = y;
          long cand = (lx * lx) + (lx * ly) + (ly * ly);
          if (!MathHelper.IsPerfectSquare(cand)) continue;
          pairs.Add(new Pair { first = x, second = y });
        }
      }
      pairs.Sort();

      // for each pair (a,b) in the list, search the list to check if there exists some c where (a,b)
      // is a pair and (b,c) is a pair.
      for (int i = 0; i < pairs.Count; ++i)
      {
        var pair = pairs[i];
        for (int c = 1; true; ++c)
        {
          int s = pair.first + pair.second + c;
          if (s > Limit) break;

          Pair m = new Pair { first = pair.first, second = c };
          if (pairs.BinarySearch(m) < 0) continue;

          Pair n = new Pair { first = pair.second, second = c };
          if (pairs.BinarySearch(n) < 0) continue;

          // If an (a,b,c) triple is found and a+b+c <= 120000, then
          // mark a+b+c as found.
          sums.Add(s);
        }
      }

      // create a variable to hold the sum
      BigInteger sum = 0;
      foreach (var s in sums)
        sum += s;
      return sum;
    }
  }
}
