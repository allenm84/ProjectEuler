using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0035 : euler
  {
    public override void Run()
    {
      var allPrime = true;
      var count = 13;

      for (var n = 98; n < 1000000; ++n)
      {
        // if the number is prime, and all the rotations of the number
        // are prime
        if (math.isPrime(n))
        {
          int m = n;
          allPrime = true;
          do
          {
            m = math.rotateShift(m, 1);
            allPrime &= math.isPrime(m);
          }
          while (allPrime && (m != n));

          // if all the numbers are prime, then increment the count
          if (allPrime)
          {
            ++count;
          }
        }
      }
      Console.WriteLine(count);
    }
  }
}
