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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem074 : EulerProblem
  {
    public override int Number { get { return 74; } }

    public override object Solve()
    {
      var facts = Enumerable
        .Range(0, 10)
        .Select(i => (int)MathHelper.Fact(i))
        .ToArray();

      // create a table to hold the chain lengths for the numbers
      var lengths = new Dictionary<int, int>();

      // add the chain lengths from the problem description
      lengths[145] = 1;
      lengths[169] = 3;
      lengths[363601] = 3;
      lengths[363600] = 3;
      lengths[1454] = 3;
      lengths[870] = 2;
      lengths[871] = 2;
      lengths[45361] = 2;
      lengths[45360] = 2;
      lengths[872] = 2;
      lengths[45362] = 2;
      lengths[69] = 5;
      lengths[78] = 4;
      lengths[540] = 4;
      lengths[541] = 4;
      lengths[4] = 8;
      lengths[24] = 7;
      lengths[26] = 6;
      lengths[722] = 5;
      lengths[5044] = 4;

      // go through the other numbers
      int count = 0;
      for (int N = 0; N < 1000000; ++N)
      {
        var chainCount = 1;
        var chain = new List<int>();
        chain.Add(N);

        var last = N;
        while (chainCount < 60)
        {
          int len;
          if (lengths.TryGetValue(last, out len))
          {
            chainCount += (len - 1);
            break;
          }

          last = last.GetDigits().Sum(i => facts[i]);
          if (!chain.Contains(last))
          {
            chain.Add(last);
            ++chainCount;
          }
          else
          {
            break;
          }
        }

        lengths[N] = chainCount;
        if (chainCount == 60)
        {
          ++count;
        }
      }

      return count;
    }
  }
}
