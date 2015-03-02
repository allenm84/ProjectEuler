﻿using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem049 : EulerProblem
  {
    public override int Number
    {
      get { return 49; }
    }

    public override object Solve()
    {
      var knownValues = new[] {1487, 4817, 8147};
      for (var i = 1000; i < 10000; ++i)
      {
        // if the current iterating value is not prime or is known, then skip
        if (!i.IsPrime() || knownValues.Contains(i)) { continue; }

        // create a list to hold the candidates
        var candidates = new List<int> {i};

        // otherwise, check the permutations
        var digits = i.GetDigits().ToList();
        var permutations = new Permutations<int>(digits)
          .Select(d => Convert.ToInt32(string.Join("", d)))
          .Where(p =>
            (i != p) && p.IsPrime() &&
              (999 < p) && (p < 10000) &&
              !knownValues.Contains(p));
        foreach (var n in permutations)
        {
          candidates.Add(n);
        }

        // if there are at least 3 candidates
        if (candidates.Count > 2)
        {
          // get a combination of three of the numbers
          var combination = new Combinations<int>(candidates, 3);
          foreach (var lst in combination)
          {
            // create a sorted list
            var sorted = lst.OrderBy(z => z).ToList();

            // retrieve the differences
            var diff1 = sorted[1] - sorted[0];
            var diff2 = sorted[2] - sorted[1];

            // if they're equal
            if (diff1 == diff2)
            {
              // we FOUND it
              return string.Join("", sorted);
            }
          }
        }
      }

      return "<None>";
    }
  }
}