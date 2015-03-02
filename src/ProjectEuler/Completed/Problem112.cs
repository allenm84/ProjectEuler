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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem112 : EulerProblem
  {
    public override int Number { get { return 112; } }

    public override object Solve()
    {
      long total = 100;
      long bouncy = 0;

      var ratio = 0L;
      for (int n = 100; true; ++n, ++total)
      {
        var digits = n.GetDigits();
        if (IsIncreasing(digits)) continue;
        if (IsDecreasing(digits)) continue;

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
      for (int i = 1; i < digits.Length; ++i)
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
      for (int i = 1; i < digits.Length; ++i)
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
