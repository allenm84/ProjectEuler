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
  public class Problem136 : Problem135
  {
    public override int Number { get { return 136; } }

    public override object Solve()
    {
      //return v1RetrieveSolutionCount(50000000, 1);
      return v2RetrieveSolutionCount(50000000, 1);
    }
  }
}
