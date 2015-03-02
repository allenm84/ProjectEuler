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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem041 : EulerProblem
  {
    public override int Number { get { return 41; } }

    public override object Solve()
    {
      string set = "123456789";
      int len = set.Length;
      while (len > 0)
      {
        List<int> matches = new List<int>();

        var perm = new Permutations<char>(set.Take(len).ToList());
        foreach (IEnumerable<char> digits in perm)
        {
          int n = Convert.ToInt32(string.Join("", digits));
          if (n.IsPrime())
          {
            matches.Add(n);
          }
        }

        if (matches.Count > 0)
        {
          return matches.Max();
        }

        --len;
      }
      return "<NONE>";
    }
  }
}
