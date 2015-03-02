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
  public class Problem067 : Problem018
  {
    public override int Number { get { return 67; } }

    public override object Solve()
    {
      // build the tree and return the max
      return MaxPathFromTree(Resources.Problem067Data);
    }
  }
}
