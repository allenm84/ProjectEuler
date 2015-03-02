using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem121 : EulerProblem
  {
    public override int Number
    {
      get { return 121; }
    }

    public override object Solve()
    {
      return Math.Floor(1m / win(0, 15, 0));
    }

    private decimal win(int idx, int turns, int blue)
    {
      if (blue == turns / 2 + 1) { return 1; }
      if (idx == turns) { return 0; }

      var balls = idx + 2;
      return (win(idx + 1, turns, blue + 1) + (balls - 1) * win(idx + 1, turns, blue)) / balls;
    }
  }
}