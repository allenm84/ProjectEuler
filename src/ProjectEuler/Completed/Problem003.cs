using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem003 : EulerProblem
  {
    private const long N = 600851475143;

    public override int Number
    {
      get { return 3; }
    }

    public override object Solve()
    {
      long result = 0;
      var sqrtN = (long)Math.Sqrt(N);

      for (var i = sqrtN; i > 0; --i)
      {
        if ((N % i) == 0 && i.IsPrime())
        {
          result = i;
          i = 0;
        }
      }

      return result;
    }
  }
}