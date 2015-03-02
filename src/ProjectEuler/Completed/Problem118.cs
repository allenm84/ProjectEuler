using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem118 : EulerProblem
  {
    private Func<int, bool> IsPrimeMemo;

    public Problem118()
    {
      IsPrimeMemo = PrimeExtensions.IsPrime;
      IsPrimeMemo = IsPrimeMemo.Memoize();
    }

    public override int Number
    {
      get { return 118; }
    }

    public override object Solve()
    {
      var partitions = 9.GetPartitions()
        .Where(p => !p.All(n => n == 1))
        .ToArray();

      var results = new List<string>();
      var digits = "123456789".ToCharArray();

      foreach (var array in digits.Permute())
      {
        foreach (var part in partitions)
        {
          List<int> set;
          if (CreateSet(part, array, out set))
          {
            // sort the set
            set.Sort();

            // add the set to the results set
            results.Add(string.Join(",", set));
          }
        }
      }
      return results.Distinct().Count();
    }

    private bool CreateSet(int[] lengths, char[] array, out List<int> set)
    {
      set = new List<int>();
      var start = 0;

      foreach (var length in lengths)
      {
        var subset = new char[length];
        Array.Copy(array, start, subset, 0, length);

        var num = int.Parse(new string(subset));
        if (!IsPrimeMemo(num)) { return false; }

        set.Add(num);
        start += length;
      }

      return true;
    }
  }
}