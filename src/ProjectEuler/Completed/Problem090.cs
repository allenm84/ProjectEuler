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

namespace ProjectEuler
{
  public class Problem090 : EulerProblem
  {
    public override int Number { get { return 90; } }

    public override object Solve()
    {
      // basically what we want is to pick digits for the two cubes such that
      // choosing a digit from cube 1 and choosing a digit from cube 2 will
      // make a square number. In addition, the arrangement of the cubes needs to be
      // distinct

      // create an array to hold the digits
      int[] digits = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

      // now, we need to create the arrangements. Basically, given the digits
      // choose 6
      var cubes = new List<int[]>();
      for (int a = 0; a < digits.Length; ++a)
      {
        int d1 = digits[a];
        for (int b = a + 1; b < digits.Length; ++b)
        {
          int d2 = digits[b];
          for (int c = b + 1; c < digits.Length; ++c)
          {
            int d3 = digits[c];
            for (int d = c + 1; d < digits.Length; ++d)
            {
              int d4 = digits[d];
              for (int e = d + 1; e < digits.Length; ++e)
              {
                int d5 = digits[e];
                for (int f = e + 1; f < digits.Length; ++f)
                {
                  int d6 = digits[f];
                  cubes.Add(new int[6] { d1, d2, d3, d4, d5, d6 });
                }
              }
            }
          }
        }
      }

      // create a list to hold the arrangements
      var arrangements = new Dictionary<long, bool>();

      // now, we need to choose two arrangements
      for (int i = 0; i < cubes.Count; ++i)
      {
        var cube1 = cubes[i];
        for (int j = i + 1; j < cubes.Count; ++j)
        {
          var cube2 = cubes[j];

          // we now have two cubes. We need to see if these cubes satisfy
          // the requirements. Can we make all the square numbers choosing
          // choosing a side of the cube?

          // 01, 04, 09, 16, 25, 36, 49, 64, and 81
          var squares = new Dictionary<int, bool>
          {
            {1, false},
            {4, false},
            {9, false},
            {16, false},
            {25, false},
            {36, false},
            {49, false},
            {64, false},
            {81, false},
          };

          for (int m = 0; m < cube1.Length; ++m)
          {
            var c1 = cube1[m];
            var c1_2 =
              c1 == 6 ? 9 :
              c1 == 9 ? 6 : -10000;

            for (int n = 0; n < cube2.Length; ++n)
            {
              var c2 = cube2[n];
              var c2_1 =
                c2 == 6 ? 9 :
                c2 == 9 ? 6 : -10000;

              var values = new int[6];

              // add the values where c1 is the first digit
              values[0] = (c1 * 10) + c2;
              values[1] = (c1_2 * 10) + c2;
              values[2] = (c1 * 10) + c2_1;

              // add the values where c2 is the first digit
              values[3] = (c2 * 10) + c1;
              values[4] = (c2_1 * 10) + c1;
              values[5] = (c2 * 10) + c1_2;

              // check the values
              for (int v = 0; v < values.Length; ++v)
              {
                var value = values[v];
                if (value < 0) continue;

                if (squares.ContainsKey(value))
                {
                  squares[value] = true;
                }
              }
            }
          }

          // were all the squares formed
          if (squares.Values.All(b => b))
          {
            // create the arrangement
            var l = string.Join("", cube1
              .OrderBy(d => d)
              .Concat(cube2
              .OrderBy(d => d)));

            // set the arrangement
            var key = Convert.ToInt64(l);
            arrangements[key] = true;
          }
        }
      }

      return arrangements.Count;
    }
  }
}
