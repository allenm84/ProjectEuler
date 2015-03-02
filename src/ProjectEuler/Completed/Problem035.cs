using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem035 : EulerProblem
  {
    public override int Number
    {
      get { return 35; }
    }

    public override object Solve()
    {
      var count = 13;
      for (var n = 98; n < 1000000; ++n)
      {
        // if the number is prime, and all the permutations of the number
        // are prime
        if (n.IsPrime())
        {
          var digits = n.ToString().ToList();
          var keepGoing = true;
          var allPrime = true;

          while (keepGoing)
          {
            // rotate the last digit through
            var last = digits.Last();
            digits.RemoveAt(digits.Count - 1);
            digits.Insert(0, last);

            // conver the new number to an integer and test to see if it's prime
            var m = Convert.ToInt32(string.Join("", digits));
            var prime = m.IsPrime();

            // we keep going if the number is prime and the two numbers are different
            keepGoing = prime && (m != n);
            allPrime &= prime;
          }

          // if all the numbers are prime, then increment the count
          if (allPrime)
          {
            ++count;
          }
        }
      }
      return count;
    }
  }
}