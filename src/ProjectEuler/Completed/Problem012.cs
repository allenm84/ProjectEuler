using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem012 : EulerProblem
  {
    public override int Number { get { return 12; } }

    public override object Solve()
    {
      int i = 11;
      int factorCount = 0;

      int result = 0;
      while (factorCount < 500)
      {
        int triangle = Enumerable.Range(1, i).Sum();
        factorCount = triangle.Factors().Count();

        result = triangle;
        ++i;
      }

      return result;
    }
  }
}
