using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem007 : EulerProblem
  {
    public override int Number
    {
      get { return 7; }
    }

    public override object Solve()
    {
      var n = 6;

      long i = 13;
      while (n < 10001)
      {
        if ((++i).IsPrime())
        {
          n++;
        }
      }

      return i;
    }
  }
}