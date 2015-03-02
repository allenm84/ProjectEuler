using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Data;

namespace ProjectEuler
{
  public class Problem113 : EulerProblem
  {
    public override int Number { get { return 113; } }

    public override object Solve()
    {
      /*
       * inkieminstrel:
       * I thought of this problem in terms of the number of ways to increase, decrease, or 
       * keep each successive digit the same. Across an increasing number, you can only 
       * increase the digit 8 times (since we don't have a leading 0). Across a decreasing 
       * number you can decrease up to 9 times. Additionally, for each digit in the number, 
       * you have a "keep the same" option. The answer is then just the number of ways you 
       * can increase or keep the same plus the number of ways you can decrease or keep the 
       * same. You have to be careful here, since keeping the entire number the same will 
       * be counted twice.
       */

      BigInteger count = 0;
      for (int i = 1; i < 101; ++i)
      {
        count += MathHelper.nCr(8 + i, i);
        count += MathHelper.nCr(9 + i, i);
        count -= 10;
      }

      return count;
    }
  }
}
