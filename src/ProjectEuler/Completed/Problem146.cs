using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class Problem146 : EulerProblem
  {
    public override int Number
    {
      get { return 146; }
    }

    public override object Solve()
    {
      return updateSolution();
      //return parallelSolution();
    }

    private object updateSolution()
    {
      // solution from radiant1

      const int MAX = 150000000;
      int[] GOOD_LIST = {1, 3, 7, 9, 13, 27};
      int[] PRIMES =
      {
        2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41,
        43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
        101, 103, 107, 109, 113, 127, 131, 137, 139, 149,
        151, 157, 163, 167, 173, 179, 181, 191, 193, 197,
        199
      };

      long[] start;
      long[] list = {0};
      var length = 1;
      long mod = 1;
      for (var i = 0; i < PRIMES.Length; i++)
      {
        var residue = new bool[PRIMES[i]];
        for (var j = 0; j <= PRIMES[i] / 2; j++) { residue[(j * j) % PRIMES[i]] = true; }
        for (var j = 0; j < GOOD_LIST.Length; j++)
        {
          var k = -GOOD_LIST[j] % PRIMES[i];
          if (k < 0) { k += PRIMES[i]; }
          residue[k] = false;
        }
        var newList = new long[length * PRIMES[i]];
        var newLength = 0;
        if (PRIMES[i] <= 23)
        {
          var newMod = mod * PRIMES[i];
          for (var j = 0; j < length; j++)
          {
            for (var k = list[j]; k < MAX && k < newMod; k += mod)
            {
              if (residue[(int)((k * k) % PRIMES[i])]) { newList[newLength++] = k; }
            }
          }
          mod = newMod;
        }
        else
        {
          for (var j = 0; j < length; j++)
          {
            var k = list[j];
            if (residue[(int)((k * k) % PRIMES[i])]) { newList[newLength++] = k; }
          }
        }
        list = newList;
        length = newLength;
      }
      start = new long[length];
      for (var i = 0; i < length; i++) { start[i] = list[i]; }

      Func<long, bool> goodTest = n =>
      {
        for (var i = 0; i < GOOD_LIST.Length; i++)
        {
          if (!(n + GOOD_LIST[i]).IsPrime()) { return false; }
        }
        return true;
      };

      long sum = 10;
      for (var i = 0; i < start.Length; i++)
      {
        var n2 = start[i] * start[i];
        if (!(n2 + 21).IsPrime() && goodTest(n2)) { sum += start[i]; }
      }

      return sum;
    }

    private object originalSolution()
    {
      const int Limit = 150000000;
      const int KLimit = Limit / 10;

      BigInteger sum = 0;
      var syncRoot = new object();

      Parallel.For(10, KLimit, k =>
      {
        var n = k * 10L;
        if (validate(ref n)) { return; }
        if (((n + 4) % 7) > 1) { return; }

        var n2 = n * n;
        if (validate(ref n2)) { return; }

        var values = new long[6];
        values[0] = n2 + 01;
        if (!values[0].IsPrime()) { return; }
        values[1] = n2 + 03;
        if (!values[1].IsPrime()) { return; }
        values[2] = n2 + 07;
        if (!values[2].IsPrime()) { return; }
        values[3] = n2 + 09;
        if (!values[3].IsPrime()) { return; }
        values[4] = n2 + 13;
        if (!values[4].IsPrime()) { return; }
        values[5] = n2 + 27;
        if (!values[5].IsPrime()) { return; }

        var consecutive = true;
        long x = 0, y = 0;

        for (var i = 1; consecutive && i < values.Length; ++i)
        {
          x = values[i - 1] + 1;
          y = values[i];
          for (; consecutive && x < y; ++x)
          {
            consecutive &= !x.IsPrime();
          }
        }

        if (consecutive)
        {
          lock (syncRoot)
          {
            sum += n;
          }
        }
      });

      return sum;
    }

    private bool validate(ref long n)
    {
      if ((n % 3) == 0) { return true; }
      if ((n % 7) == 0) { return true; }
      if ((n % 13) == 0) { return true; }
      return false;
    }
  }
}