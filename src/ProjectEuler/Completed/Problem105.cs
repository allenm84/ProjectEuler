using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem105 : Problem103
  {
    public override int Number
    {
      get { return 105; }
    }

    public override object Solve()
    {
      return Resources.Problem105Data
        .SplitBy('\r', '\n')
        .Select(line => line
          .SplitBy(',')
          .Select(text => int.Parse(text))
          .ToArray())
        .Where(set => IsSpecialSumSet(set))
        .Sum(set => set.Sum());
    }
  }
}