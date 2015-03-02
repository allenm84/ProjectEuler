using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem041 : EulerProblem
  {
    public override int Number
    {
      get { return 41; }
    }

    public override object Solve()
    {
      var set = "123456789";
      var len = set.Length;
      while (len > 0)
      {
        var matches = new List<int>();

        var perm = new Permutations<char>(set.Take(len).ToList());
        foreach (IEnumerable<char> digits in perm)
        {
          var n = Convert.ToInt32(string.Join("", digits));
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