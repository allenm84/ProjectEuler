using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem116 : EulerProblem
  {
    public override int Number
    {
      get { return 116; }
    }

    public override object Solve()
    {
      /*
       * A row of five black square tiles is to have a number of its tiles 
       * replaced with coloured oblong tiles chosen from red (length two), 
       * green (length three), or blue (length four).
       */
      const int Length = 50;
      BigInteger sum = 0;

      // create a function to memoize
      Func<int, int, BigInteger> count = null;
      count = (length, size) =>
      {
        if (length < size) { return 0; }

        // how many ways can we shift size through length?
        BigInteger retval = (length - size) + 1;

        // subtract the size
        length -= size;

        // now, we need to go through and take away length until we can't anymore
        while (length >= size)
        {
          retval += count(length, size);
          --length;
        }

        return retval;
      };
      count = count.Memoize();

      int[] sizes = {2, 3, 4};
      foreach (var size in sizes)
      {
        sum += count(Length, size);
      }

      return sum;
    }
  }
}