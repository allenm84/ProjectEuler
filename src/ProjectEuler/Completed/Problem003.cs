using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem003 : EulerProblem
  {
    const long N = 600851475143;

    public override int Number { get { return 3; } }

    public override object Solve()
    {
      long result = 0;
      long sqrtN = (long)Math.Sqrt(N);

      for (long i = sqrtN; i > 0; --i)
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
