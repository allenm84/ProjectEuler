using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem099 : EulerProblem
  {
    public override int Number
    {
      get { return 99; }
    }

    public override object Solve()
    {
      var lst = Resources.Problem099Data
        .SplitBy('\r', '\n')
        .Select(l => l.SplitBy(','))
        .Select(arr => new BaseExponent(arr[0], arr[1]))
        .ToList();

      var max = lst[0];
      var line = 0;

      for (var i = 1; i < lst.Count; ++i)
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

    #region Nested type: BaseExponent

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

      #region IComparable<BaseExponent> Members

      public int CompareTo(BaseExponent other)
      {
        return value.CompareTo(other.value);
      }

      #endregion
    }

    #endregion
  }
}