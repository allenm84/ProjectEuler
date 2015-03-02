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

namespace ProjectEuler
{
  public class Problem115 : EulerProblem
  {
    public override int Number { get { return 115; } }

    public override object Solve()
    {
      /*
       * A row measuring n units in length has red blocks with a minimum length of m units 
       * placed on it, such that any two red blocks (which are allowed to be different lengths) 
       * are separated by at least one black square.
       * 
       * Let the fill-count function, F(m, n), represent the number of ways that a row can be 
       * filled.
       */

      int oneMillion = 1000000;
      int m = 50;
      for (int n = 100; true; ++n)
      {
        var count = FillCount(m, n);
        if (count > oneMillion)
        {
          return n;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m">Red blocks with a minimum length of m units</param>
    /// <param name="n">A row measuring n units in length.</param>
    /// <returns></returns>
    protected int FillCount(int m, int n)
    {
      // a row measuring n units in length
      // has red blocks with a minimum of m units.
      int[] P = new int[n + 1];

      // every length has the empty solution
      for (int i = 0; i < P.Length; i++)
      {
        P[i] = 1;
      }

      // length of the row
      for (int length = m; length < P.Length; length++)
      {
        // length of first tile
        for (int t = m; t <= length; t++)
        {
          // amount of blocks left free from the left
          for (int start = 0; start <= length - m; start++)
          {
            // check if tile of length t fits in remaining space,
            // letting start blocks from left free
            if (length - t - start >= 0)
            {
              P[length] += 1;
            }

            // number of possibilities to put tiles in the remaining space,
            // letting 1 block next to tile of length t free
            if (length - t - start - 1 >= m)
            {
              P[length] += P[length - t - start - 1] - 1;
            }
          }
        }
      }
      return P[n];
    }
  }
}
