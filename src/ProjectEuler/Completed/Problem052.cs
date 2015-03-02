using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem052 : EulerProblem
  {
    public override int Number
    {
      get { return 52; }
    }

    public override object Solve()
    {
      var x = 1;
      while (true)
      {
        var dx1 = new string(x.ToString().OrderBy(c => c).ToArray());

        var x2 = x + x;
        var dx2 = new string(x2.ToString().OrderBy(c => c).ToArray());
        if (dx2.Equals(dx1))
        {
          var x3 = x2 + x;
          var dx3 = new string(x3.ToString().OrderBy(c => c).ToArray());
          if (dx3.Equals(dx1))
          {
            var x4 = x3 + x;
            var dx4 = new string(x4.ToString().OrderBy(c => c).ToArray());
            if (dx4.Equals(dx1))
            {
              var x5 = x4 + x;
              var dx5 = new string(x5.ToString().OrderBy(c => c).ToArray());
              if (dx5.Equals(dx1))
              {
                var x6 = x5 + x;
                var dx6 = new string(x6.ToString().OrderBy(c => c).ToArray());
                if (dx6.Equals(dx1))
                {
                  break;
                }
              }
            }
          }
        }

        ++x;
        if (x < 0)
        {
          throw new OverflowException("x wrapped around");
        }
      }
      return x;
    }
  }
}