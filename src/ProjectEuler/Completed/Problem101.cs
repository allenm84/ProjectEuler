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
  public class Problem101 : EulerProblem
  {
    public override int Number { get { return 101; } }

    public override object Solve()
    {
      // store the first term (and we won't use it).
      var sequence = new List<BigInteger>(5000) { 1 };
     
      // generate the first 500 terms
      Generate(ref sequence, 50);

      // create a variable to hold the sequence so far
      var kSequence = new List<BigInteger>(5000);
      kSequence.Add(1);
      
      // keep track of the sum
      var sum = BigInteger.One;

      // go through the k values
      for (int k = 2; true; ++k)
      {
        // calculate the next value of k
        var next = k + 1;

        // generate more if needed
        if (next >= sequence.Count)
        {
          // generate another 500 terms
          Generate(ref sequence, 50);
        }

        // add the kth term in the sequence
        kSequence.Add(sequence[k]);

        // retrieve the differences
        var order = CalculateOrder(kSequence.ToArray());
        var numEquations = order + 1;

        // now, we have the order of the equation. If the order is 1,
        // then we know that the equation is of the form "ax + b". If the
        // order is 2, then we know that the equation is of the form "ax^2 + bx + c".
        // Now we need to generate N equations, where N = order.
        var matrix = new double[numEquations][];
        for (int n = 1; n <= numEquations; ++n)
        {
          var coefficients = new List<double>(order << 1);
          for (int p = order; p > 0; --p)
          {
            coefficients.Add(Math.Pow(n, p));
          }

          coefficients.Add(1);
          coefficients.Add((double)sequence[n]);

          matrix[n - 1] = coefficients.ToArray();
        }

        // solve the matrix to get the equation
        double[] x;
        MathHelper.GaussianSolve(SwitchToColumnRows(matrix), order + 1, out x);

        // now, we need to determine what the next term is
        var term = BigInteger.Zero;
        for (int i = 0; i < x.Length; ++i)
        {
          var pow = Math.Pow(next, x.Length - (i + 1));
          term += (BigInteger)(pow * Math.Round(x[i]));
        }

        // does this term match the next term?
        if (term == sequence[next])
        {
          break;
        }

        // otherwise, add the next
        sum += term;
      }
      return sum;
    }

    private double[][] SwitchToColumnRows(double[][] matrix)
    {
      int rows = matrix.Length;
      int cols = matrix[0].Length;

      double[][] retval = new double[cols][];
      for (int c = 0; c < cols; ++c)
      {
        retval[c] = new double[rows];
        for (int r = 0; r < rows; ++r)
        {
          retval[c][r] = matrix[r][c];
        }
      }

      return retval;
    }

    private int CalculateOrder(BigInteger[] sequence)
    {
      var values = sequence;
      var order = 1;

      while (true)
      {
        var diffs = new BigInteger[values.Length - 1];
        for (int i = 1; i < values.Length; ++i)
        {
          diffs[i - 1] = (values[i] - values[i - 1]);
        }

        var comp = diffs[0];
        var allSame = true;

        for (int i = 1; allSame && i < diffs.Length; ++i)
        {
          allSame &= (diffs[i] == comp);
        }

        if (allSame)
        {
          break;
        }
        else
        {
          values = diffs;
          ++order;
        }
      }

      return order;
    }

    private void Generate(ref List<BigInteger> sequence, int number)
    {
      int n = sequence.Count;
      int count = n + number;

      for (; n < count; ++n)
      {
        sequence.Add(Un(n));
      }
    }

    private BigInteger Un(BigInteger n)
    {
      BigInteger[] ns = new BigInteger[10];
      ns[0] = n;
      for (int i = 1; i < 10; ++i)
        ns[i] = ns[i - 1] * n;

      return 1 - ns[0] + ns[1] - ns[2] + ns[3] - ns[4] + ns[5] - ns[6] + ns[7] - ns[8] + ns[9];
    }
  }
}
