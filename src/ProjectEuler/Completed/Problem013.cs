using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;

namespace ProjectEuler
{
  public class Problem013 : EulerProblem
  {
    public override int Number { get { return 13; } }

    public override object Solve()
    {
      var numbers = Resources
        .Problem013Data
        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => BigInteger.Parse(s.Trim()));

      BigInteger sum = new BigInteger(0);
      foreach (var n in numbers)
      {
        sum += n;
      }

      return string.Join("", sum.ToString().Take(10));
    }
  }
}
