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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem093 : EulerProblem
  {
    public override int Number { get { return 93; } }

    public override object Solve()
    {
      // save the set
      var maxSet = new int[0];
      var max1toNCount = 0;

      // go through the abcd sets
      foreach (var abcd in GetSets())
      {
        // create a dictionary to hold the values
        var values = new Dictionary<int, bool>();

        // generate all the permutations of the set
        var permutations = new Permutations<int>(abcd);
        foreach (IList<int> set in permutations)
        {
          var evaluations = GetEvaluations(set[0], set[1], set[2], set[3]);
          foreach (var value in evaluations)
          {
            if (value.IsInteger() && value > 0)
            {
              values[(int)value] = true;
            }
          }
        }

        // if there were values generated
        if (values.Count > 0)
        {
          var results = values.Keys.ToList();
          results.Sort();

          if (results[0] == 1)
          {
            var count = 0;
            var current = 1;

            for (int i = 1; i < results.Count; ++i)
            {
              var n = results[i];
              if ((n - current) == 1)
              {
                current = n;
                ++count;
              }
              else
              {
                break;
              }
            }

            if (count > max1toNCount)
            {
              max1toNCount = count;
              maxSet = abcd;
            }
          }
        }
      }

      return string.Format("abcd = {0} with 1 to {1}", string.Join("", maxSet), max1toNCount);
    }

    private IEnumerable<int[]> GetSets()
    {
      var digits = "0123456789".Select(c => c - 48).ToArray();
      for (int i = 0; i < digits.Length; ++i)
      {
        var a = digits[i];
        for (int j = i + 1; j < digits.Length; ++j)
        {
          var b = digits[j];
          for (int k = j + 1; k < digits.Length; ++k)
          {
            var c = digits[k];
            for (int l = k + 1; l < digits.Length; ++l)
            {
              var d = digits[l];
              yield return new int[4] { a, b, c, d };
            }
          }
        }
      }
    }

    private IEnumerable<double> GetEvaluations(double a, double b, double c, double d)
    {
      // OP = {+,-,*,/}
      // We can generate this list of yields by taking the combination of every operation
      // and applying them to the combinations listed below:
      // 
      // a OP1 b OP2 c OP3 d
      // (a OP1 b) OP2 c OP3 d
      // a OP1 (b OP2 c) OP3 d
      // a OP1 b OP2 (c OP3 d)
      // (a OP1 b OP2 c) OP3 d
      // ((a OP1 b) OP2 c) OP3 d
      // (a OP1 (b OP2 c)) OP3 d
      // a OP1 (b OP2 c OP3 d)
      // a OP1 ((b OP2 c) OP3 d)
      // a OP1 (b OP2 (c OP3 d))
      //

      yield return a + b + c + d;
      yield return (a + b) + c + d;
      yield return a + (b + c) + d;
      yield return a + b + (c + d);
      yield return (a + b + c) + d;
      yield return ((a + b) + c) + d;
      yield return (a + (b + c)) + d;
      yield return a + (b + c + d);
      yield return a + ((b + c) + d);
      yield return a + (b + (c + d));
      yield return a + b + c - d;
      yield return (a + b) + c - d;
      yield return a + (b + c) - d;
      yield return a + b + (c - d);
      yield return (a + b + c) - d;
      yield return ((a + b) + c) - d;
      yield return (a + (b + c)) - d;
      yield return a + (b + c - d);
      yield return a + ((b + c) - d);
      yield return a + (b + (c - d));
      yield return a + b + c * d;
      yield return (a + b) + c * d;
      yield return a + (b + c) * d;
      yield return a + b + (c * d);
      yield return (a + b + c) * d;
      yield return ((a + b) + c) * d;
      yield return (a + (b + c)) * d;
      yield return a + (b + c * d);
      yield return a + ((b + c) * d);
      yield return a + (b + (c * d));
      yield return a + b + c / d;
      yield return (a + b) + c / d;
      yield return a + (b + c) / d;
      yield return a + b + (c / d);
      yield return (a + b + c) / d;
      yield return ((a + b) + c) / d;
      yield return (a + (b + c)) / d;
      yield return a + (b + c / d);
      yield return a + ((b + c) / d);
      yield return a + (b + (c / d));
      yield return a + b - c + d;
      yield return (a + b) - c + d;
      yield return a + (b - c) + d;
      yield return a + b - (c + d);
      yield return (a + b - c) + d;
      yield return ((a + b) - c) + d;
      yield return (a + (b - c)) + d;
      yield return a + (b - c + d);
      yield return a + ((b - c) + d);
      yield return a + (b - (c + d));
      yield return a + b - c - d;
      yield return (a + b) - c - d;
      yield return a + (b - c) - d;
      yield return a + b - (c - d);
      yield return (a + b - c) - d;
      yield return ((a + b) - c) - d;
      yield return (a + (b - c)) - d;
      yield return a + (b - c - d);
      yield return a + ((b - c) - d);
      yield return a + (b - (c - d));
      yield return a + b - c * d;
      yield return (a + b) - c * d;
      yield return a + (b - c) * d;
      yield return a + b - (c * d);
      yield return (a + b - c) * d;
      yield return ((a + b) - c) * d;
      yield return (a + (b - c)) * d;
      yield return a + (b - c * d);
      yield return a + ((b - c) * d);
      yield return a + (b - (c * d));
      yield return a + b - c / d;
      yield return (a + b) - c / d;
      yield return a + (b - c) / d;
      yield return a + b - (c / d);
      yield return (a + b - c) / d;
      yield return ((a + b) - c) / d;
      yield return (a + (b - c)) / d;
      yield return a + (b - c / d);
      yield return a + ((b - c) / d);
      yield return a + (b - (c / d));
      yield return a + b * c + d;
      yield return (a + b) * c + d;
      yield return a + (b * c) + d;
      yield return a + b * (c + d);
      yield return (a + b * c) + d;
      yield return ((a + b) * c) + d;
      yield return (a + (b * c)) + d;
      yield return a + (b * c + d);
      yield return a + ((b * c) + d);
      yield return a + (b * (c + d));
      yield return a + b * c - d;
      yield return (a + b) * c - d;
      yield return a + (b * c) - d;
      yield return a + b * (c - d);
      yield return (a + b * c) - d;
      yield return ((a + b) * c) - d;
      yield return (a + (b * c)) - d;
      yield return a + (b * c - d);
      yield return a + ((b * c) - d);
      yield return a + (b * (c - d));
      yield return a + b * c * d;
      yield return (a + b) * c * d;
      yield return a + (b * c) * d;
      yield return a + b * (c * d);
      yield return (a + b * c) * d;
      yield return ((a + b) * c) * d;
      yield return (a + (b * c)) * d;
      yield return a + (b * c * d);
      yield return a + ((b * c) * d);
      yield return a + (b * (c * d));
      yield return a + b * c / d;
      yield return (a + b) * c / d;
      yield return a + (b * c) / d;
      yield return a + b * (c / d);
      yield return (a + b * c) / d;
      yield return ((a + b) * c) / d;
      yield return (a + (b * c)) / d;
      yield return a + (b * c / d);
      yield return a + ((b * c) / d);
      yield return a + (b * (c / d));
      yield return a + b / c + d;
      yield return (a + b) / c + d;
      yield return a + (b / c) + d;
      yield return a + b / (c + d);
      yield return (a + b / c) + d;
      yield return ((a + b) / c) + d;
      yield return (a + (b / c)) + d;
      yield return a + (b / c + d);
      yield return a + ((b / c) + d);
      yield return a + (b / (c + d));
      yield return a + b / c - d;
      yield return (a + b) / c - d;
      yield return a + (b / c) - d;
      yield return a + b / (c - d);
      yield return (a + b / c) - d;
      yield return ((a + b) / c) - d;
      yield return (a + (b / c)) - d;
      yield return a + (b / c - d);
      yield return a + ((b / c) - d);
      yield return a + (b / (c - d));
      yield return a + b / c * d;
      yield return (a + b) / c * d;
      yield return a + (b / c) * d;
      yield return a + b / (c * d);
      yield return (a + b / c) * d;
      yield return ((a + b) / c) * d;
      yield return (a + (b / c)) * d;
      yield return a + (b / c * d);
      yield return a + ((b / c) * d);
      yield return a + (b / (c * d));
      yield return a + b / c / d;
      yield return (a + b) / c / d;
      yield return a + (b / c) / d;
      yield return a + b / (c / d);
      yield return (a + b / c) / d;
      yield return ((a + b) / c) / d;
      yield return (a + (b / c)) / d;
      yield return a + (b / c / d);
      yield return a + ((b / c) / d);
      yield return a + (b / (c / d));
      yield return a - b + c + d;
      yield return (a - b) + c + d;
      yield return a - (b + c) + d;
      yield return a - b + (c + d);
      yield return (a - b + c) + d;
      yield return ((a - b) + c) + d;
      yield return (a - (b + c)) + d;
      yield return a - (b + c + d);
      yield return a - ((b + c) + d);
      yield return a - (b + (c + d));
      yield return a - b + c - d;
      yield return (a - b) + c - d;
      yield return a - (b + c) - d;
      yield return a - b + (c - d);
      yield return (a - b + c) - d;
      yield return ((a - b) + c) - d;
      yield return (a - (b + c)) - d;
      yield return a - (b + c - d);
      yield return a - ((b + c) - d);
      yield return a - (b + (c - d));
      yield return a - b + c * d;
      yield return (a - b) + c * d;
      yield return a - (b + c) * d;
      yield return a - b + (c * d);
      yield return (a - b + c) * d;
      yield return ((a - b) + c) * d;
      yield return (a - (b + c)) * d;
      yield return a - (b + c * d);
      yield return a - ((b + c) * d);
      yield return a - (b + (c * d));
      yield return a - b + c / d;
      yield return (a - b) + c / d;
      yield return a - (b + c) / d;
      yield return a - b + (c / d);
      yield return (a - b + c) / d;
      yield return ((a - b) + c) / d;
      yield return (a - (b + c)) / d;
      yield return a - (b + c / d);
      yield return a - ((b + c) / d);
      yield return a - (b + (c / d));
      yield return a - b - c + d;
      yield return (a - b) - c + d;
      yield return a - (b - c) + d;
      yield return a - b - (c + d);
      yield return (a - b - c) + d;
      yield return ((a - b) - c) + d;
      yield return (a - (b - c)) + d;
      yield return a - (b - c + d);
      yield return a - ((b - c) + d);
      yield return a - (b - (c + d));
      yield return a - b - c - d;
      yield return (a - b) - c - d;
      yield return a - (b - c) - d;
      yield return a - b - (c - d);
      yield return (a - b - c) - d;
      yield return ((a - b) - c) - d;
      yield return (a - (b - c)) - d;
      yield return a - (b - c - d);
      yield return a - ((b - c) - d);
      yield return a - (b - (c - d));
      yield return a - b - c * d;
      yield return (a - b) - c * d;
      yield return a - (b - c) * d;
      yield return a - b - (c * d);
      yield return (a - b - c) * d;
      yield return ((a - b) - c) * d;
      yield return (a - (b - c)) * d;
      yield return a - (b - c * d);
      yield return a - ((b - c) * d);
      yield return a - (b - (c * d));
      yield return a - b - c / d;
      yield return (a - b) - c / d;
      yield return a - (b - c) / d;
      yield return a - b - (c / d);
      yield return (a - b - c) / d;
      yield return ((a - b) - c) / d;
      yield return (a - (b - c)) / d;
      yield return a - (b - c / d);
      yield return a - ((b - c) / d);
      yield return a - (b - (c / d));
      yield return a - b * c + d;
      yield return (a - b) * c + d;
      yield return a - (b * c) + d;
      yield return a - b * (c + d);
      yield return (a - b * c) + d;
      yield return ((a - b) * c) + d;
      yield return (a - (b * c)) + d;
      yield return a - (b * c + d);
      yield return a - ((b * c) + d);
      yield return a - (b * (c + d));
      yield return a - b * c - d;
      yield return (a - b) * c - d;
      yield return a - (b * c) - d;
      yield return a - b * (c - d);
      yield return (a - b * c) - d;
      yield return ((a - b) * c) - d;
      yield return (a - (b * c)) - d;
      yield return a - (b * c - d);
      yield return a - ((b * c) - d);
      yield return a - (b * (c - d));
      yield return a - b * c * d;
      yield return (a - b) * c * d;
      yield return a - (b * c) * d;
      yield return a - b * (c * d);
      yield return (a - b * c) * d;
      yield return ((a - b) * c) * d;
      yield return (a - (b * c)) * d;
      yield return a - (b * c * d);
      yield return a - ((b * c) * d);
      yield return a - (b * (c * d));
      yield return a - b * c / d;
      yield return (a - b) * c / d;
      yield return a - (b * c) / d;
      yield return a - b * (c / d);
      yield return (a - b * c) / d;
      yield return ((a - b) * c) / d;
      yield return (a - (b * c)) / d;
      yield return a - (b * c / d);
      yield return a - ((b * c) / d);
      yield return a - (b * (c / d));
      yield return a - b / c + d;
      yield return (a - b) / c + d;
      yield return a - (b / c) + d;
      yield return a - b / (c + d);
      yield return (a - b / c) + d;
      yield return ((a - b) / c) + d;
      yield return (a - (b / c)) + d;
      yield return a - (b / c + d);
      yield return a - ((b / c) + d);
      yield return a - (b / (c + d));
      yield return a - b / c - d;
      yield return (a - b) / c - d;
      yield return a - (b / c) - d;
      yield return a - b / (c - d);
      yield return (a - b / c) - d;
      yield return ((a - b) / c) - d;
      yield return (a - (b / c)) - d;
      yield return a - (b / c - d);
      yield return a - ((b / c) - d);
      yield return a - (b / (c - d));
      yield return a - b / c * d;
      yield return (a - b) / c * d;
      yield return a - (b / c) * d;
      yield return a - b / (c * d);
      yield return (a - b / c) * d;
      yield return ((a - b) / c) * d;
      yield return (a - (b / c)) * d;
      yield return a - (b / c * d);
      yield return a - ((b / c) * d);
      yield return a - (b / (c * d));
      yield return a - b / c / d;
      yield return (a - b) / c / d;
      yield return a - (b / c) / d;
      yield return a - b / (c / d);
      yield return (a - b / c) / d;
      yield return ((a - b) / c) / d;
      yield return (a - (b / c)) / d;
      yield return a - (b / c / d);
      yield return a - ((b / c) / d);
      yield return a - (b / (c / d));
      yield return a * b + c + d;
      yield return (a * b) + c + d;
      yield return a * (b + c) + d;
      yield return a * b + (c + d);
      yield return (a * b + c) + d;
      yield return ((a * b) + c) + d;
      yield return (a * (b + c)) + d;
      yield return a * (b + c + d);
      yield return a * ((b + c) + d);
      yield return a * (b + (c + d));
      yield return a * b + c - d;
      yield return (a * b) + c - d;
      yield return a * (b + c) - d;
      yield return a * b + (c - d);
      yield return (a * b + c) - d;
      yield return ((a * b) + c) - d;
      yield return (a * (b + c)) - d;
      yield return a * (b + c - d);
      yield return a * ((b + c) - d);
      yield return a * (b + (c - d));
      yield return a * b + c * d;
      yield return (a * b) + c * d;
      yield return a * (b + c) * d;
      yield return a * b + (c * d);
      yield return (a * b + c) * d;
      yield return ((a * b) + c) * d;
      yield return (a * (b + c)) * d;
      yield return a * (b + c * d);
      yield return a * ((b + c) * d);
      yield return a * (b + (c * d));
      yield return a * b + c / d;
      yield return (a * b) + c / d;
      yield return a * (b + c) / d;
      yield return a * b + (c / d);
      yield return (a * b + c) / d;
      yield return ((a * b) + c) / d;
      yield return (a * (b + c)) / d;
      yield return a * (b + c / d);
      yield return a * ((b + c) / d);
      yield return a * (b + (c / d));
      yield return a * b - c + d;
      yield return (a * b) - c + d;
      yield return a * (b - c) + d;
      yield return a * b - (c + d);
      yield return (a * b - c) + d;
      yield return ((a * b) - c) + d;
      yield return (a * (b - c)) + d;
      yield return a * (b - c + d);
      yield return a * ((b - c) + d);
      yield return a * (b - (c + d));
      yield return a * b - c - d;
      yield return (a * b) - c - d;
      yield return a * (b - c) - d;
      yield return a * b - (c - d);
      yield return (a * b - c) - d;
      yield return ((a * b) - c) - d;
      yield return (a * (b - c)) - d;
      yield return a * (b - c - d);
      yield return a * ((b - c) - d);
      yield return a * (b - (c - d));
      yield return a * b - c * d;
      yield return (a * b) - c * d;
      yield return a * (b - c) * d;
      yield return a * b - (c * d);
      yield return (a * b - c) * d;
      yield return ((a * b) - c) * d;
      yield return (a * (b - c)) * d;
      yield return a * (b - c * d);
      yield return a * ((b - c) * d);
      yield return a * (b - (c * d));
      yield return a * b - c / d;
      yield return (a * b) - c / d;
      yield return a * (b - c) / d;
      yield return a * b - (c / d);
      yield return (a * b - c) / d;
      yield return ((a * b) - c) / d;
      yield return (a * (b - c)) / d;
      yield return a * (b - c / d);
      yield return a * ((b - c) / d);
      yield return a * (b - (c / d));
      yield return a * b * c + d;
      yield return (a * b) * c + d;
      yield return a * (b * c) + d;
      yield return a * b * (c + d);
      yield return (a * b * c) + d;
      yield return ((a * b) * c) + d;
      yield return (a * (b * c)) + d;
      yield return a * (b * c + d);
      yield return a * ((b * c) + d);
      yield return a * (b * (c + d));
      yield return a * b * c - d;
      yield return (a * b) * c - d;
      yield return a * (b * c) - d;
      yield return a * b * (c - d);
      yield return (a * b * c) - d;
      yield return ((a * b) * c) - d;
      yield return (a * (b * c)) - d;
      yield return a * (b * c - d);
      yield return a * ((b * c) - d);
      yield return a * (b * (c - d));
      yield return a * b * c * d;
      yield return (a * b) * c * d;
      yield return a * (b * c) * d;
      yield return a * b * (c * d);
      yield return (a * b * c) * d;
      yield return ((a * b) * c) * d;
      yield return (a * (b * c)) * d;
      yield return a * (b * c * d);
      yield return a * ((b * c) * d);
      yield return a * (b * (c * d));
      yield return a * b * c / d;
      yield return (a * b) * c / d;
      yield return a * (b * c) / d;
      yield return a * b * (c / d);
      yield return (a * b * c) / d;
      yield return ((a * b) * c) / d;
      yield return (a * (b * c)) / d;
      yield return a * (b * c / d);
      yield return a * ((b * c) / d);
      yield return a * (b * (c / d));
      yield return a * b / c + d;
      yield return (a * b) / c + d;
      yield return a * (b / c) + d;
      yield return a * b / (c + d);
      yield return (a * b / c) + d;
      yield return ((a * b) / c) + d;
      yield return (a * (b / c)) + d;
      yield return a * (b / c + d);
      yield return a * ((b / c) + d);
      yield return a * (b / (c + d));
      yield return a * b / c - d;
      yield return (a * b) / c - d;
      yield return a * (b / c) - d;
      yield return a * b / (c - d);
      yield return (a * b / c) - d;
      yield return ((a * b) / c) - d;
      yield return (a * (b / c)) - d;
      yield return a * (b / c - d);
      yield return a * ((b / c) - d);
      yield return a * (b / (c - d));
      yield return a * b / c * d;
      yield return (a * b) / c * d;
      yield return a * (b / c) * d;
      yield return a * b / (c * d);
      yield return (a * b / c) * d;
      yield return ((a * b) / c) * d;
      yield return (a * (b / c)) * d;
      yield return a * (b / c * d);
      yield return a * ((b / c) * d);
      yield return a * (b / (c * d));
      yield return a * b / c / d;
      yield return (a * b) / c / d;
      yield return a * (b / c) / d;
      yield return a * b / (c / d);
      yield return (a * b / c) / d;
      yield return ((a * b) / c) / d;
      yield return (a * (b / c)) / d;
      yield return a * (b / c / d);
      yield return a * ((b / c) / d);
      yield return a * (b / (c / d));
      yield return a / b + c + d;
      yield return (a / b) + c + d;
      yield return a / (b + c) + d;
      yield return a / b + (c + d);
      yield return (a / b + c) + d;
      yield return ((a / b) + c) + d;
      yield return (a / (b + c)) + d;
      yield return a / (b + c + d);
      yield return a / ((b + c) + d);
      yield return a / (b + (c + d));
      yield return a / b + c - d;
      yield return (a / b) + c - d;
      yield return a / (b + c) - d;
      yield return a / b + (c - d);
      yield return (a / b + c) - d;
      yield return ((a / b) + c) - d;
      yield return (a / (b + c)) - d;
      yield return a / (b + c - d);
      yield return a / ((b + c) - d);
      yield return a / (b + (c - d));
      yield return a / b + c * d;
      yield return (a / b) + c * d;
      yield return a / (b + c) * d;
      yield return a / b + (c * d);
      yield return (a / b + c) * d;
      yield return ((a / b) + c) * d;
      yield return (a / (b + c)) * d;
      yield return a / (b + c * d);
      yield return a / ((b + c) * d);
      yield return a / (b + (c * d));
      yield return a / b + c / d;
      yield return (a / b) + c / d;
      yield return a / (b + c) / d;
      yield return a / b + (c / d);
      yield return (a / b + c) / d;
      yield return ((a / b) + c) / d;
      yield return (a / (b + c)) / d;
      yield return a / (b + c / d);
      yield return a / ((b + c) / d);
      yield return a / (b + (c / d));
      yield return a / b - c + d;
      yield return (a / b) - c + d;
      yield return a / (b - c) + d;
      yield return a / b - (c + d);
      yield return (a / b - c) + d;
      yield return ((a / b) - c) + d;
      yield return (a / (b - c)) + d;
      yield return a / (b - c + d);
      yield return a / ((b - c) + d);
      yield return a / (b - (c + d));
      yield return a / b - c - d;
      yield return (a / b) - c - d;
      yield return a / (b - c) - d;
      yield return a / b - (c - d);
      yield return (a / b - c) - d;
      yield return ((a / b) - c) - d;
      yield return (a / (b - c)) - d;
      yield return a / (b - c - d);
      yield return a / ((b - c) - d);
      yield return a / (b - (c - d));
      yield return a / b - c * d;
      yield return (a / b) - c * d;
      yield return a / (b - c) * d;
      yield return a / b - (c * d);
      yield return (a / b - c) * d;
      yield return ((a / b) - c) * d;
      yield return (a / (b - c)) * d;
      yield return a / (b - c * d);
      yield return a / ((b - c) * d);
      yield return a / (b - (c * d));
      yield return a / b - c / d;
      yield return (a / b) - c / d;
      yield return a / (b - c) / d;
      yield return a / b - (c / d);
      yield return (a / b - c) / d;
      yield return ((a / b) - c) / d;
      yield return (a / (b - c)) / d;
      yield return a / (b - c / d);
      yield return a / ((b - c) / d);
      yield return a / (b - (c / d));
      yield return a / b * c + d;
      yield return (a / b) * c + d;
      yield return a / (b * c) + d;
      yield return a / b * (c + d);
      yield return (a / b * c) + d;
      yield return ((a / b) * c) + d;
      yield return (a / (b * c)) + d;
      yield return a / (b * c + d);
      yield return a / ((b * c) + d);
      yield return a / (b * (c + d));
      yield return a / b * c - d;
      yield return (a / b) * c - d;
      yield return a / (b * c) - d;
      yield return a / b * (c - d);
      yield return (a / b * c) - d;
      yield return ((a / b) * c) - d;
      yield return (a / (b * c)) - d;
      yield return a / (b * c - d);
      yield return a / ((b * c) - d);
      yield return a / (b * (c - d));
      yield return a / b * c * d;
      yield return (a / b) * c * d;
      yield return a / (b * c) * d;
      yield return a / b * (c * d);
      yield return (a / b * c) * d;
      yield return ((a / b) * c) * d;
      yield return (a / (b * c)) * d;
      yield return a / (b * c * d);
      yield return a / ((b * c) * d);
      yield return a / (b * (c * d));
      yield return a / b * c / d;
      yield return (a / b) * c / d;
      yield return a / (b * c) / d;
      yield return a / b * (c / d);
      yield return (a / b * c) / d;
      yield return ((a / b) * c) / d;
      yield return (a / (b * c)) / d;
      yield return a / (b * c / d);
      yield return a / ((b * c) / d);
      yield return a / (b * (c / d));
      yield return a / b / c + d;
      yield return (a / b) / c + d;
      yield return a / (b / c) + d;
      yield return a / b / (c + d);
      yield return (a / b / c) + d;
      yield return ((a / b) / c) + d;
      yield return (a / (b / c)) + d;
      yield return a / (b / c + d);
      yield return a / ((b / c) + d);
      yield return a / (b / (c + d));
      yield return a / b / c - d;
      yield return (a / b) / c - d;
      yield return a / (b / c) - d;
      yield return a / b / (c - d);
      yield return (a / b / c) - d;
      yield return ((a / b) / c) - d;
      yield return (a / (b / c)) - d;
      yield return a / (b / c - d);
      yield return a / ((b / c) - d);
      yield return a / (b / (c - d));
      yield return a / b / c * d;
      yield return (a / b) / c * d;
      yield return a / (b / c) * d;
      yield return a / b / (c * d);
      yield return (a / b / c) * d;
      yield return ((a / b) / c) * d;
      yield return (a / (b / c)) * d;
      yield return a / (b / c * d);
      yield return a / ((b / c) * d);
      yield return a / (b / (c * d));
      yield return a / b / c / d;
      yield return (a / b) / c / d;
      yield return a / (b / c) / d;
      yield return a / b / (c / d);
      yield return (a / b / c) / d;
      yield return ((a / b) / c) / d;
      yield return (a / (b / c)) / d;
      yield return a / (b / c / d);
      yield return a / ((b / c) / d);
      yield return a / (b / (c / d));
    }
  }
}
