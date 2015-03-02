﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem005 : EulerProblem
  {
    public override int Number
    {
      get { return 5; }
    }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=5
      var numbers = Enumerable.Range(1, 20).ToArray();

      var result = 0;
      var i = 20;

      while (result == 0)
      {
        if (numbers.All(n => (i % n) == 0))
        {
          result = i;
        }
        ++i;
      }

      return result;
    }
  }
}