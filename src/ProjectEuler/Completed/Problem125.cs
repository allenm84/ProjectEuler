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
using System.IO;
using System.Collections;
using System.Data;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem125 : EulerProblem
  {
    public override int Number { get { return 125; } }

    public override object Solve()
    {
      // 10^8 = 100000000
      const int Maximum = 100000000;

      // the sqrt of 10^8 is 10000
      const int MaximumSqrt = 10000;

      // so, starting at 1^2 generate until 10000^2
      var squares = new int[MaximumSqrt];
      for (int n = 1; n <= MaximumSqrt; ++n)
      {
        squares[n - 1] = n * n;
      }

      // create a set to hold the distinct numbers
      var palindromeNumbers = new HashSet<int>();

      // go through the squares
      for (int i = 0; i < squares.Length; ++i)
      {
        // sum the squares together looking for palindromes
        int sum = squares[i];
        for (int d = i + 1; sum < Maximum && d < squares.Length; ++d)
        {
          sum += squares[d];
          if (sum.IsPalindrome())
          {
            palindromeNumbers.Add(sum);
          }
        }
      }

      // take the sum of the palindrome numbers
      long retval = 0;
      foreach (var v in palindromeNumbers)
      {
        retval += v;
      }

      // return the result
      return retval;
    }
  }
}
