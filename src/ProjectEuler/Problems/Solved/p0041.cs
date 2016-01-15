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
  public class p0041 : euler
  {
    public override void Run()
    {
      var set = "123456789";
      var len = set.Length;

      int max = 0;
      while (len > 0)
      {
        var matches = new List<int>();

        var perm = new Permutations<char>(set.Take(len).ToList());
        foreach (IEnumerable<char> digits in perm)
        {
          var n = Convert.ToInt32(string.Join("", digits));
          if (math.isPrime(n))
          {
            max = Math.Max(n, max);
          }
        }

        --len;
      }

      Console.WriteLine(max);
    }
  }
}
