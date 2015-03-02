using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem050 : EulerProblem
  {
    public override int Number
    {
      get { return 50; }
    }

    public override object Solve()
    {
      // set the limit
      const int Limit = 1000000;

      // generate a list of primes
      var primes = Enumerable
        .Range(1, Limit)
        .Where(p => p.IsPrime())
        .ToList();

      // keep track of the count and the number
      var maxCount = 0;
      var thePrime = 0;

      // go through the numbers
      for (var i = 1000; i < Limit; ++i)
      {
        // if the number isn't prime, then continue
        if (!i.IsPrime()) { continue; }

        // then, starting at the first prime, add together until the
        // sum is bigger than the number
        var sum = 0;
        var p = 0;
        for (; sum < i && p < primes.Count; ++p)
        {
          sum += primes[p];
        }

        // is the sum equal to the prime?
        if (sum == i)
        {
          maxCount = p;
          thePrime = i;
        }
        else
        {
          // try and narrow in
          var front = p;
          var back = 0;
          var count = p;

          while (
            (back < primes.Count) && (front < primes.Count) &&
              (back < front) && (sum != i) && (count > 0) &&
              primes[front] < i)
          {
            var takeAway = false;
            if (sum > i)
            {
              takeAway = true;
              sum -= primes[back++];
              --count;
            }
            if (sum < i)
            {
              var newSum = sum + primes[front++];
              if (newSum < i && !takeAway)
              {
                sum = newSum;
                ++count;
              }
              else
              {
                break;
              }
            }
          }

          // if the sum is equal
          if (sum == i && count > maxCount)
          {
            maxCount = count;
            thePrime = i;
          }
        }
      }

      // return the prime
      return thePrime;
    }
  }
}