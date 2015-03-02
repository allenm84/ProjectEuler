using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem102 : EulerProblem
  {
    public override int Number
    {
      get { return 102; }
    }

    public override object Solve()
    {
      var sum = 0;
      var triangles = Resources
        .Problem102Data
        .SplitBy('\r', '\n')
        .Select(l => l.SplitBy(',')
          .Select(c => int.Parse(c))
          .ToArray());

      var P = Vector2F.Zero;
      foreach (var triangle in triangles)
      {
        var A = new Vector2F(triangle[0], triangle[1]);
        var B = new Vector2F(triangle[2], triangle[3]);
        var C = new Vector2F(triangle[4], triangle[5]);

        // Compute vectors
        var v0 = C - A;
        var v1 = B - A;
        var v2 = P - A;

        // Compute dot products
        var dot00 = Vector2F.Dot(v0, v0);
        var dot01 = Vector2F.Dot(v0, v1);
        var dot02 = Vector2F.Dot(v0, v2);
        var dot11 = Vector2F.Dot(v1, v1);
        var dot12 = Vector2F.Dot(v1, v2);

        // Compute barycentric coordinates
        var invDenom = 1f / (dot00 * dot11 - dot01 * dot01);
        var u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        var v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        // Check if point is in triangle
        if ((u > 0) && (v > 0) && (u + v < 1))
        {
          ++sum;
        }
      }

      return sum;
    }
  }
}