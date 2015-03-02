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

namespace ProjectEuler
{
  public class Problem082 : Problem081
  {
    public override int Number { get { return 82; } }

    public override object Solve()
    {
      // read in the matrix of values
      var matrix = ReadMatrix(Resources.Problem081Data);

      // retrieve the row and column count
      var cols = matrix.GetLength(0);
      var rows = matrix.GetLength(1);

      // go through the matrix starting in the right most column
      #region reduce matrix
      for (int c = cols - 2; c >= 0; --c)
      {
        // copy the values into an array
        int[] values = new int[rows];
        for (int r = 0; r < rows; ++r)
        {
          values[r] = matrix[c, r];
        }

        // go through the rows
        for (int r = rows - 1; r > -1; --r)
        {
          // retrieve the starting value. This is the value we're currently on
          var current = values[r];

          // create a list to hold the candidates
          var candidates = new List<int>();

          // add the right move
          candidates.Add(matrix[c + 1, r]);

          // basically, starting from where we are, we want to move
          // up and then right. So, move up 2, move right
          // move up 3, move right
          var totalUpCost = 0;
          for (int up = r - 1; up > -1; --up)
          {
            // add the up cost
            totalUpCost += values[up];

            // add to the cost of moving right
            candidates.Add(totalUpCost + matrix[c + 1, up]);
          }

          // basically, starting from where we are, we want to move
          // down and then right. So, move down 2, move right
          // move down 3, move right
          var totalDownCost = 0;
          for (int down = r + 1; down < rows; ++down)
          {
            // add the down cost
            totalDownCost += values[down];

            // add to the cost moving right
            candidates.Add(totalDownCost + matrix[c + 1, down]);
          }

          // set the current
          matrix[c, r] = current + candidates.Min();
        }
      }
      #endregion

      // find the min
      #region find the minimum row
      int minSum = int.MaxValue;
      int minRow = 0;
      for (int r = 0; r < rows; ++r)
      {
        int sum = matrix[0, r];
        if (sum < minSum)
        {
          minSum = sum;
          minRow = r;
        }
      }
      #endregion

      // return the min sum
      return minSum;
    }
  }
}
