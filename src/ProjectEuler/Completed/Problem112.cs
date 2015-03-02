using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem112 : EulerProblem
  {
    public override int Number
    {
      get { return 112; }
    }

    public override object Solve()
    {
      long total = 100;
      long bouncy = 0;

      var ratio = 0L;
      for (var n = 100;; ++n, ++total)
      {
        var digits = n.GetDigits();
        if (IsIncreasing(digits)) { continue; }
        if (IsDecreasing(digits)) { continue; }

        ++bouncy;
        ratio = (bouncy * 100) / total;
        if (ratio == 99)
        {
          return n;
        }
      }
    }

    protected bool IsDecreasing(int[] digits)
    {
      for (var i = 1; i < digits.Length; ++i)
      {
        if (digits[i] > digits[i - 1])
        {
          return false;
        }
      }

      return true;
    }

    protected bool IsIncreasing(int[] digits)
    {
      for (var i = 1; i < digits.Length; ++i)
      {
        if (digits[i] < digits[i - 1])
        {
          return false;
        }
      }

      return true;
    }
  }
}