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
using System.Drawing;

namespace ProjectEuler
{
  public class Problem148 : EulerProblem
  {
    public override int Number { get { return 148; } }

    public override object Solve()
    {
      //return new Solution1().GetSolution();
      return new Solution2().GetSolution();
    }

    /// <summary>MY original solution</summary>
    private class Solution1 : EulerSolution
    {
      #region integer class
      class integer
      {
        private long value;
        private long count;
        private long incr;

        public long Value { get { return value; } }
        public long Count { get { return count; } }

        public integer(long initial)
        {
          value = initial;
          incr = 1;
          count = 0;
        }

        public void Incr()
        {
          Flash();
          ++count;
        }

        public void Reset(long incrAmt)
        {
          incr = incrAmt;
          count = 0;
          value = 0;
        }

        public void Flash()
        {
          value += incr;
        }
      }
      #endregion

      public override object GetSolution()
      {
        long sum = 0;
        int rows = 1000000000;
        //int rows = 100;
        int i = 0;

        var counter = new List<integer>();
        counter.Add(new integer(0));

        for (int r = 0; r < rows; ++r)
        {
          // update the value in the counter
          counter[0].Incr();

          // add on the current value in the counter
          sum += counter[0].Value;

          // if we've reached the 7 limit
          while (counter[i].Count == 7)
          {
            // go to the next value and increment it
            ++i;
            while (i >= counter.Count)
              counter.Add(new integer(1));
            counter[i].Incr();
          }

          // go backwards to reset
          for (; i > 0; --i)
          {
            if (counter[i].Value == 0) counter[i].Flash();
            counter[i - 1].Reset(counter[i].Value);
          }
        }

        return sum;
      }
    }

    /// <summary>Solution from quintana in forums</summary>
    private class Solution2 : EulerSolution
    {
      private long r(long height)
      {
        if (height <= 7) return (height + 1) * height / 2;
        long lines = 7, smallt = 28;
        while (lines * 7 < height)
        {
          lines *= 7;
          smallt *= 28;
        }
        smallt *= (height / lines) * (height / lines + 1) / 2;
        return smallt + (height / lines + 1) * r(height - height / lines * lines);
      }

      public override object GetSolution()
      {
        return r(1000000000);
      }
    }
  }
}
