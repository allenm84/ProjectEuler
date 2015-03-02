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

namespace ProjectEuler
{
  public class Problem022 : EulerProblem
  {
    public override int Number { get { return 22; } }

    public override object Solve()
    {
      const int A = (int)'A';
      return Resources.Problem022Data
        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => string.Join("", s.Where(c => char.IsLetter(c))))
        .OrderBy(s => s)
        .Select((s, i) => s.Sum(c => 1 + (((int)c) - A)) * (i + 1))
        .Sum();
    }
  }
}
