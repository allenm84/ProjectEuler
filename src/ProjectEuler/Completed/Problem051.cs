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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem051 : EulerProblem
  {
    public override int Number { get { return 51; } }

    public override object Solve()
    {
      var digits = "0123456789".ToCharArray();
      int len = 7;
      int minValue = int.MaxValue;

      // len is the number of digits in the number
      for (int dot = 1; dot < len; ++dot)
      {
        // dot is the number of dots within the number
        int XCount = len - dot;
        var pattern =
          Enumerable.Repeat('X', XCount).Concat(
          Enumerable.Repeat('*', dot)).ToList();

        // permutate the values
        var permutations = new Permutations<char>(pattern);
        foreach (IList<char> value in permutations)
        {
          // now that we have the pattern, we need to replace the
          // dots with a single digit (which we can do elsewhere),
          // and we need to replace the Xs with digits
          var combinations = new Combinations<char>(digits, XCount, GenerateOption.WithRepetition);
          foreach (var insertions in combinations)
          {
            int i = 0;
            var text = value.ToArray();
            for (int v = 0; v < text.Length; ++v)
            {
              if (text[v] == 'X')
              {
                // insert a digit into 'X'
                text[v] = insertions[i++];
              }
            }

            // now, we need to replace "*" with a digit
            var family = GetFamily(text);
            if (family.Count == 8)
            {
              // retrieve the min value
              int min = family.Min();
              if (min < minValue)
              {
                minValue = min;
              }
            }
          }
        }
      }

      return minValue;
    }

    private List<int> GetFamily(char[] pattern)
    {
      List<int> indices = new List<int>();
      for (int j = 0; j < pattern.Length; ++j)
      {
        if (pattern[j] == '*')
        {
          indices.Add(j);
        }
      }

      List<int> retval = new List<int>();
      if (indices.Count > 0)
      {
        for (char c = '0'; c <= '9'; ++c)
        {
          foreach (int i in indices)
          {
            pattern[i] = c;
          }

          int num = Convert.ToInt32(new string(pattern));
          if (num.IsPrime())
          {
            retval.Add(num);
          }
        }

        // retrieve the lengths of all the values
        var lengths = new Dictionary<int, bool>();
        foreach (var value in retval)
        {
          lengths[value.ToString().Length] = true;
        }

        // if the all the lengths aren't equal
        if (lengths.Count != 1)
        {
          retval.Clear();
        }
      }
      return retval;
    }
  }
}
