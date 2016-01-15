using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0003 : euler
  {
    const long N = 600851475143;

    public override void Run()
    {
      long result = 0;
      var sqrtN = (long)math.sqrt(N);

      for (var i = sqrtN; i > 0; --i)
      {
        if ((N % i) == 0 && math.isPrime(i))
        {
          result = i;
          i = 0;
        }
      }

      Console.WriteLine(result);
    }
  }
}
