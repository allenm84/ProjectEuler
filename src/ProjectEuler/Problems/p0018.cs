using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0018 : euler
  {
    const string Data =
@"75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

    public override void Run()
    {
      Console.WriteLine(MaxPathFromTree(Data));
    }

    protected long MaxPathFromTree(string data)
    {
      var tree = data
        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
        .Select(arr => arr.Select(s => Convert.ToInt64(s)).ToArray())
        .ToArray();

      for (var i = tree.Length - 2; i >= 0; i--)
      {
        var below = i + 1;
        for (var j = 0; j < tree[i].Length; j++)
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
