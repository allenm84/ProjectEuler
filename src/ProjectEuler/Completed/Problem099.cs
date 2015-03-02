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
  public class Problem099 : EulerProblem
  {
    private class BaseExponent : IComparable<BaseExponent>
    {
      private int mBase;
      private int mExp;
      private double value;

      public BaseExponent(string b, string e)
      {
        mBase = int.Parse(b);
        mExp = int.Parse(e);
        value = mExp * Math.Log10(mBase);
      }

      public int CompareTo(BaseExponent other)
      {
        return value.CompareTo(other.value);
      }
    }

    public override int Number { get { return 99; } }

    public override object Solve()
    {
      var lst = Resources.Problem099Data
        .SplitBy('\r', '\n')
        .Select(l => l.SplitBy(','))
        .Select(arr => new BaseExponent(arr[0], arr[1]))
        .ToList();

      var max = lst[0];
      var line = 0;

      for (int i = 1; i < lst.Count; ++i)
      {
        var value = lst[i];
        if (value.CompareTo(max) > 0)
        {
          max = value;
          line = i;
        }
      }

      return (line + 1);
    }
  }
}
