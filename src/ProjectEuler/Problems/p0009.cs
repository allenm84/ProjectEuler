using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0009 : euler
  {
    public override void Run()
    {
      var squares = Enumerable.Range(0, 1000).Select(i => (i * i)).ToArray();
      long result = 0;

      for (var a = 1; a < 999; ++a)
      {
        var a2 = squares[a];
        for (var b = a + 1; b < 999; ++b)
        {
          var b2 = squares[b];
          for (var c = b + 1; c < 999; ++c)
          {
            var c2 = squares[c];

            if ((a + b + c) != 1000) { continue; }
            if ((a2 + b2) != c2) { continue; }

            long al = a;
            long bl = b;
            long cl = c;
            result = al * bl * cl;
          }
        }
      }

      Console.WriteLine(result);
    }
  }
}
