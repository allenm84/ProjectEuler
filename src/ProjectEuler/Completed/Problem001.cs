using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem001 : EulerProblem
  {
    public override int Number { get { return 1; } }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=1
      return Enumerable
        .Range(0, 1000)
        .Where(i => (i % 5) == 0 || (i % 3) == 0)
        .Sum();
    }
  }
}
