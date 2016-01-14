using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0024 : euler
  {
    public override void Run()
    {
      var digits = "0123456789";
      var perm = new Permutations<char>(digits.ToList());
      var values = perm
        .Select(p => Convert.ToInt64(string.Join("", p)))
        .OrderBy(i => i)
        .ToArray();
      Console.WriteLine(values[1000000 - 1]);
    }
  }
}
