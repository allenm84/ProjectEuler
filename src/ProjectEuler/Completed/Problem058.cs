using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem058 : EulerProblem
  {
    public override int Number
    {
      get { return 58; }
    }

    public override object Solve()
    {
      /*
       * 37 36 35 34 33 32 31
       * 38 17 16 15 14 13 30
       * 39 18  5  4  3 12 29
       * 40 19  6  1  2 11 28
       * 41 20  7  8  9 10 27
       * 42 21 22 23 24 25 26
       * 43 44 45 46 47 48 49
       */

      double primeCount = 8;
      double diagonalCount = 13;
      var sideLength = 7;

      // create a 
      var diagonals = new Diagonal[4];
      diagonals[0] = new Diagonal(20, 37);
      diagonals[1] = new Diagonal(18, 31);
      diagonals[2] = new Diagonal(24, 49);
      diagonals[3] = new Diagonal(22, 43);

      // the next number in the diagonal is +8 the increment. The next sideLength is +2
      // the next diagonalCount is +4
      while ((primeCount / diagonalCount) >= 0.1)
      {
        for (var i = 0; i < 4; ++i)
        {
          var diagonal = diagonals[i];
          diagonal.Incr += 8;
          diagonal.Value += diagonal.Incr;
          if (diagonal.Value.IsPrime())
          {
            ++primeCount;
          }
        }

        diagonalCount += 4;
        sideLength += 2;
      }

      return sideLength;
    }

    #region Nested type: Diagonal

    private class Diagonal
    {
      public int Incr;
      public int Value;

      public Diagonal(int incr, int value)
      {
        Incr = incr;
        Value = value;
      }
    }

    #endregion
  }
}