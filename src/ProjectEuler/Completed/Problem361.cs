using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem361 : EulerProblem
  {
    private const int UlongSize = sizeof(ulong) * 8;
    private const ulong MaxValue = ulong.MaxValue - 1;

    public override int Number
    {
      get { return 361; }
    }

    public override object Solve()
    {
      return "";
    }

    private void binary(ulong value, StringBuilder sb)
    {
      var remainder = value % 2;

      sb.Insert(0, remainder);
      if (value >= 2)
      {
        binary(value >> 1, sb);
      }
    }

    private void ThueMorse(StringBuilder t, int maximum)
    {
      while (t.Length < maximum)
      {
        var n = t.Length;
        if (n % 2 == 0)
        {
          var i = n / 2;
          t.Append(t[i]);
        }
        else
        {
          var i = (n - 1) / 2;
          t.Append(t[i] == '1' ? '0' : '1');
        }
      }
    }
  }
}