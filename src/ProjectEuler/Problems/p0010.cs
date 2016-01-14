using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0010 : euler
  {
    public override void Run()
    {
      long sum = 17;
      for (int i = 8; i < 2000000; ++i)
      {
        if (math.isPrime(i))
        {
          sum += i;
        }
      }
      Console.WriteLine(sum);
    }
  }
}
