using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem008 : EulerProblem
  {
    public override int Number
    {
      get { return 8; }
    }

    public override object Solve()
    {
      // first, retrieve all of the digits
      var text = Resources.Problem008Data.Where(c => char.IsDigit(c)).ToArray();

      // next, break them up into groups of five
      var groups = from i in Enumerable.Range(0, 996)
        select string.Join("", text.Extract(i, 5).OrderByDescending(c => c));

      // order the groups by the largest number
      var max = groups.Max();

      // multiply the digits in the max together
      var prod = 1;
      for (var i = 0; i < 5; ++i)
      {
        var n = max[i] - 48;
        prod *= n;
      }

      // return the result
      return prod;
    }
  }
}