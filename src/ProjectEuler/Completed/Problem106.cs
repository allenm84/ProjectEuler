using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem106 : Problem103
  {
    public override int Number
    {
      get { return 106; }
    }

    public override object Solve()
    {
      // create a variable for N
      var N = 4;

      // create a set
      var set = Enumerable.Range(1, N).ToArray();

      // generate the subsets
      var subsets = GenerateSubsets(set).ToArray();

      // create a variable for the count
      var pairs = new List<SumSet[]>(subsets.Length << 1);

      // go through the subsets
      for (var i = 0; i < subsets.Length; ++i)
      {
        var B = subsets[i];
        var bArr = B.Set.ToArray();

        for (var j = i + 1; j < subsets.Length; ++j)
        {
          var C = subsets[j];

          // the sets must be non-empty and disjoint
          if (C.Overlaps(B)) { continue; }

          // If B contains more elements than C then S(B) > S(C).
          if ((B.Count > C.Count) && (B.Sum > C.Sum)) { continue; }
          if ((C.Count > B.Count) && (C.Sum > B.Sum)) { continue; }
          if (B.Count == 1 || C.Count == 1) { continue; }
          if (B.Count != C.Count) { continue; }

          // retrieve the B/C array
          var cArr = C.Set.ToArray();

          // add the sumset pair
          var skipped = true;
          for (var s = 0; skipped && s < B.Count; ++s)
          {
            skipped &= cArr[s] > bArr[s];
          }
          if (!skipped)
          {
            pairs.Add(new[] {B, C});
          }
        }
      }

      // return the count
      return pairs.Count;
    }
  }
}