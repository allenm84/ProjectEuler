using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class Problem149 : EulerProblem
  {
    public override int Number
    {
      get { return 149; }
    }

    public override object Solve()
    {
      //return new Solution1().GetSolution();
      return new Solution2().GetSolution();
    }

    private static int[,] GetGrid()
    {
      var values = new int[4000000];
      Func<int, long> cube = x => x * x * x;

      // fill the first 55 elements
      Parallel.For(0, 55, v => values[v] =
        (int)(((100003L - 200003L * (v + 1) + 300007L * cube(v + 1)) % 1000000L) - 500000L));

      // fill the rest of the elements
      for (var i = 55; i < values.Length; ++i)
      {
        values[i] = ((values[i - 24] + values[i - 55] + 1000000) % 1000000) - 500000;
      }

      // translate to the grid
      var grid = new int[2000, 2000];
      int rCols = 0, idx = 0, c, r;
      for (r = 0; r < 2000; ++r)
      {
        rCols = r * 2000;
        for (c = 0; c < 2000; ++c)
        {
          idx = rCols + c;
          grid[c, r] = values[idx];
        }
      }

      values = null;
      return grid;
    }

    #region Nested type: Solution1

    /// <summary>My original solution (takes nearly an hour)</summary>
    private class Solution1 : EulerSolution
    {
      public override object GetSolution()
      {
        return findmax(GetGrid());
      }

      private BigInteger findmax(int[,] grid)
      {
        var getAdjacent = new Func<Point, Point>[8];

        // horizontal
        getAdjacent[0] = p => p.Move(-1, 0);
        getAdjacent[1] = p => p.Move(1, 0);

        // vertical
        getAdjacent[2] = p => p.Move(0, -1);
        getAdjacent[3] = p => p.Move(0, 1);

        // diagonal
        getAdjacent[4] = p => p.Move(1, -1);
        getAdjacent[5] = p => p.Move(-1, 1);

        // anti-diagonal
        getAdjacent[6] = p => p.Move(1, 1);
        getAdjacent[7] = p => p.Move(-1, -1);

        BigInteger max = int.MinValue;
        for (var i = 0; i < getAdjacent.Length; ++i)
        {
          max = BigInteger.Max(max, findmaxinternal(grid, getAdjacent[i]));
        }

        return max;
      }

      private BigInteger findmaxinternal(int[,] grid, Func<Point, Point> adjacent)
      {
        var columns = grid.GetLength(0);
        var rows = grid.GetLength(1);

        var pt = Point.Empty;
        BigInteger max = int.MinValue, sum = 0;

        for (var r = 0; r < rows; ++r)
        {
          for (var c = 0; c < columns; ++c)
          {
            pt.Y = r;
            pt.X = c;

            sum = 0;
            while (isvalid(ref pt, ref columns, ref rows))
            {
              sum += grid[pt.X, pt.Y];
              pt = adjacent(pt);
            }

            max = BigInteger.Max(sum, max);
          }
        }

        return max;
      }

      private bool isvalid(ref Point pt, ref int columns, ref int rows)
      {
        return (-1 < pt.X && pt.X < columns) && (-1 < pt.Y && pt.Y < rows);
      }
    }

    #endregion

    #region Nested type: Solution2

    /// <summary>Solution from Jni in forums (takes &lt; 1 sec)</summary>
    private class Solution2 : EulerSolution
    {
      public override object GetSolution()
      {
        return CountHighest(GetGrid());
      }

      private long CountHighest(int[,] a)
      {
        long max = 0, Tsum, Nsum;
        for (var i = 0; i < 2000; i++)
        {
          Tsum = Nsum = 0;
          for (var j = 0; j < 2000; j++)
          {
            Nsum += a[i, j];
            Tsum += a[j, i];
            if (Nsum < a[i, j]) { Nsum = a[i, j]; }
            if (Tsum < a[j, i]) { Tsum = a[j, i]; }
            if (max < Nsum) { max = Nsum; }
            if (max < Tsum) { max = Tsum; }
          }
        }
        return max;
      }
    }

    #endregion
  }
}