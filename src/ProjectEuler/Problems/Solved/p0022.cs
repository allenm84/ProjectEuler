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
  public class p0022 : euler
  {
    public override void Run()
    {
      const int A = (int)'A';
      Console.WriteLine(Resources.p0022
        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => string.Join("", s.Where(c => char.IsLetter(c))))
        .OrderBy(s => s)
        .Select((s, i) => s.Sum(c => 1 + (((int)c) - A)) * (i + 1))
        .Sum());
    }
  }
}
