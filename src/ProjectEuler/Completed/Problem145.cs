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
  public class Problem145 : EulerProblem
  {
    public override int Number { get { return 145; } }

    public override object Solve()
    {
      //const int Maximum = 1000;
      const int Maximum = 1000000000;

      int count = 0;
      for (int n = 1; n < Maximum; n += 2)
      {
        // skip numbers that will give leading 0, and skip 1,5, and 9 digit numbers
        if ((n % 10 == 0) ||
          (1 <= n && n < 10) || 
          (10000 <= n && n < 100000) ||
          (100000000 <= n && n < 1000000000)) continue;

        // next, make sure that the digits are all odd
        if (allodd(reverse(n) + n)) count += 2;
      }

      return count;
    }

    private int reverse(int n)
    {
      int m = 0;
      while (n > 0)
      {
        m *= 10;
        m += (n % 10);
        n /= 10;
      }
      return m;
    }

    private bool allodd(int n)
    {
      while (n > 0)
      {
        if (n % 2 == 0)
          return false;
        else
          n /= 10;
      }
      return true;
    }
  }
}
