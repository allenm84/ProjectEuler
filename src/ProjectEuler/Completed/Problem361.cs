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
using System.Text.RegularExpressions;

namespace ProjectEuler
{
  public class Problem361 : EulerProblem
  {
    const int UlongSize = sizeof(ulong) * 8;
    const ulong MaxValue = ulong.MaxValue - 1;

    public override int Number { get { return 361; } }

    public override object Solve()
    {
      return "";
    }

    private void binary(ulong value, StringBuilder sb)
    {
      ulong remainder = value % 2;

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
        int n = t.Length;
        if (n % 2 == 0)
        {
          int i = n / 2;
          t.Append(t[i]);
        }
        else
        {
          int i = (n - 1) / 2;
          t.Append(t[i] == '1' ? '0' : '1');
        }
      }
    }
  }
}
