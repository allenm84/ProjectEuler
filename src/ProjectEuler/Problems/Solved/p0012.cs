using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0012 : euler
  {
    public override void Run()
    {
      var i = 11;
      var factorCount = 0;

      var result = 0;
      while (factorCount < 500)
      {
        var triangle = Enumerable.Range(1, i).Sum();
        factorCount = ntheory.divisors(triangle).Count();

        result = triangle;
        ++i;
      }

      Console.WriteLine(result);
    }
  }
}
