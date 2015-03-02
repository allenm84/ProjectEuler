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
  public class Problem098 : EulerProblem
  {
    public override int Number { get { return 98; } }

    public override object Solve()
    {
      // get a list of all the words
      var words = Resources.Problem098Data
        .SplitBy(',')
        .Select(l => l.SplitBy('"')[0]);

      // basically, let's say we have the word CARE and RACE. We need
      // to find all 4-digit square numbers such that the sorted distinct
      // letters of CARE and RACE {ACER} have the same length as the sorted
      // distinct digits of the square. Then, we need to test and make sure
      // that when mapping the 4-digit square to one of the words in the pair
      // that using the map for the other word in the pair also results in a 4-digit
      // number. For example, if we were using {1,2,6,9} for CARE, we'd get {6,2,1,9}
      // for RACE.

      // basically, we need to group all the anagram words together
      var wordGroups = (from w in words
                    group w by k(w) into wg
                    select new
                    {
                      Key = wg.Key,
                      Count = wg.Count(),
                      Values = wg.ToArray(),
                    })
                    .Where(a => a.Count > 1)
                    .OrderByDescending(a => a.Key.Length);

      // keep track of the maximum
      var maximum = 0;

      // go through the groups
      foreach (var wordGroup in wordGroups)
      {
        // retrieve all the squares that are "anagrams" themselves
        var candidates = (from s in GetMatchingSquares(wordGroup.Key)
                          group s by k(s) into sg
                          select new
                          {
                            Key = sg.Key,
                            Count = sg.Count(),
                            Squares = new HashSet<int>(sg.ToArray()),
                          })
                         .Where(a => a.Count > 1 && !a.Key.StartsWith("0"))
                         .ToArray();

        // if candidates were actually found
        if (candidates.Length > 0)
        {
          // now, we need to go through the candidates and see if they match our specific
          // word pairs
          var values = wordGroup.Values;
          for (int v1 = 0; v1 < values.Length; ++v1)
          {
            var word1 = values[v1];
            for (int v2 = v1 + 1; v2 < values.Length; ++v2)
            {
              var word2 = values[v2];
              foreach (var candidate in candidates)
              {
                var matches = Anagram(word1, word2, candidate.Squares);
                foreach (var m in matches)
                {
                  if (maximum < m)
                  {
                    maximum = m;
                  }
                }
              }
            }
          }
        }
      }

      // return an empty string for now
      return maximum;
    }

    private IEnumerable<int> Anagram(string word1, string word2, HashSet<int> squares)
    {
      var key = word1.Distinct().ToArray();
      foreach (var square in squares)
      {
        // we have a square number; let's say 1296. We also have word1 and word2.
        // lets see if 1296 mapped to word2 will produce a square
        var number = square.ToString().Distinct().ToArray();
        var table = new Dictionary<char, char>();
        for (int i = 0; i < number.Length; ++i)
        {
          table[key[i]] = number[i];
        }

        var wordValue = new char[word2.Length];
        for (int w = 0; w < wordValue.Length; ++w)
        {
          wordValue[w] = table[word2[w]];
        }

        var value = int.Parse(new string(wordValue));
        if (squares.Contains(value))
        {
          yield return value;
        }
      }
    }

    private IEnumerable<int> GetMatchingSquares(string key)
    {
      var n = (int)Math.Sqrt(int.Parse(
        new string(Enumerable
          .Repeat('9', key.Length)
          .ToArray())));

      var distinctKey = key.Distinct().ToArray();
      for (; n > 0; --n)
      {
        var square = (n * n);
        var squareText = square.ToString();
        if (squareText.Length < key.Length) break;

        var squareTextDistinct = squareText.Distinct().ToArray();
        if (squareTextDistinct.Length == distinctKey.Length)
        {
          yield return square;
        }
      }
    }

    private string k(object o)
    {
      return new string(o.ToString().OrderBy(c => c).ToArray());
    }
  }
}
