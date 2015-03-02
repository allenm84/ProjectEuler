using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem081 : EulerProblem
  {
    public override int Number
    {
      get { return 81; }
    }

    public override object Solve()
    {
      // read in the matrix of values
      var matrix = ReadMatrix(Resources.Problem081Data);

      // retrieve the row and column count
      var cols = matrix.GetLength(0);
      var rows = matrix.GetLength(1);

      // for this problem, we can only move right and now
      var allowedMoves = new[] {Movement.Right, Movement.Down};

      // we're interested in moving from the top-left to the bottom right
      var start = new Index(0, 0);
      var end = new Index(cols - 1, rows - 1);

      // return the sum of the minimal path through the matrix
      return Dijkstra.GetPath(Dijkstra.Solve(matrix, allowedMoves, start), start, end)
        .Sum(n => n.Value);
    }

    protected int[,] ReadMatrix(string data)
    {
      var lines = data.SplitBy('\r', '\n');
      var rows = lines.Length;
      var cols = lines[0].SplitBy(',').Length;

      var matrix = new int[cols, rows];
      for (var r = 0; r < rows; ++r)
      {
        var line = lines[r].SplitBy(',');
        for (var c = 0; c < cols; ++c)
        {
          matrix[c, r] = int.Parse(line[c]);
        }
      }

      return matrix;
    }
  }
}