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

namespace ProjectEuler
{
  public class Problem072 : EulerProblem
  {
    public override int Number { get { return 72; } }

    public override object Solve()
    {
      long count = 0;
      for (int d = 2; d <= 1000000; ++d)
      {
        count += MathHelper.Phi(d);
      }
      return count;
    }
  }
}
