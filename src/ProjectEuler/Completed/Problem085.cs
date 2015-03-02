using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem085 : EulerProblem
  {
    public override int Number
    {
      get { return 85; }
    }

    public override object Solve()
    {
      BigInteger Max = 2000000;

      var minDiff = Max;
      var area = 0;

      for (var w = 2; w <= 100; ++w)
      {
        for (var h = 1; h < w; ++h)
        {
          var count = countRectangles(w, h);
          var diff = BigInteger.Abs(Max - count);
          if (diff < minDiff)
          {
            minDiff = diff;
            area = (w * h);
          }
        }
      }

      return area;
    }

    private BigInteger countRectangles(int width, int height)
    {
      return (MathHelper.nCr(width + 1, 2) * MathHelper.nCr(height + 1, 2));
    }
  }
}