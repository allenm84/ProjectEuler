using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0007 : euler
  {
    public override void Run()
    {
      var n = 6;

      long i = 13;
      while (n < 10001)
      {
        if (math.isPrime(++i))
        {
          n++;
        }
      }

      Console.WriteLine(i);
    }
  }
}
