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
using System.Data;

namespace ProjectEuler
{
  using Line = System.Tuple<ProjectEuler.Vector2D, ProjectEuler.Vector2D>;

  public class Problem144 : EulerProblem
  {
    public override int Number { get { return 144; } }

    public override object Solve()
    {
      //return v1Solution();
      return v2Solution();
    }

    private object v2Solution()
    {
      // solution from D_R_Eadful in project euler forums

      Func<double, double> sqrt = Math.Sqrt;
      Func<double, double> abs = Math.Abs;
      Func<double, double> tan = Math.Tan;
      Func<double, double> atan = Math.Atan;

      double x0, x11, x12, y, k, b;
      k = (-9.6 - 10.1) / 1.4;
      b = 10.1;
      x11 = 0.0;
      y = 10.1;
      int counter = 0;
      do
      {
        x0 = x11;
        x11 = -(k * b - 2 * sqrt(100 + 25 * k * k - b * b)) / (4 + k * k);
        x12 = -(k * b + 2 * sqrt(100 + 25 * k * k - b * b)) / (4 + k * k);
        if (abs(x11 - x0) < 0.001) x11 = x12;
        y = k * x11 + b;
        k = -tan(atan(k) + 2 * atan(4 * x11 / y));
        b = y - k * x11;
        counter++;
      } while (!(y > 0 && x11 > -0.01 && x11 < 0.01));
      return counter - 1;
    }

    private object v1Solution()
    {
      // 4x^2 + y^2 = 100
      // (4x^2)/100 + (y^2)/100 = 1
      // x^2/25 + y^2/100 = 1
      // x^2/5^2 + y^2/10^2 = 1
      // a = 5, b = 10
      double width = 5, height = 10;
      List<Line> lines = new List<Line>();
      lines.Add(FromCoordinates(0.0, 10.1, 1.4, -9.6));

      int count = 0;
      bool done = false;

      for (; !done; ++count)
      {
        var next = Reflect(lines[lines.Count - 1], width, height);
        lines.Add(next);

        var pt2 = next.Item2;
        var left = -0.01;
        var right = 0.01;
        var y = 10.0;
        var epsilon = 0.001;
        if ((left - epsilon) <= pt2.X && pt2.X <= (right + epsilon) &&
          (y - epsilon) <= pt2.Y && pt2.Y <= (y + epsilon))
        {
          done = true;
        }
      }
      return count;
    }

    private Line FromSlope(double m, double b, double x1, double x2)
    {
      return FromCoordinates(x1, (m * x1) + b, x2, (m * x2) + b);
    }

    private Line FromCoordinates(double x1, double y1, double x2, double y2)
    {
      return FromPoints(new Vector2D(x1, y1), new Vector2D(x2, y2));
    }

    private Line FromPoints(Vector2D pt1, Vector2D pt2)
    {
      return new Line(pt1, pt2);
    }

    private Line Reflect(Line line, double width, double height)
    {
      // create the return value
      Line retval = null;

      // we know that the slope of the tangent line is -4x/y at any point (x,y).
      // so, we take the second point from the first line, and determine what m is
      var pt1 = line.Item2;
      var m = (-4.0 * pt1.X) / pt1.Y;
      var tangent = FromSlope(m, pt1.Y - (m * pt1.X), -50.0, 50.0);

      // rotate 90 degrees to get the normal
      var normal = tangent.Item2 - tangent.Item1;
      normal.Rotate(Math.PI / 2.0);
      normal.Normalize();

      // reflect the line to get the reflected line
      var direction = Vector2D.Reflect(line.Item2 - line.Item1, normal);
      var reflected = FromPoints(pt1 - direction * 10.0, pt1 + direction * 10.0);

      // now, we need to find the intersection of the ellipse and this line
      var points = GetIntersectionPoints(width, height, reflected);
      if (points.Length == 2)
      {
        var p1 = points[0];
        var p2 = points[1];

        var v1 = (p1 - pt1).LengthSquared();
        var v2 = (p2 - pt1).LengthSquared();

        if (v1 < v2)
        {
          // p1 and pt1 are the same
          retval = new Line(p1, p2);
        }
        else
        {
          // p1 and pt1 are not the same
          retval = new Line(p2, p1);
        }
      }

      // return the line
      return retval;
    }

    private Vector2D[] GetIntersectionPoints(double rx, double ry, Line line)
    {
      var center = Vector2D.Zero;
      var a1 = line.Item1;
      var a2 = line.Item2;

      var result = new List<Vector2D>();

      var origin = new Vector2D(a1.X, a1.Y);
      var dir = a2 - a1;

      var diff = origin - center;

      var mDir = new Vector2D(dir.X / (rx * rx), dir.Y / (ry * ry));
      var mDiff = new Vector2D(diff.X / (rx * rx), diff.Y / (ry * ry));

      var a = Vector2D.Dot(dir, mDir);
      var b = Vector2D.Dot(dir, mDiff);
      var c = Vector2D.Dot(diff, mDiff) - 1.0;

      var d = b * b - a * c;
      if (d < 0)
      {
        AddMessage("Outside");
      }
      else if (d > 0)
      {
        var root = Math.Sqrt(d);
        var t_a = (-b - root) / a;
        var t_b = (-b + root) / a;
        if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
        {
          if ((t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1))
          {
            AddMessage("Outside");
          }
          else
          {
            AddMessage("Inside");
          }
        }
        else
        {
          if (0 <= t_a && t_a <= 1)
          {
            result.Add(Vector2D.Lerp(a1, a2, t_a));
          }
          if (0 <= t_b && t_b <= 1)
          {
            result.Add(Vector2D.Lerp(a1, a2, t_b));
          }
        }
      }
      else
      {
        var t = -b / a;
        if (0 <= t && t <= 1)
        {
          result.Add(Vector2D.Lerp(a1, a2, t));
        }
        else
        {
          AddMessage("Outside");
        }
      }
      return result.ToArray();
    }
  }
}
