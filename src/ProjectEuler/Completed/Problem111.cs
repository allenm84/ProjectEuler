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
  public class Problem111 : EulerProblem
  {
    public override int Number { get { return 111; } }

    public override object Solve()
    {
      var num = new char[10];
      var indices = Enumerable.Range(0, num.Length).ToArray();

      BigInteger sum = 0;

      // create the repeated digit thing
      for (int n = 0; n < 10; ++n)
      {
        num.Fill((char)(n + 48));

        // determine how many digits to replace
        for (int d = 1; d < num.Length; ++d)
        {
          var end = int.Parse(string.Format("1{0}", string.Join("", Enumerable.Repeat('0', d))));
          var start = end / 10;

          var primes = new List<long>();

          var combinations = new Combinations<int>(indices, d);
          foreach (var combination in combinations)
          {
            var permutations = new Permutations<int>(combination);
            foreach (IList<int> permutation in permutations)
            {
              for (int i = start; i < end; ++i)
              {
                num.Fill((char)(n + 48));

                var text = i.ToString();
                for (int t = 0; t < permutation.Count; ++t)
                {
                  num[permutation[t]] = text[t];
                }

                if (num[0] == '0') continue;
                if (num.Same()) continue;

                var x = long.Parse(new string(num));
                if (x.IsPrime())
                {
                  primes.Add(x);
                }
              }
            }
          }

          if (primes.Count > 0)
          {
            sum += primes.Distinct().Sum();
            break;
          }
        }
      }

      return sum;
    }
  }
}
