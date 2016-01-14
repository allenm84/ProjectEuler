using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0001 : euler
  {
    public override void Run()
    {
      Console.WriteLine(Enumerable
        .Range(0, 1000)
        .Where(i => (i % 5) == 0 || (i % 3) == 0)
        .Sum());
    }
  }
}
