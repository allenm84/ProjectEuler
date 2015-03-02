using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem136 : Problem135
  {
    public override int Number
    {
      get { return 136; }
    }

    public override object Solve()
    {
      //return v1RetrieveSolutionCount(50000000, 1);
      return v2RetrieveSolutionCount(50000000, 1);
    }
  }
}