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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem142 : EulerProblem
  {
    public override int Number { get { return 142; } }

    public override object Solve()
    {
      //return v1Solution();
      return v2Solution();
    }

    private object v2Solution()
    {
      // from sinan
      const int MASK = 0xff, M = 1000;
      bool[] ab = new bool[MASK + 1];
      Func<int, bool> is_square = ss =>
      {
        if (!ab[ss & MASK])
          return false;
        return Math.Sqrt(ss).IsInteger();
      };

      int i = -1;
      while (++i <= MASK)
        ab[(i * i) & MASK] = true;

      int b = 0, a;
      while (true)
      {
        a = ++b;
        int bb = b * b;
        while (a < M)
        {
          a += 2;
          int aa = a * a;
          int x = (aa + bb) >> 1;
          int y = (aa - bb) >> 1;
          int c = (int)Math.Sqrt(x);
          while (true)
          {
            ++c;
            int z = c * c - x;
            if (y <= z) break;
            if (is_square(x - z) && is_square(y + z) && is_square(y - z))
            {
              return string.Format("{0} + {1} + {2} = {3}",
                x, y, z, x + y + z);
            }
          }
        }
      }
    }

    private object v1Solution()
    {
      Func<int, bool> ispfs = t => Math.Sqrt(t).IsInteger();
      for (int n = 1; n < int.MaxValue; ++n)
      {
        if (!ispfs(n)) continue;

        // we know that this value can be expressed as the sum of two squares, so lets get the pairs
        foreach (var pair in Pairs(n))
        {
          // we need to make sure that each value is greater than 0
          if (!pair.All(k => k > 0)) continue;
          if (pair[0] == pair[1]) continue;

          // now, these pairs of numbers are either x+y, x+z or y+z.
          // so, first, let's sort the values
          int x, y;
          if (pair[0] > pair[1]) { x = pair[0]; y = pair[1]; }
          else { x = pair[1]; y = pair[0]; }

          // next, let's subtract the two
          int diff = x - y;
          if (diff == 0) continue;
          if (!ispfs(diff)) continue;

          // the difference is ALSO a perfect square. Since there are three possibilities, we
          // need to explore each one. So first, lets go with x+y. This means we need to find z. z will
          // start with y-1.
          int m = FindZ(x, y, ispfs);
          if (m > 0) return string.Format("{0} + {1} + {2} = {3}", x, y, m, (x + y + m));

          // If we get here, then lets assume that we have x and z
          m = FindY(x, y, ispfs);
          if (m > 0) return string.Format("{0} + {1} + {2} = {3}", x, m, y, (x + y + m));

          // if we get here, let's just stop because x will have no boundary
        }
      }

      return -1;
    }

    private IEnumerable<int[]> Pairs(int n)
    {
      int max = n >> 1;
      for (int x = 1; x <= max; ++x)
      {
        yield return new int[] { n - x, x };
      }
    }

    private int FindY(int x, int z, Func<int, bool> ispfs)
    {
      for (int y = x - 1; y > z; --y)
      {
        if (new int[] { x + y, x - y, y + z, y - z }.All(ispfs))
        {
          return y;
        }
      }
      return 0;
    }

    private int FindZ(int x, int y, Func<int, bool> ispfs)
    {
      for (int z = y - 1; z > 0; --z)
      {
        if (new int[] { x + z, x - z, y + z, y - z }.All(ispfs))
        {
          return z;
        }
      }
      return 0;
    }
  }
}
