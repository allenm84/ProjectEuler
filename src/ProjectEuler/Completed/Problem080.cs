using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem080 : EulerProblem
  {
    public override int Number
    {
      get { return 80; }
    }

    public override object Solve()
    {
      // create a variable to store the overall sum
      var sum = 0;

      // generate a table of squares
      var squares = new Dictionary<int, bool>();
      for (var n = 1; n <= 100; ++n)
      {
        squares[(n * n)] = true;
      }

      // go throuhg the numbers
      for (var n = 1; n <= 100; ++n)
      {
        // if this is a perfect square, then continue
        if (squares.ContainsKey(n)) { continue; }

        // create a list containing the digits
        var digits = n.ToString().ToList();
        if (digits.Count % 2 == 1)
        {
          digits.Insert(0, '0');
        }

        // while the decimal digit count is less than 100, keep
        // adding groups of 0
        digits.AddRange(Enumerable.Repeat('0', 204 - digits.Count));

        // create a variable to store the remainder
        var remainder = BigInteger.Zero;
        var root = BigInteger.Zero;

        // create a variable to iterate the digits
        var i = 0;
        while (i < digits.Count)
        {
          // starting on the left, bring down the left most
          // significant pair of digits not yet used and write
          // them to the right of the remainined of the previous
          // step. In other words, multiply the remainder by 100 
          // and add the two digits. This will be the current value c.
          var c = (remainder * 100);
          c += int.Parse(new string(new[]
          {digits[i], digits[i + 1]}));

          // update the digit selector
          i += 2;

          // Find p, y and x, as follows:

          // * Let p be the part of the root found so far, ignoring 
          //   any decimal point. (For the first step, p = 0).
          var p = root;

          // * Determine the greatest digit x such that:
          //   y = (20 * p + x) * x does not exceed c
          var x = 0;
          var y = (20 * p + x) * x;
          while (y < c)
          {
            ++x;
            var newY = (20 * p + x) * x;
            if (newY > c)
            {
              --x;
              break;
            }
            y = newY;
          }

          // Place the digit x as the next digit of the root, 
          // i.e., above the two digits of the square you 
          // just brought down. Thus the next p will be the 
          // old p times 10 plus x.
          root *= 10;
          root += x;

          // Subtract y from c to form a new remainder.
          remainder = c - y;
        }

        // calculate the sum of the string
        sum += root.ToString().Take(100).Sum(l => ((int)l) - 48);
      }

      return sum;
    }
  }
}