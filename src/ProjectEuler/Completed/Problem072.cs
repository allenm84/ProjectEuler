using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem072 : EulerProblem
  {
    public override int Number
    {
      get { return 72; }
    }

    public override object Solve()
    {
      long count = 0;
      for (var d = 2; d <= 1000000; ++d)
      {
        count += MathHelper.Phi(d);
      }
      return count;
    }
  }
}