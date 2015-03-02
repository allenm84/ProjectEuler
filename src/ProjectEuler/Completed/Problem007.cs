using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem007 : EulerProblem
  {
    public override int Number { get { return 7; } }

    public override object Solve()
    {
      int n = 6;

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
