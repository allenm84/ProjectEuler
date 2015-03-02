﻿using System;
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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem117 : EulerProblem
  {
    public override int Number { get { return 117; } }

    public override object Solve()
    {
      const int Length = 50;

      Func<int, BigInteger> count = null;
      count = length =>
        {
          BigInteger retval = 0;
          for (int size = 2; size <= 4; ++size)
          {
            if (size > length) break;
            var remaining = length - size;

            retval += remaining + 1;
            retval += count(remaining);

            // take one of the blocks away until there isn't enough
            while ((--remaining) >= 2)
            {
              retval += count(remaining);
            }
          }
          return retval;
        };

      count = count.Memoize();
      return (count(Length) + 1);
    }
  }
}
