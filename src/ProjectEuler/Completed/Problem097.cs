using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem097 : EulerProblem
  {
    public override int Number
    {
      get { return 97; }
    }

    public override object Solve()
    {
      // create a variable to hold the result of the power
      var a = 2UL;

      // create a variable to backup the result
      var previous = 0UL;

      // the power to raise 2 to
      var power = 7830457;

      // go through the powers
      for (var i = 1; i < power; ++i)
      {
        previous = a;
        a += a;
        if (a < previous)
        {
          // this means that a overflowed. We need to get the last
          // 10 digits of a
          a = Last10(previous.ToString());

          // go backwards so we re-add the correct value of a
          --i;
        }
      }

      // we need to take this result and multiply it by 28433 and add 1
      BigInteger result = (Last10(a.ToString()) * 28433) + 1;

      // return the last 10 digits of the result
      return Last10(result.ToString());
    }

    private ulong Last10(string text)
    {
      var digits = new char[10];
      for (int i = text.Length - 1, d = 9; d > -1; --i, --d)
      {
        digits[d] = text[i];
      }
      return Convert.ToUInt64(new string(digits));
    }
  }
}