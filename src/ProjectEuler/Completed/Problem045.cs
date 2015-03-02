using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem045 : EulerProblem
  {
    public override int Number
    {
      get { return 45; }
    }

    public override object Solve()
    {
      Func<long, long> Triangle = n => (n * (n + 1L)) / 2L;
      var t = 287;
      while (true)
      {
        var tri = Triangle(t);
        if (MathHelper.IsPentagonal(tri))
        {
          return tri;
        }
        t += 2;
      }
    }
  }
}