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
  public class Problem091 : EulerProblem
  {
    public override int Number { get { return 91; } }

    public override object Solve()
    {
      // holds the max k
      const int K = 50;

      // generate the values [0,K]
      var values = Enumerable.Range(0, K + 1).ToArray();

      // next, create the different points
      var points = new List<int[]>();
      for (int a = 0; a < values.Length; ++a)
      {
        int x = values[a];
        for (int b = 0; b < values.Length; ++b)
        {
          int y = values[b];
          if (x == 0 && y == 0) continue;
          points.Add(new int[2] { x, y });
        }
      }

      // keep track of the count
      int count = 0;

      // finally, go through and choose two of the points
      for (int i = 0; i < points.Count; ++i)
      {
        var P = points[i];
        for (int j = i + 1; j < points.Count; ++j)
        {
          var Q = points[j];
          if (IsRightTriangle(P, Q))
          {
            ++count;
          }
        }
      }

      // return the count
      return count;
    }

    private bool IsRightTriangle(int[] P, int[] Q)
    {
      int x1 = P[0];
      int y1 = P[1];

      int x2 = Q[0];
      int y2 = Q[1];

      int x3 = 0;
      int y3 = 0;

      var dx = new int[3];
      dx[0] = sqrd(x3 - x1);
      dx[1] = sqrd(x3 - x2);
      dx[2] = sqrd(x2 - x1);

      var dy = new int[3];
      dy[0] = sqrd(y3 - y1);
      dy[1] = sqrd(y3 - y2);
      dy[2] = sqrd(y2 - y1);

      var side1 = dx[0] + dy[0];
      var side2 = dx[1] + dy[1];
      var side3 = dx[2] + dy[2];

      int a, b, c;
      if (side1 > side2 && side1 > side3)
      {
        c = side1;
        a = side2;
        b = side3;
      }
      else if (side2 > side1 && side2 > side3)
      {
        b = side1;
        c = side2;
        a = side3;
      }
      else
      {
        a = side1;
        b = side2;
        c = side3;
      }

      return (a + b) == c;
    }

    private int sqrd(int p)
    {
      return p * p;
    }
  }
}
