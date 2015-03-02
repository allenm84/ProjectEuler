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
  public class Problem032 : EulerProblem
  {
    public override int Number { get { return 32; } }

    public override object Solve()
    {
      var allDigits = "123456789".ToList();
      var table = new Dictionary<int, bool>();

      // a is the number of digits in the multiplicand.
      for (int a = 1; a < allDigits.Count; ++a)
      {
        // create a combinations object to choose the digits for the multiplicand
        var multiplicandDigitChooser = new Combinations<char>(allDigits, a);
        foreach (var multiplicandDigits in multiplicandDigitChooser)
        {
          // get the available digits for the multiplier
          var availableMultiplierDigits = allDigits.Except(multiplicandDigits).ToList();

          // now that we have the multiplicand digits, we need to generate permutations of the digits
          var multiplicandNumbers =
            new Permutations<char>(multiplicandDigits)
            .Select(ch => Convert.ToInt32(string.Join("", ch)))
            .ToArray();

          // b is the number of digits in the multiplier.
          for (int b = 1; b < availableMultiplierDigits.Count; ++b)
          {
            // create a combinations object to choose the digits for the multiplier
            var multiplierDigitChooser = new Combinations<char>(availableMultiplierDigits, b);
            foreach (var multiplierDigits in multiplierDigitChooser)
            {
              // now that we have the multiplier digits, we need to generate permutations of the digits
              var multiplierNumbers =
                new Permutations<char>(multiplierDigits)
                .Select(ch => Convert.ToInt32(string.Join("", ch)))
                .ToArray();

              // get the available digits for the product, and sort them
              var availableProductDigits = string.Join("", availableMultiplierDigits
                .Except(multiplierDigits)
                .OrderBy(c => c));

              // now, we need to multiply the two together and see if they match the available
              // product digits
              for (int i = 0; i < multiplicandNumbers.Length; ++i)
              {
                var multiplicand = multiplicandNumbers[i];
                for (int j = 0; j < multiplierNumbers.Length; ++j)
                {
                  var multiplier = multiplierNumbers[j];
                  var product = multiplicand * multiplier;
                  var text = string.Join("", product.ToString().OrderBy(c => c));
                  if (text.Equals(availableProductDigits))
                  {
                    // the multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital,
                    // so add the product to the table
                    table[product] = true;
                  }
                }
              }
            }
          }
        }
      }

      // return the sum of the values in the table
      return table.Keys.Sum();
    }
  }
}
