using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Data;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem127 : EulerProblem
  {
    public override int Number { get { return 127; } }

    public override object Solve()
    {
      const int MaxC = 120000;
      int sum = 0;
      int count = 1;

      Seed(MaxC);
      int[] rad = new int[MaxC];
      for (int i = 0; i < MaxC; ++i)
      {
        rad[i] = DistinctPrimeFactors(i).Multiply();
      }
      CleanUp();

      for (int c = 1; c < MaxC; c++)
      {
        long radc = rad[c];
        if (radc < c)
        {
          int chalf = c / 2;
          for (int a = 1; a < chalf; a++)
          {
            int b = c - a;
            long radb = radc * rad[b];
            if (radb < b)
            {
              long rada = radb * rad[a];
              if (rada < c)
              {
                if (MathHelper.GCD(a, b) == 1)
                {
                  sum += c;
                  count++;
                }
              }
            }
          }
        }
      }

      return string.Format("sum = {0}, count = {1}", sum, count);
    }

    private static List<int> primes;
    private static HashSet<int> primeSet;

    private static void CleanUp()
    {
      if (primes != null)
      {
        primes.Clear();
        primes = null;
      }

      if (primeSet != null)
      {
        primeSet.Clear();
        primeSet = null;
      }
    }

    private static void Seed(int maximum)
    {
      CleanUp();

      primes = new List<int>(maximum);
      primeSet = new HashSet<int>();

      for (int i = 0; i < maximum; ++i)
      {
        if (!i.IsPrime()) continue;
        primes.Add(i);
        primeSet.Add(i);
      }
    }

    private static IEnumerable<int> DistinctPrimeFactors(int n)
    {
      if (primeSet.Contains(n))
      {
        yield return n;
      }
      else
      {
        int max = n >> 1;
        for (int i = 0; i < primes.Count; ++i)
        {
          int p = primes[i];
          if (p > max) break;
          if ((n % p) == 0) yield return p;
        }
      }
    }
  }
}
