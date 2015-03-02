using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem040 : EulerProblem
  {
    public override int Number
    {
      get { return 40; }
    }

    public override object Solve()
    {
      var indices = new[] {1, 10, 100, 1000, 10000, 100000, 1000000};
      var capacity = indices.Last() + 1;

      var sb = new StringBuilder(capacity);
      var i = 1;
      while (sb.Length < capacity)
      {
        sb.Append(i++);
      }

      var result = 1;
      foreach (var index in indices)
      {
        var j = index - 1;
        var c = sb[j];
        var d = c - 48;
        result *= d;
      }

      return result;
    }
  }
}