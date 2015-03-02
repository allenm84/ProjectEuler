using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem196 : EulerProblem
  {
    public override int Number
    {
      get { return 196; }
    }

    public override object Solve()
    {
      //return new Solution1().GetSolution();
      return new Solution2().GetSolution();
    }

    #region Nested type: Solution1

    private class Solution1 : EulerSolution
    {
      public override object GetSolution()
      {
        BigInteger a = S(5678027);
        BigInteger b = S(7208785);
        return a + b;
      }

      private bool IsPrime(long n)
      {
        return n.IsPrime();
      }

      private IEnumerable<int[]> GetNeighbors(int column, int row)
      {
        var mods = new[]
        {
          // above
          new[] {-1, -1},
          new[] {0, -1},
          new[] {1, -1},

          // same row
          new[] {-1, 0},
          new[] {1, 0},

          // below
          new[] {-1, 1},
          new[] {0, 1},
          new[] {1, 1}
        };

        var columns = row;

        int c = 0, r = 0;
        for (var m = 0; m < mods.Length; ++m)
        {
          // retrieve the index
          c = column + mods[m][0];
          r = row + mods[m][1];

          // if the index of the column is too far left, then continue
          if (c < 1) { continue; }

          // if the row is above me, and the column is too far right, then continue
          if (r == row - 1 && c > (columns - 1)) { continue; }

            // if the row is the same as me, and the column is too far right, then continue
          if (r == row && c > columns) { continue; }

            // if the row is below me, and the column is too far right, then continue
          if (r == row + 1 && c > (columns + 1)) { continue; }

          // return the index
          yield return new[] {c, r};
        }
      }

      private long S(int row)
      {
        var n = GetValue(1, row);
        var sum = 0L;

        for (var i = 1; i <= row; ++i, ++n)
        {
          if (IsPrime(n))
          {
            // lets go through our neigbors which are also prime
            var directNeighborsPrime = false;
            var neighborsNeighborsPrime = false;

            var neighborPrimeCount = 0;
            foreach (var idx in GetNeighbors(i, row))
            {
              // if the value at this index is prime
              if (IsPrime(GetValue(idx[0], idx[1])))
              {
                ++neighborPrimeCount;
                if (!neighborsNeighborsPrime)
                {
                  var primeNeighbors = GetNeighbors(idx[0], idx[1]).Where(a => IsPrime(GetValue(a[0], a[1])));
                  neighborsNeighborsPrime = primeNeighbors.Count() > 1;
                }
              }
            }
            directNeighborsPrime = neighborPrimeCount > 1;

            if (directNeighborsPrime || neighborsNeighborsPrime)
            {
              sum += n;
            }
          }
        }

        return sum;
      }

      private long GetValue(long column, long row)
      {
        return ((row * row) - row + (column << 1)) >> 1;
      }
    }

    #endregion

    #region Nested type: Solution2

    /// <summary>Solution from lg5293 in forums</summary>
    private class Solution2 : EulerSolution
    {
      private int idx;
      private bool[] isPrime;
      private int len;
      private int[] prime;
      private bool[][] sieve, vis;

      /// <summary>Solution from lg5293 in forums</summary>
      public override object GetSolution()
      {
        BigInteger a = calc(5678027);
        BigInteger b = calc(7208785);
        return a + b;
      }

      private long calc(int row)
      {
        len = row;
        generatePrimes();

        sieve = new bool[5][];
        vis = new bool[5][];

        for (var i = 0; i < 5; i++)
        {
          sieve[i] = new bool[row - 2 + i];
          vis[i] = new bool[row - 2 + i];
          for (var j = 0; j < sieve[i].Length; j++)
          {
            sieve[i][j] = true;
          }
        }

        for (var i = 2; i < len; i++)
        {
          if (isPrime[i])
          {
            var start = (i - (int)((((long)(row - 3) * (row - 2)) / 2 + 1) % i)) % i;
            for (var j = 0; j < 5; j++)
            {
              var k = start;
              for (; k < sieve[j].Length; k += i)
              {
                sieve[j][k] = false;
              }
              k -= i;
              start = i - (sieve[j].Length - k);
            }
          }
        }

        long sum = 0, off = (long)(row - 1) * (row) / 2 + 1;
        for (var i = 0; i < sieve[2].Length; i++)
        {
          if (sieve[2][i])
          {
            if (vis[2][i] || mark(2, i) >= 3)
            {
              sum += off + i;
            }
          }
        }

        return sum;
      }

      private byte mark(int row, int col)
      {
        if (row < 0 || row > 4 || col < 0 || col > sieve[row].Length - 1) { return 0; }
        if (vis[row][col]) { return 0; }
        if (!sieve[row][col]) { return 0; }
        vis[row][col] = true;
        byte count = 1;
        count += mark(row - 1, col);
        count += mark(row - 1, col - 1);
        count += mark(row - 1, col + 1);
        count += mark(row, col);
        count += mark(row, col - 1);
        count += mark(row, col + 1);
        count += mark(row + 1, col);
        count += mark(row + 1, col - 1);
        count += mark(row + 1, col + 1);
        return count;
      }

      private void generatePrimes()
      {
        isPrime = new bool[len + 1];
        prime = new int[len / 2];
        isPrime[2] = true;
        prime[idx++] = 2;
        idx = 0;
        for (var i = 3; i <= len; i += 2) { isPrime[i] = true; }
        for (var i = 3; i <= len; i += 2)
        {
          if (isPrime[i])
          {
            prime[idx++] = i;
            for (var j = i + i; j <= len; j += i)
            {
              isPrime[j] = false;
            }
          }
        }
      }
    }

    #endregion
  }
}