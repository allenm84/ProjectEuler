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
  public class Problem100 : EulerProblem
  {
    public override int Number { get { return 100; } }

    public override object Solve()
    {
      //a(n)=6*a(n-1)-a(n-2)-2
      //with a(0)=1, a(1)=3.

      StringBuilder sb = new StringBuilder();
      var an = new BigInteger[2];
      an[0] = 1;
      an[1] = 3;

      // this is (10^12)^2
      BigInteger maximum = BigInteger.Parse("1000000000000000000000000");

      // while we've yet to reach the maximum
      while (true)
      {
        var next = 6 * an[1] - an[0] - 2;
        an[0] = an[1];
        an[1] = next;

        sb.Append(next);
        sb.AppendLine();

        var denominator = next * (next - 1);
        var value = denominator * 2;

        if (value > maximum)
        {
          break;
        }
      }

      return sb;
    }
  }
}
