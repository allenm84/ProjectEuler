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
  public class Problem120 : EulerProblem
  {
    public override int Number { get { return 120; } }

    public override object Solve()
    {
      // using brute force, these values were generated:
      // 6, 8, 20, 24, 42, 48, 72, 80, 110, 120, 156, 168, 210, 224, 272, 288, 
      // 342, 360, 420, 440, 506, 528, 600, 624, 702, 728, 812, 840, 930, 960, 
      // 1056, 1088, 1190, 1224, 1332, 1368, 1482, 1520, 1640, 1680, 1806, 1848, 
      // 1980, 2024, 2162, 2208, 2352, 2400, 2550, 2600, 2756, 2808, 2970, 3024, 
      // 3192, 3248, 3422, 3480, 3660, 3720, 3906, 3968, 4160, 4224, 4422, 4488, 
      // 4554, 4760, 4970, 5040, 5110, 5328, 5550, 5624, 5698, 5928, 6162, 6240, 
      // 6318, 6560, 6806, 6888, 6970, 7224, 7482, 7568, 7654, 7920, 8190, 8280, 
      // 8370, 8648, 8930, 9024, 9118, 9408, 9702, 9800

      // there is a pattern formed when taking the differences between the first few values:
      // 2, 12, 4, 18, 6, 24, 8, 30, 10, 36, 12, 42, 14, etc...

      // as you can see, the odd term (1,3,5) when using 0-based indices, is just
      // the term AFTER it times 3. The even terms are just incremented by 2 each time

      int[] incrs = new int[] { 4, 12 };
      int i = 1;

      int r1 = 6;
      int r2 = 8;

      BigInteger sum = r1 + r2;

      int a = 4;
      while (a < 1000)
      {
        int rnext = r2 + incrs[i];
        sum += rnext;

        if (i == 1)
        {
          int next = incrs[0] + 2;
          incrs[i] = next * 3;
        }
        else
        {
          incrs[i] += 2;
        }
        i = 1 - i;

        r1 = r2;
        r2 = rnext;
        ++a;
      }

      return sum;
    }
  }
}
