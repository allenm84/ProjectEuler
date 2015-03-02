using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class NumberTheory
  {
    private Record[] table;

    public NumberTheory(uint maximum)
    {
      uint c = 0;
      table = new Record[maximum + 1];
      for (uint n = 2; n < table.Length; n++)
      {
        if (table[n] != null)
        {
          continue;
        }
        table[n] = new Record {Prime = n};
        for (uint m = 2; (c = n * m) < table.Length; m++)
        {
          if (table[c] == null)
          {
            table[c] = new Record {JumpTo = m, Prime = n};
          }
        }
      }
    }

    public bool IsPrime(uint N)
    {
      return !table[N].JumpTo.HasValue;
    }

    public void PrimesLessThan(uint value, Action<uint> actOnPrime)
    {
      for (uint n = 2; n < value; n++)
      {
        if (IsPrime(n))
        {
          actOnPrime(n);
        }
      }
    }

    public uint CountPrimes(uint n)
    {
      uint count = 0;
      PrimesLessThan(n, p => { count++; });
      return count;
    }

    public void PrimeFactorsOf(uint composite, Action<uint> actOnFactor)
    {
      var temp = table[composite];
      while (temp != null)
      {
        actOnFactor(temp.Prime);
        if (temp.JumpTo.HasValue)
        {
          temp = table[temp.JumpTo.Value];
        }
        else
        {
          temp = null;
        }
      }
    }

    public uint EulerTotient(uint n)
    {
      uint phi = 1, last = 0;
      PrimeFactorsOf(n, p =>
      {
        if (p != last)
        {
          phi *= p - 1;
          last = p;
        }
        else
        {
          phi *= p;
        }
      });
      return phi;
    }

    public uint DedekindPsi(uint n)
    {
      uint phi = 1, last = 0;
      PrimeFactorsOf(n, p =>
      {
        if (p != last)
        {
          phi *= p + 1;
          last = p;
        }
        else
        {
          phi *= p;
        }
      });
      return phi;
    }

    public double VonMangoldt(uint n)
    {
      uint P = 0;
      PrimeFactorsOf(n, p =>
      {
        if (P == 0)
        {
          P = p;
        }
        else if (P != p)
        {
          P = 1;
        }
      });
      return Math.Log(P);
    }

    public int MoebiusFunction(uint N)
    {
      if (N == 1)
      {
        return 1;
      }
      var distinct = true;
      uint last = 0, k = 0;
      PrimeFactorsOf(N, p =>
      {
        if (p == last)
        {
          distinct = false;
        }
        else
        {
          k++;
          last = p;
        }
      });

      if (distinct)
      {
        return ((k & 1) == 0) ? 1 : -1;
      }
      return 0;
    }

    public int Mertens(uint n)
    {
      var m = 0;
      for (uint k = 1; k <= n; k++)
      {
        m += MoebiusFunction(k);
      }
      return m;
    }

    public IEnumerable<uint> DivisorsOf(uint n)
    {
      var primes = new Dictionary<uint, uint>();
      PrimeFactorsOf(n, (pf =>
      {
        if (!primes.ContainsKey(pf)) { primes[pf] = 0; }
        primes[pf]++;
      }));

      if (primes.Count == 0)
      {
        return new uint[] {};
      }
      var divisors = new HashSet<uint>();
      var primeDivisors = new uint[primes.Count];
      var multiplicity = new uint[primes.Count];

      var i = 0;
      foreach (var kvp in primes)
      {
        primeDivisors[i] = kvp.Key;
        multiplicity[i] = kvp.Value;
        ++i;
      }

      findFactors(primeDivisors, multiplicity, 0, 1, divisors);
      return divisors;
    }

    private void findFactors(uint[] primes, uint[] mult, uint dv, uint cr, HashSet<uint> divisors)
    {
      if (dv == primes.Length)
      {
        // no more balls
        divisors.Add(cr);
        return;
      }

      // how many times will we take current divisor?
      // we have to try all options
      for (var i = 0; i <= mult[dv]; ++i)
      {
        findFactors(primes, mult, dv + 1, cr, divisors);
        cr *= primes[dv];
      }
    }

    #region Nested type: Record

    private class Record
    {
      public uint Prime { get; set; }
      public uint? JumpTo { get; set; }
    }

    #endregion
  }
}