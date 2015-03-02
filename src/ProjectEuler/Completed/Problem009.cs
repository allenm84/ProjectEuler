using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class Problem009 : EulerProblem
  {
    public override int Number { get { return 9; } }

    public override object Solve()
    {
      var squares = Enumerable.Range(0, 1000).Select(i => (i * i)).ToArray();
      long result = 0;

      for (int a = 1; a < 999; ++a)
      {
        int a2 = squares[a];
        for (int b = a + 1; b < 999; ++b)
        {
          int b2 = squares[b];
          for (int c = b + 1; c < 999; ++c)
          {
            int c2 = squares[c];

            if ((a + b + c) != 1000) continue;
            if ((a2 + b2) != c2) continue;

            long al = a;
            long bl = b;
            long cl = c;
            result = al * bl * cl;
          }
        }
      }

      return result;
    }
  }
}
