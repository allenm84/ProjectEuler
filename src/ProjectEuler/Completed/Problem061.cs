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

namespace ProjectEuler
{
  public class Problem061 : EulerProblem
  {
    const byte Tri = 0x20;
    const byte Sqr = Tri >> 1;
    const byte Pen = Sqr >> 1;
    const byte Hex = Pen >> 1;
    const byte Hep = Hex >> 1;
    const byte Oct = Hep >> 1;

    static int[] triangles;
    static int[] squares;
    static int[] pentagons;
    static int[] hexagons;
    static int[] heptagons;
    static int[] octagons;

    static Problem061()
    {
      Func<int, bool> skip = (k => k < 1000);
      Func<int, bool> take = (k => k < 10000);

      Func<IEnumerable<int>>[] getters = new Func<IEnumerable<int>>[6];
      getters[0] = MathHelper.TriangleNumbers;
      getters[1] = MathHelper.SquareNumbers;
      getters[2] = MathHelper.PentagonNumbers;
      getters[3] = MathHelper.HexagonNumbers;
      getters[4] = MathHelper.HeptagonNumbers;
      getters[5] = MathHelper.OctagonNumbers;

      Action<int[]>[] setters = new Action<int[]>[6];
      setters[0] = a => triangles = a;
      setters[1] = a => squares = a;
      setters[2] = a => pentagons = a;
      setters[3] = a => hexagons = a;
      setters[4] = a => heptagons = a;
      setters[5] = a => octagons = a;

      for (int i = 0; i < 6; ++i)
      {
        setters[i](getters[i]()
          .SkipWhile(skip)
          .TakeWhile(take)
          .OrderBy(c => c)
          .ToArray());
      }
    }

    public override int Number { get { return 61; } }

    public override object Solve()
    {
      var candidates = new List<int>();
      for (int m = 1000; m < 10000; ++m)
      {
        // if the number is contained within any of the tables
        if (
          triangles.Contains(m) ||
          squares.Contains(m) ||
          pentagons.Contains(m) ||
          hexagons.Contains(m) ||
          heptagons.Contains(m) ||
          octagons.Contains(m))
        {
          candidates.Add(m);
        }
      }

      // get all of the numbers that have a parent and a child
      for (int i = 0; i < candidates.Count; ++i)
      {
        var first = candidates[i];
        var firstArr = candidates.Where(c => IsChildOf(first, c));
        foreach (var second in firstArr)
        {
          if (second == first) continue;
          var secondArr = candidates.Where(c => IsChildOf(second, c));
          foreach (var third in secondArr)
          {
            if (third == first || third == second) continue;
            var thirdArr = candidates.Where(c => IsChildOf(third, c));
            foreach (var fourth in thirdArr)
            {
              if (fourth == first || fourth == second || fourth == third) continue;
              var fourthArr = candidates.Where(c => IsChildOf(fourth, c));
              foreach (var fifth in fourthArr)
              {
                if (fifth == first || fifth == second || fifth == third || fifth == fourth) continue;
                var fifthArr = candidates.Where(c => IsChildOf(fifth, c));
                foreach (var sixth in fifthArr)
                {
                  if (sixth == first || sixth == second || sixth == third || sixth == fourth || sixth == fifth) continue;
                  if (IsChildOf(sixth, first))
                  {
                    // hooray! This is completely cyclic. Next, we need
                    // to make sure that each of the six values appears is
                    // polygonal
                    int[] values = new int[]
                    {
                      first,second,third,
                      fourth,fifth,sixth,
                    };

                    // make sure that the chain is valid
                    if (IsChainPolygonal(values))
                    {
                      return values.Sum();
                    }
                  }
                }
              }
            }
          }
        }
      }

      return "<NONE>";
    }

    private bool IsChainPolygonal(int[] values)
    {
      // create a dictionary
      var terms = new Dictionary<int, List<int>>();
      terms[Tri] = new List<int>();
      terms[Sqr] = new List<int>();
      terms[Pen] = new List<int>();
      terms[Hex] = new List<int>();
      terms[Hep] = new List<int>();
      terms[Oct] = new List<int>();

      // determine if each value is contained at least once
      // in each of the arrays
      foreach (var v in values)
      {
        if (triangles.Contains(v)) terms[Tri].Add(v);
        if (squares.Contains(v)) terms[Sqr].Add(v);
        if (pentagons.Contains(v)) terms[Pen].Add(v);
        if (hexagons.Contains(v)) terms[Hex].Add(v);
        if (heptagons.Contains(v)) terms[Hep].Add(v);
        if (octagons.Contains(v)) terms[Oct].Add(v);
      }

      // if any of the lists are empty
      if (terms.Values.Any(t => t.Count == 0)) return false;

      // get the list of terms
      var allTerms = terms.Values.ToList();

      // go through the list and remove any values that appear more than once
      for (int a = 0; a < allTerms.Count; ++a)
      {
        var lstA = allTerms[a];
        if (lstA.Count == 1)
        {
          var value = lstA[0];
          for (int b = 0; b < allTerms.Count; ++b)
          {
            if (b == a) { continue; }
            var lstB = allTerms[b];

            if (lstB.Contains(value))
            {
              lstB.Remove(value);
            }
          }
        }
      }

      // make sure that after removing the values, the counts are 1
      bool properPlacement = true;
      for (int c = 0; c < allTerms.Count && properPlacement; ++c)
      {
        properPlacement &= allTerms[c].Count == 1;
      }

      return properPlacement;
    }

    private bool IsChildOf(int parent, int child)
    {
      var a = parent.ToString();
      var b = child.ToString();
      return IsCyclic(a, b);
    }

    private bool IsCyclic(string num1, string num2)
    {
      // if the last two digits equal the first two digits
      return 
        (num1[2] == num2[0]) && 
        (num1[3] == num2[1]);
    }
  }
}
