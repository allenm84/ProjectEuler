using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem151 : EulerProblem
  {
    public override int Number
    {
      get { return 151; }
    }

    public override object Solve()
    {
      return ExactSolution();
    }

    private object ExactSolution()
    {
      double prob = 0;
      Recur(1, 1, 1, 1, 1.0, 1, ref prob);
      return Math.Round(prob, 6);
    }

    private void Recur(int A2, int A3, int A4, int A5, double pr, int time, ref double prob)
    {
      if (A2 + A3 + A4 + A5 == 1) { prob += pr; }
      if (time < 14)
      {
        var pr1 = pr / (A2 + A3 + A4 + A5);
        if (A2 > 0) { Recur(A2 - 1, A3 + 1, A4 + 1, A5 + 1, pr1 * A2, time + 1, ref prob); }
        if (A3 > 0) { Recur(A2, A3 - 1, A4 + 1, A5 + 1, pr1 * A3, time + 1, ref prob); }
        if (A4 > 0) { Recur(A2, A3, A4 - 1, A5 + 1, pr1 * A4, time + 1, ref prob); }
        if (A5 > 0) { Recur(A2, A3, A4, A5 - 1, pr1 * A5, time + 1, ref prob); }
      }
    }

    private object MonteCarloSolution()
    {
      const long Weeks = 500000000000;
      var random = new Random();

      long sum = 0;
      for (long w = 0; w < Weeks; ++w)
      {
        sum += run(random);
      }

      return Math.Round(decimal.Divide(sum, Weeks), 6);
    }

    private int run(Random random)
    {
      const int A2 = 1;
      const int A3 = 2;
      const int A4 = 3;
      const int A5 = 4;

      // start with an A1, then cut in half:
      // A2-[A2]-> A3-[A3]-> A4-[A4]-> A5-A5
      // if we exclude the papers cut in half, we have:
      // {A2, A3, A4, A5, A5}. However, one of the A5 papers
      // is used for the first job of the week. So we have:
      // {A2, A3, A4, A5}.
      var envelope = new List<int>(20) {A2, A3, A4, A5};

      // initialize the variables
      int count = 0, i = 0, sheet, batch;

      // excluding the first (0) and last (15) batch of the week
      for (batch = 1; batch < 15; ++batch)
      {
        // ... he takes from the envelope one sheet of paper at random
        if (envelope.Count == 1)
        {
          ++count;
          i = 0;
        }
        else
        {
          i = random.Next(envelope.Count);
        }
        sheet = envelope.Pop(i);

        switch (sheet)
        {
          case A2:
            envelope.Add(A3);
            goto case A3;
          case A3:
            envelope.Add(A4);
            goto case A4;
          case A4:
            envelope.Add(A5);
            break;
        }
      }

      return count;
    }
  }
}