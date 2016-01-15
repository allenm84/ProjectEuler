using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0019 : euler
  {
    public override void Run()
    {
      // 1 Jan 1901 to 31 Dec 2000
      var count = 0;
      var start = DateTime.Parse("1/1/1901");
      var end = DateTime.Parse("12/31/2000");
      for (var i = start; i <= end; i = i.AddDays(1))
      {
        if (i.Day == 1 && i.DayOfWeek == DayOfWeek.Sunday)
        {
          ++count;
        }
      }
      Console.WriteLine(count);
    }
  }
}
