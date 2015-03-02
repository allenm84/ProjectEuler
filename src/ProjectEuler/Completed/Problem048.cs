﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem048 : EulerProblem
  {
    public override int Number
    {
      get { return 48; }
    }

    public override object Solve()
    {
      var sum = new BigInteger(0);
      for (short i = 1; i <= 1000; ++i)
      {
        sum += BigInteger.Pow(new BigInteger(i), i);
      }

      var text = sum.ToString();
      var start = text.Length - 10;

      var digits = new char[10];
      var d = 0;

      for (var i = start; i < text.Length; ++i)
      {
        digits[d++] = text[i];
      }

      return new string(digits);
    }
  }
}