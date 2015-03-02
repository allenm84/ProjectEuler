using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;

namespace ProjectEuler
{
  public class Problem019 : EulerProblem
  {
    public override int Number { get { return 19; } }

    public override object Solve()
    {
      // 1 Jan 1901 to 31 Dec 2000
      int count = 0;
      DateTime start = DateTime.Parse("1/1/1901");
      DateTime end = DateTime.Parse("12/31/2000");
      for (var i = start; i <= end; i = i.AddDays(1))
      {
        if (i.Day == 1 && i.DayOfWeek == DayOfWeek.Sunday)
        {
          ++count;
        }
      }
      return count;
    }
  }
}
