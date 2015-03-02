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
  public class Problem147 : EulerProblem
  {
    public override int Number { get { return 147; } }

    public override object Solve()
    {
      //return new Solution1().GetSolution();
      return new Solution2().GetSolution();
    }

    /// <summary>MY original solution</summary>
    private class Solution1 : EulerSolution
    {
      public override object GetSolution()
      {
        const int maxC = 47;
        const int maxR = 43;

        long sum = 0;
        for (int rows = 1; rows <= maxR; ++rows)
        {
          for (int columns = 1; columns <= maxC; ++columns)
          {
            sum += (CountDiagonals(columns, rows) + CountRectangles(columns, rows));
          }
        }
        return sum;
      }

      private int CountDiagonals(int columns, int rows)
      {
        // create the return value
        int count = 0;

        // allocate the variables
        int r = 0, c = 0, downDistance = 0, i = 0, dx = 0, dy = 0;
        int minX = int.MaxValue, maxX = int.MinValue, minY = int.MaxValue, maxY = int.MinValue;
        Point previous = Point.Empty, last = Point.Empty, pt;
        bool all = true, fitAnywhere = false;

        // each square is made up of a 3x3 matrix of dots. Each dot can be connected
        // via diagonal lines: A 3x2 grid of squares would be made up like this:
        // * * * * * * *
        // * * * * * * *
        // * * * * * * *
        // * * * * * * *
        // * * * * * * *
        // As you can see, each square will share at least one side. This mean the total
        // number of dots will be (2c + 1) x (2r + 1) where c,r are the number of columns
        // and rows in the original grid (respectively). Using the example of a 3x2 grid,
        // we have 7x5, or 7 columns and 5 rows of dots
        bool[,] dots = new bool[(columns * 2) + 1, (rows * 2) + 1];

        // after we create the matrix of dots, we need to mark which ones can be used
        // and which ones can't.
        bool v = true;
        for (r = 0; r < dots.GetLength(1); ++r)
        {
          v = r % 2 == 0;
          for (c = 0; c < dots.GetLength(0); ++c, v = !v)
          {
            dots[c, r] = v;
          }
        }

        // now we have a matrix of dots, we need to determine how
        // many shapes can be used. We do this be iterating over the dots
        // the dots that can't be used, we skip

        // we start out going right and up
        var points = new List<Point>();
        points.Add(new Point(0, 0));
        points.Add(new Point(1, -1));

        // keep track of the up distance
        int upDistance = 1;

        while (true)
        {
          // keep track of the down distance
          downDistance = 0;

          var figure = new List<Point>(points);
          while (true)
          {
            // move down and right
            previous = figure.Last();
            previous = previous.Move(1, 1);
            figure.Add(previous);

            // update the down distance
            ++downDistance;

            // close the figure
            var rect = Close(figure, downDistance, upDistance);

            // now, we need to keep track if this figure fit ANY where
            fitAnywhere = false;

            // check to see if the diamong pattern exists
            for (r = 0; r < dots.GetLength(1); ++r)
            {
              for (c = 0; c < dots.GetLength(0); ++c)
              {
                if (!dots[c, r]) continue;

                // we can use this dot. Does the desired pattern exist?
                all = true;
                for (i = 0; all && i < rect.Count; ++i)
                {
                  // apply the pattern to the current location
                  pt = rect[i];
                  pt.X += c;
                  pt.Y += r;

                  // determine if the index is valid, and if we
                  // can use the dot specified by the index
                  all &=
                    (-1 < pt.X && pt.X < dots.GetLength(0)) &&
                    (-1 < pt.Y && pt.Y < dots.GetLength(1)) &&
                    dots[pt.X, pt.Y];
                }

                // if the desied pattern exists, then update the count
                if (all)
                {
                  ++count;
                  fitAnywhere = true;
                }
              }
            }

            // if the figure did not fit anywhere, then stop
            if (!fitAnywhere) break;
          }

          // now, we need to go up and right again
          last = points[points.Count - 1];
          last = last.Move(1, -1);

          // update the up distance
          ++upDistance;

          // retrieve the bounds of the points
          minX = int.MaxValue;
          maxX = int.MinValue;
          minY = int.MaxValue;
          maxY = int.MinValue;
          for (i = 0; i < points.Count; ++i)
          {
            minX = Math.Min(minX, points[i].X);
            minY = Math.Min(minY, points[i].Y);
            maxX = Math.Max(maxX, points[i].X);
            maxY = Math.Max(maxY, points[i].Y);
          }

          // did we just go outside the grid?
          dx = maxX - minX;
          dy = maxY - minY;
          if (dx > dots.GetLength(0) || dy > dots.GetLength(1)) break;

          // add to the points
          points.Add(last);
        }

        // return the count
        return count;
      }

      private List<Point> Close(List<Point> figure, int upDistance, int downDistance)
      {
        var retval = new List<Point>(figure);
        Point start = Point.Empty;

        // go the distance!
        while ((downDistance--) > 0)
        {
          // move down and left
          start = retval[retval.Count - 1];
          retval.Add(start.Move(-1, 1));
        }

        // go the distance!
        while ((upDistance--) > 0)
        {
          // move up and left
          start = retval[retval.Count - 1];
          retval.Add(start.Move(-1, -1));
        }

        return retval;
      }

      private int CountRectangles(int columns, int rows)
      {
        Rectangle grid = new Rectangle(0, 0, columns, rows), player = Rectangle.Empty;
        int count = 0, width = 0, height = 0;

        for (width = 1; width <= columns; ++width)
        {
          for (height = 1; height <= rows; ++height)
          {
            player.X = 0;
            player.Y = 0;
            player.Width = width;
            player.Height = height;

            while (true)
            {
              if (grid.Contains(player))
              {
                ++count;
                ++player.X;
              }
              else
              {
                player.X = 0;
                ++player.Y;
                if (!grid.Contains(player)) break;
              }
            }
          }
        }

        return count;
      }
    }

    /// <summary>solution from tien in forums</summary>
    private class Solution2 : EulerSolution
    {
      public override object GetSolution()
      {
        long sum = 0;
        for (int i = 1; i <= 47; i++)
          for (int j = 1; j <= 43; j++)
            sum += f(i, j);
        return sum;
      }

      private long f(long m, long n)
      {
        if (m < n) return f(n, m);
        long sum = m * (m + 1) * n * (n + 1) / 4;
        sum += ((m + m - n) * (n * n * 4 - 1) - 3) * n / 6;
        return sum;
      }
    }
  }
}
