using System;
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
  public class p0038 : euler
  {
    public override void Run()
    {
      var digits = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
      var text = string.Join("", digits);
      var result = 0;

      var factors = Enumerable
        .Range(2, digits.Length - 1)
        .Select(r => digits.Take(r).ToArray())
        .ToList();

      // we have to have at least (1,2).
      // so basically, n x (1,2) can not exceed 987654321
      // Lets say n is 1000. We would have:
      // 1000 + 2000 = 10002000, which isn't the same number
      // of digits. Lets say n is 5000, we would have:
      // 5000 + 10000 = 500010000, which is the same number of
      // digits. So basically, n must be less than 10000.
      for (var n = 1; n < 10000; ++n)
      {
        foreach (var fact in factors)
        {
          var sb = new StringBuilder();
          foreach (var f in fact)
          {
            sb.Append(n * f);
          }

          // create the string
          var t = sb.ToString();
          if (t.Length == text.Length && string.Join("", t.OrderBy(c => c)).Equals(text))
          {
            // create a number from the string
            result = Math.Max(result, Convert.ToInt32(t));
          }
        }
      }

      Console.WriteLine(result);
    }
  }
}
