using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem010 : EulerProblem
  {
    public override int Number { get { return 10; } }

    public override object Solve()
    {
      long sum = 17;
      for (long i = 8; i < 2000000; ++i)
      {
        if (i.IsPrime())
          sum += i;
      }
      return sum;
    }
  }
}
