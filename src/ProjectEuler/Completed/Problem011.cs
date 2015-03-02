using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem011 : EulerProblem
  {
    public override int Number
    {
      get { return 11; }
    }

    public override object Solve()
    {
      var data = Resources.Problem011Data
        .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s.Split(' '))
        .ToArray();

      var matrix = new int[20, 20];
      for (var c = 0; c < 20; ++c)
      {
        for (var r = 0; r < 20; ++r)
        {
          matrix[c, r] = Convert.ToInt32(data[c][r]);
        }
      }

      var prod = 1;
      for (var c = 0; c < 20; ++c)
      {
        for (var r = 0; r < 20; ++r)
        {
          // create an array to hold the values
          var values = new int[5];

          // get the values when moving down
          values[0] = MultValues(20, 20, ref matrix, c, r, c, r + 1, c, r + 2, c, r + 3);

          // get the values when moving right
          values[1] = MultValues(20, 20, ref matrix, c, r, c + 1, r, c + 2, r, c + 3, r);

          // get the values when moving up and right
          values[2] = MultValues(20, 20, ref matrix, c, r, c + 1, r - 1, c + 2, r - 2, c + 3, r - 3);

          // get the values when moving down and right
          values[3] = MultValues(20, 20, ref matrix, c, r, c + 1, r + 1, c + 2, r + 2, c + 3, r + 3);

          // and of course, add the current value
          values[4] = prod;

          // retrieve the maximum
          prod = values.Max();
        }
      }
      return prod;
    }

    private int MultValues(int cols, int rows, ref int[,] matrix, params int[] indices)
    {
      var retval = 1;
      for (var i = 0; i < indices.Length; i += 2)
      {
        var c = indices[i];
        var r = indices[i + 1];
        if (-1 < c && c < cols && -1 < r && r < rows)
        {
          retval *= matrix[c, r];
        }
        else
        {
          retval = 1;
          i = indices.Length;
        }
      }
      return retval;
    }
  }
}