using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem079 : EulerProblem
  {
    public override int Number
    {
      get { return 79; }
    }

    public override object Solve()
    {
      var lines = Resources
        .Problem079Data
        .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
        .OrderBy(s => s)
        .Distinct()
        .ToArray();

      // get the distinct digits
      var digits = lines
        .SelectMany(s => s)
        .Distinct()
        .ToArray();

      // count how many distinct digits there are
      var count = digits.Length;

      // create an array that is count big
      var passcode = new char[count];

      // create a variable for what's needed
      var needed = 0;

      // while we still need values
      while (needed < passcode.Length)
      {
        // retrieve the known digits as an array
        var known = passcode.Take(needed).ToArray();

        // set the guessed value
        passcode[needed] = digits.Except(known).First();

        // if needed is passcode.Length-1, then we discovered the passcode
        if (needed == passcode.Length - 1) { break; }

        // go through the lines
        foreach (var line in lines)
        {
          // retrieve the current digit at the needed index
          var current = passcode[needed];

          // retrieve the index of the current digit
          var i = line.IndexOf(current);
          if (i < 0) {}
          if (i != 0)
          {
            // this means that the current digit isn't the first digit.
            // there is a digit before it. As long as the digit before it isn't
            // any of the known digit, we can set it as the needed digit.
            var candidate = line[i - 1];
            if (!known.Contains(candidate))
            {
              passcode[needed] = candidate;
            }
          }
        }

        // move to the next needed digit
        ++needed;
      }

      // return the pass code
      return string.Join("", passcode);
    }
  }
}