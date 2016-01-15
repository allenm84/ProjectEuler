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
  public class p0040 : euler
  {
    public override void Run()
    {
      var indices = new[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };
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

      Console.WriteLine(result);
    }
  }
}
