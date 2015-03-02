using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem042 : EulerProblem
  {
    public override int Number
    {
      get { return 42; }
    }

    public override object Solve()
    {
      // retrieve the words from the list ordered by their length
      var words = Resources.Problem042Data
        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
        .Select(t => string.Join("", t.Where(c => char.IsLetter(c))))
        .OrderBy(t => t.Length)
        .ToArray();

      // get the length of the longest word
      var len = words.Last().Length;

      // now, assume that every letter in that word is 'Z'. We would need a maximum
      // value of 'Z'*len to match triangle numbers
      var maxTriangle = 26 * len;

      // create a function to calculate triangle numbers
      Func<float, int> Triangle = n => (int)((n / 2) * (n + 1));

      // create a dictionary to hold the triangle numbers
      var table = new Dictionary<int, bool>(maxTriangle);

      // generate the triangle numbers
      var tn = 0;
      var num = 1;
      do
      {
        tn = Triangle(num++);
        table[tn] = true;
      } while (tn < maxTriangle);

      // create a function to calculate a word score
      Func<string, int> Score = s => s.ToCharArray().Sum(c => (c - 'A') + 1);

      // create a counter to maintain how many words are triangle words
      var triangleWordCount = 0;

      // calculate the score for each word
      foreach (var word in words)
      {
        var s = Score(word);
        if (table.ContainsKey(s))
        {
          ++triangleWordCount;
        }
      }

      // return the count
      return triangleWordCount;
    }
  }
}