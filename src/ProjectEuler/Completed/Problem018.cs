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

namespace ProjectEuler
{
  public class Problem018 : EulerProblem
  {
    public override int Number { get { return 18; } }

    public override object Solve()
    {
      return MaxPathFromTree(Resources.Problem018Data);
    }

    protected long MaxPathFromTree(string data)
    {
      var tree = data
        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
        .Select(arr => arr.Select(v => Convert.ToInt64(v)).ToArray())
        .ToArray();

      for (int i = tree.Length - 2; i >= 0; i--)
      {
        int below = i + 1;
        for (int j = 0; j < tree[i].Length; j++)
        {
          /// starting from the second to last row, reduce the tree.
          /// take each node and the maximum value from the set
          /// of two adjacent nodes. That value gets added to the current node.
          /// this is continued until the end. For example, consider the tree:
          ///
          ///     8
          ///    6 7
          ///   5 4 9
          ///  3 1 3 6
          ///  
          ///  Starting from the second to last row {5,4,9} add on the max values
          ///  for the row below it {3,1,3,6}. max(3,1) = 3. 5+3 = 8. max(1,3) = 3. 4+3 = 7.
          ///  max(3,6) = 6. 9+6 = 15. The new second to last row becomes {8, 7, 15}. Then, 
          ///  process is repeated using the row {6,7}. The result will be stored in the top
          ///  row.
          tree[i][j] = tree[i][j] + Math.Max(tree[below][j], tree[below][j + 1]);
        }
      }

      return tree[0][0];
    }
  }
}
