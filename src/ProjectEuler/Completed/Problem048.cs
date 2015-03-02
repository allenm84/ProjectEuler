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

namespace ProjectEuler
{
  public class Problem048 : EulerProblem
  {
    public override int Number { get { return 48; } }

    public override object Solve()
    {
      BigInteger sum = new BigInteger(0);
      for (short i = 1; i <= 1000; ++i)
      {
        sum += BigInteger.Pow(new BigInteger(i), i);
      }

      var text = sum.ToString();
      int start = text.Length - 10;

      char[] digits = new char[10];
      int d = 0;

      for (int i = start; i < text.Length; ++i)
      {
        digits[d++] = text[i];
      }

      return new string(digits);
    }
  }
}
