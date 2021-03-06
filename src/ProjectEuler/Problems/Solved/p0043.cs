﻿using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0043 : euler
  {
    public override void Run()
    {
      // create a running total
      ulong sum = 0;

      // create an array to hold the mods
      var mods = new[] { 2, 3, 5, 7, 11, 13, 17 };

      // create a set of the largest 0-9 pandigital number
      var set = "9876543210".ToList();

      // permutate the number to get all of the 0-9 pandigital numbers
      // that don't begin with '0'
      var perm = new Permutations<char>(set);

      // go through the numbers
      foreach (IList<char> digits in perm)
      {
        var allHoldProperty = true;
        var i = 3;
        var m = 0;
        while (allHoldProperty && i < digits.Count)
        {
          var text = string.Concat(digits[i - 2], digits[i - 1], digits[i]);
          var num = Convert.ToInt32(text);
          allHoldProperty &= (num % mods[m++]) == 0;
          ++i;
        }

        if (allHoldProperty)
        {
          sum += Convert.ToUInt64(string.Concat(digits));
        }
      }

      Console.WriteLine(sum);
    }
  }
}
