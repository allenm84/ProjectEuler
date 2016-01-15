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
  public class p0016 : euler
  {
    public override void Run()
    {
      Console.WriteLine(BigInteger.Pow(2, 1000)
        .ToString()
        .Sum(c => ((int)c) - 48));
    }
  }
}
