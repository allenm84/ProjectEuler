using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem096 : EulerProblem
  {
    public override int Number
    {
      get { return 96; }
    }

    public override object Solve()
    {
      var lines = Resources
        .Problem096Data
        .SplitBy('\r', '\n')
        .ToArray();

      var grids = new SudokuGrid[50];
      for (int i = 9, g = 0; i < lines.Length; i += 10, ++g)
      {
        grids[g] = new SudokuGrid(new[]
        {
          lines[i - 8], lines[i - 7], lines[i - 6],
          lines[i - 5], lines[i - 4], lines[i - 3],
          lines[i - 2], lines[i - 1], lines[i - 0]
        });
      }

      var sum = 0;
      foreach (var grid in grids)
      {
        grid.Solve();
        sum += int.Parse(string.Concat(grid[0, 0], grid[1, 0], grid[2, 0]));
      }

      return sum;
    }
  }
}