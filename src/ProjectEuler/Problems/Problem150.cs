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
using System.Drawing;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem150 : EulerProblem
  {
    public override int Number { get { return 150; } }

    public override object Solve()
    {
      //return new Solution1().GetSolution();
      //return new Solution2().GetSolution();
      return new Solution3().GetSolution();
    }

    /// <summary>Mr original solution. Completes in ~01:46:40.0930152</summary>
    private class Solution1 : EulerSolution
    {
      class Binode
      {
        public Binode left, right;
        public long value;
        public int k;
        public Binode(long v, int i) { value = v; k = i; }
      }

      public override object GetSolution()
      {
        var tree = GetTree();
        //var tree = GetTestTree();
        long min = long.MaxValue, sum = 0;
        Binode b = null;
        HashSet<int> visited = new HashSet<int>();
        List<Binode> row = new List<Binode>(), nextRow = new List<Binode>();
        int i = 0, r = 0;

        for (i = 0; i < tree.Length; ++i)
        {
          // retrieve the element that we're currently iterating over
          b = tree[i];

          // reset
          sum = b.value;
          visited.Clear();

          // add the two below me
          row.AddIff(b.left);
          row.AddIff(b.right);

          // evaluate these
          while (row.Count > 0)
          {
            nextRow.Clear();
            for (r = 0; r < row.Count; ++r)
            {
              var item = row[r];
              if (!visited.Add(item.k)) continue;
              sum += item.value;

              // the next row is below me
              nextRow.AddIff(item.left);
              nextRow.AddIff(item.right);
            }

            // update the minimum
            min = Math.Min(sum, min);

            // transfer to the next row
            row.Clear();
            row.AddRange(nextRow);
          }
        }

        return min;
      }

      private void inittree(Binode[] sk)
      {
        int row = 1, values = 0, offset = 0, ldx = 0, rdx = 0;
        for (int i = 0; i < sk.Length; ++i)
        {
          ldx = offset + i + 1;
          if (ldx >= sk.Length) break;
          sk[i].left = sk[ldx];

          rdx = offset + i + 2;
          if (rdx >= sk.Length) break;
          sk[i].right = sk[rdx];

          ++values;
          if (values == row)
          {
            values = 0;
            ++row;
            ++offset;
          }
        }
      }

      private Binode[] GetTestTree()
      {
        var sk = new Binode[21];
        var i = 0;

        sk[i++] = new Binode(15, i);
        sk[i++] = new Binode(-14, i);
        sk[i++] = new Binode(-7, i);
        sk[i++] = new Binode(20, i);
        sk[i++] = new Binode(-13, i);
        sk[i++] = new Binode(-5, i);
        sk[i++] = new Binode(-3, i);
        sk[i++] = new Binode(8, i);
        sk[i++] = new Binode(23, i);
        sk[i++] = new Binode(-26, i);
        sk[i++] = new Binode(1, i);
        sk[i++] = new Binode(-4, i);
        sk[i++] = new Binode(-5, i);
        sk[i++] = new Binode(-18, i);
        sk[i++] = new Binode(5, i);
        sk[i++] = new Binode(-16, i);
        sk[i++] = new Binode(31, i);
        sk[i++] = new Binode(2, i);
        sk[i++] = new Binode(9, i);
        sk[i++] = new Binode(28, i);
        sk[i++] = new Binode(3, i);

        inittree(sk);
        return sk;
      }

      private Binode[] GetTree()
      {
        int mod = 2 << 19;
        int diff = 2 << 18;

        int k = 500500;
        var sk = new Binode[k];

        long t = 0;
        for (int i = 1; i <= k; ++i)
        {
          t = (615949L * t + 797807L) % mod;
          sk[i - 1] = new Binode(t - diff, i);
        }

        inittree(sk);
        return sk;
      }
    }

    /// <summary>Solution from stubbscroll</summary>
    private class Solution2 : EulerSolution
    {
      public override object GetSolution()
      {
        int[,] s = new int[1024, 1024];
        long[,] rs = new long[1024, 1024];

        int t = 0, k, x = 0, y = 0, i, j, w, M = 1000;
        long best = long.MaxValue, cur;

        for (k = 0; k < 500500; k++)
        {
          t = ((615949 * t + 797807 + (1 << 20)) % (1 << 20) + (1 << 20)) % (1 << 20);
          s[x++, y] = t - (1 << 19);
          if (x == y + 1) { x = 0; y++; }
        }
        for (j = 0; j < M; j++) for (rs[0, j] = i = 0; i <= j; i++) rs[i + 1, j] = rs[i, j] + s[i, j];
        for (j = 0; j < M; j++) for (i = 0; i <= j; i++)
          {
            for (cur = 0, w = 1, k = j; k < M; k++, w++)
            {
              cur += rs[i + w, k] - rs[i, k];
              if (cur < best) best = cur;
            }
          }

        return best;
      }
    }

    /// <summary>Solution from lomont</summary>
    private class Solution3 : EulerSolution
    {
      public override object GetSolution()
      {
        // we think in square arrays [i,j], 0<=i<N, 0<=j<=i
        // and map back to linear with [i,j]->(i-1)*i/2 + j
        // s[k] is the random numbers
        // r[k] is row sums: r[i,j] = sum[s[k,j],k=0 to i

        checked
        {
          int rows = 1000;
          int size = rows * (rows + 1) / 2;
          long t = 0;   // needs to be long to prevent overflow!
          int[] s = new int[size];
          int[] r = new int[size];

          for (int k = 0, i = 0, j = 0; k < size; ++k)
          { // initialize items
            t = (615949 * t + 797807) % (1 << 20);
            s[k] = (int)(t - (1 << 19));
            r[k] = s[k];

            if (k > 0)
            {
              j++;
              if (j > i)
              { // new row
                j = 0;
                i++;
              }
              else
                r[k] += r[k - 1];
            }
          }

          // iterate over solution
          int best = r[0]; // best value seen so far
          for (int i = 0; i < rows; ++i)
            for (int j = 0; j <= i; ++j)
            {
              int k = i * (i + 1) / 2 + j;
              int b = s[k]; // temp best value starting at (i,j)
              best = Math.Min(best, b);
              for (int h = 1; h + i < rows; ++h)
              {
                b += r[(i + h) * (i + h + 1) / 2 + j + h];
                if (j > 0)
                  b -= r[(i + h) * (i + h + 1) / 2 + j - 1];
                best = Math.Min(best, b);
              }
            }

          return best;
        }
      }
    }
  }
}
