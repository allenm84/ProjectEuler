using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem062 : EulerProblem
  {
    private const double OneThird = 0.333333333333333;

    public override int Number
    {
      get { return 62; }
    }

    public override object Solve()
    {
      // determine the maximum number we can cube
      var limit = (decimal)Math.Floor(Math.Pow((double)decimal.MaxValue, OneThird));

      // start out with 3-digit perfect cubes
      byte min = 3;

      // don't exceed that
      byte max = 4;

      // create the seed value
      decimal seed = 0;

      // create a list to hold the cubes
      var cubes = new List<decimal>();

      // while the seed is valid
      while ((++seed) <= limit)
      {
        var cube = (seed * seed * seed);
        var len = (byte)cube.ToString().Length;
        if (len < min) {}
        if (len >= max)
        {
          // this means the cube we generated is too big, 
          // so lets check the current cubes
          decimal result;
          if (ResultExists(cubes, out result))
          {
            return result;
          }
          // we didn't find the result
          cubes.Clear();

          // decrement the seed so we generate this number again
          --seed;

          // update the min and max
          min = max;
          ++max;
        }
        else
        {
          // this means that the length is greater than or equal to
          // min but less than max, so lets add the cube to the list
          cubes.Add(cube);
        }
      }

      return "<NONE>";
    }

    private bool ResultExists(List<decimal> cubes, out decimal result)
    {
      result = 0.0m;
      if (cubes.Count == 0) { return false; }

      // go through the cubes in the list and reduce them to their digits
      var groups = from v in
        (from c in cubes
          select new {Value = c, Digits = SortedString(c)})
        group v by v.Digits
        into dg
        select dg;

      // go through each group and retrieve the group with five (if there is any)
      foreach (var g in groups)
      {
        if (g.Count() == 5)
        {
          result = g.Min(a => a.Value);
          return true;
        }
      }

      // didn't find it
      return false;
    }

    private string SortedString(object obj)
    {
      return new string(obj.ToString().OrderBy(c => c).ToArray());
    }
  }
}