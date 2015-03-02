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

namespace ProjectEuler
{
  public class Problem026 : EulerProblem
  {
    public override int Number { get { return 26; } }

    public override object Solve()
    {
      int zeroes = 2048;
      BigInteger one = BigInteger.Parse("1".PadRight(zeroes, '0'));

      int max = 0;
      int result = 11;

      for (var d = 11; d < 1000; ++d)
      {
        // create a big integer for the iterating value
        BigInteger bd = new BigInteger(d);

        // retrieve the value
        BigInteger value = one / bd;

        // get the digits from the value
        var digits = value.ToString().ToArray();

        // add the first part of the digits
        List<char> sequence = new List<char>();
        sequence.Add(digits[0]);

        // create a variable letting us know that the sequence is repeating
        bool repeating = false;

        // go through the rest of the digits
        for (int i = 1; i < digits.Length; ++i)
        {
          char c = digits[i];
          bool add = true;
          if (sequence[0] == c)
          {
            // this means that the current digit is equal to the first digit in the
            // sequence. Lets say that the number is 0.6565656565 or 0.145231245 or
            // 0.1111111111111111111.
            add = false;

            if (sequence.Count > 1)
            {
              // this means that so far, there is more than one number in the sequence. We
              // now need to make sure that the other numbers after the digit match those
              // in the sequence.

              // first, create a string representing the current sequence
              string a = string.Join("", sequence);

              // next, create a string representing the sequence of digits
              string b = string.Join("", digits.Skip(i).Take(sequence.Count));
              if (string.Equals(a, b))
              {
                // this means that the current sequence is repeating, so just stop checking
                // the rest of the digits
                i = digits.Length;
                repeating = true;
              }
              else
              {
                // this means that altough the current digit is equal to the first number in the
                // sequence, the sequence (so far) isn't repeating, so just add the digit to the
                // sequence.
                add = true;
              }
            }
            else
            {
              // this means that so far, there is only one number in the sequence, and right now,
              // it's repeating over and over and over
              i = digits.Length;
              repeating = true;
            }
          }

          if(add)
          {
            sequence.Add(c);
          }
        }

        // if the sequence is repeating, then set the max
        if (repeating && sequence.Count > max)
        {
          max = sequence.Count;
          result = d;
        }
      }

      // return the maximum
      return result;
    }
  }
}
