using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem060 : EulerProblem
  {
    public override int Number
    {
      get { return 60; }
    }

    public override object Solve()
    {
      // here is what we know. Every prime in the set must be able to be concatenated
      // with the other primes in the set. So, we just need to add primes to a set until
      // we come up with 5 that satisfy the requirements
      const int Max = 10000;

      // create the set to hold the primes
      var set = new List<int>();

      // add 3 to the set. Since 2 appended to any number will make it divisble by 2!
      set.Add(3);

      // find the next prime
      var candidate = 5;

      // go through until we find a match
      while (true)
      {
        var matchFound = false;
        for (var p = candidate; !matchFound && p < Max; ++p)
        {
          if (p.IsPrime() && PropertyExists(ref set, ref p))
          {
            matchFound = true;
            set.Add(p);
            if (set.Count == 5)
            {
              return set.Sum();
            }
          }
        }

        // either way, we need the index of the last value
        var idx = set.Count - 1;

        // if we didn't find a prime that matches
        if (!matchFound)
        {
          // retrieve the last number
          var n = set[idx];
          set.RemoveAt(idx);

          // did we just remove the last number in the set?
          if (set.Count == 0)
          {
            // this means we ran out of primes! Find the next one
            while (!(++n).IsPrime()) { ; }

            // add the prime to the set
            set.Add(n);
          }

          // set out candidate to be +1 this prime
          candidate = (n + 1);
        }
        else
        {
          // our candidate is the next prime!
          candidate = (set[idx] + 1);
        }
      }
    }

    private bool PropertyExists(ref List<int> set, ref int p)
    {
      var valid = true;
      for (var i = 0; valid && i < set.Count; ++i)
      {
        var s = set[i];
        var p1 = Convert.ToInt32(string.Concat(s, p));

        valid = false;
        if (p1.IsPrime())
        {
          var p2 = Convert.ToInt32(string.Concat(p, s));
          if (p2.IsPrime())
          {
            valid = true;
          }
        }
      }
      return valid;
    }
  }
}