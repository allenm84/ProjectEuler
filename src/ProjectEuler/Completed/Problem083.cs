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
  public class Problem083 : Problem081
  {
    public override int Number { get { return 83; } }

    public override object Solve()
    {
      // read in the matrix of values
      var matrix = ReadMatrix(Resources.Problem081Data);

      // retrieve the row and column count
      var cols = matrix.GetLength(0);
      var rows = matrix.GetLength(1);

      // create the allowed moves
      var allowedMoves = Enum.GetValues(typeof(Movement)).Cast<Movement>().ToArray();

      // we're interested in moving from the top-left to the bottom right
      var start = new Index(0, 0);
      var end = new Index(cols - 1, rows - 1);

      // return the sum of the minimal path through the matrix
      return Dijkstra.GetPath(Dijkstra.Solve(matrix, allowedMoves, start), start, end)
        .Sum(n => n.Value);
    }
  }
}
