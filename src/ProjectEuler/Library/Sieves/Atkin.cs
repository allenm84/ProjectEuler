using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class Atkin : IEnumerable<uint>
  {
    private List<uint> primes;
    private uint limit;

    public Atkin(uint candidate)
    {
      limit = candidate;
      primes = new List<uint>();
      fill();
    }

    private void fill()
    {
      var isPrime = new bool[limit + 1];
      var sqrt = (uint)Math.Sqrt(limit) + 1;

      Parallel.For(1, sqrt, x =>
      {
        var xx = x * x;
        for (var y = 1L; y <= sqrt; y++)
        {
          var yy = y * y;
          var n = 4 * xx + yy;
          if (n <= limit && (n % 12 == 1 || n % 12 == 5))
            isPrime[n] ^= true;

          n = 3 * xx + yy;
          if (n <= limit && n % 12 == 7)
            isPrime[n] ^= true;

          n = 3 * xx - yy;
          if (x > y && n <= limit && n % 12 == 11)
            isPrime[n] ^= true;
        }
      });

      primes.Add(2);
      primes.Add(3);

      for (uint n = 5; n <= sqrt; n++)
      {
        if (isPrime[n])
        {
          primes.Add(n);
          var nn = n * n;
          for (var k = nn; k <= limit; k += nn)
            isPrime[k] = false;
        }
      }

      for (var n = sqrt + 1; n <= limit; n++)
        if (isPrime[n])
          primes.Add(n);
    }

    public IEnumerator<uint> GetEnumerator()
    {
      foreach (var p in primes)
        yield return p;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
