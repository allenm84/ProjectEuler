using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem012 : EulerProblem
  {
    public override int Number
    {
      get { return 12; }
    }

    public override object Solve()
    {
      var i = 11;
      var factorCount = 0;

      var result = 0;
      while (factorCount < 500)
      {
        var triangle = Enumerable.Range(1, i).Sum();
        factorCount = triangle.Factors().Count();

        result = triangle;
        ++i;
      }

      return result;
    }
  }
}