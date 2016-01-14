using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0006 : euler
  {
    public override void Run()
    {
      long sumSquares = 0;
      long sum = 0;

      for (long i = 1; i < 101; ++i)
      {
        var square = (i * i);
        sumSquares += square;
        sum += i;
      }

      var squareSum = (sum * sum);
      Console.WriteLine(Math.Abs(sumSquares - squareSum));
    }
  }
}
