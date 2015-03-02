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
  public class Problem109 : EulerProblem
  {
    private enum Dart : byte
    {
      Zero = 0,

      S1, S2, S3, S4, S5,
      S6, S7, S8, S9, S10,
      S11, S12, S13, S14, S15,
      S16, S17, S18, S19, S20,
      S25,

      D1, D2, D3, D4, D5,
      D6, D7, D8, D9, D10,
      D11, D12, D13, D14, D15,
      D16, D17, D18, D19, D20,
      D25,

      T1, T2, T3, T4, T5,
      T6, T7, T8, T9, T10,
      T11, T12, T13, T14, T15,
      T16, T17, T18, T19, T20,
    };

    public override int Number { get { return 109; } }

    public override object Solve()
    {
      // create an array of darts
      var sections = Enum
        .GetValues(typeof(Dart))
        .Cast<Dart>()
        .OrderBy(d => d)
        .ToArray();

      // create an array of dart values
      var mult = 1;
      var add = 0;
      var values = new int[sections.Length];
      for (int i = 1; i < values.Length; ++i)
      {
        if (i == 21)
        {
          values[i] = 25;
          continue;
        }
        else if (i == 42)
        {
          values[i] = 50;
          continue;
        }

        if (i == 22)
        {
          mult = 2;
          add = -21;
        }
        else if (i == 43)
        {
          mult = 3;
          add = -42;
        }

        values[i] = (i + add) * mult;
      }

      // go through the game
      var checkOuts = new Dictionary<int, HashSet<int>>();
      for (int c = 1; c <= 3; ++c)
      {
        // generate the combinations of the sections
        foreach (var arrangement in sections.Choose(c, true))
        {
          // get the permuations
          var permutation = arrangement.Permute();
          foreach (var game in permutation)
          {
            // if this is a not checkout game, then continue
            var last = game.Last();
            if (last > Dart.D25 || last < Dart.D1) continue;

            // get the score of this throw
            int score = game.Sum(t => values[(int)t]);

            // if the score is zero, then continue
            if (score == 0) continue;

            // if the score is too big, then continue
            if (score >= 100) continue;

            // retrieve the list for this score
            HashSet<int> set;
            if (!checkOuts.TryGetValue(score, out set))
            {
              set = new HashSet<int>();
              checkOuts[score] = set;
            }

            // retrieve the keys
            byte key1, key2, key3;
            Sort(game, out key1, out key2, out key3);

            // from the keys, create an integer value
            int value = BitConverter.ToInt32(new byte[] { 
            0x00, key1, key2, key3, }, 0);

            // add the value to the set
            set.Add(value);
          }
        }
      }

      return checkOuts.Sum(k => k.Value.Count);
    }

    private void Sort(Dart[] game, out byte key1, out byte key2, out byte key3)
    {
      if (game.Length == 1)
      {
        key1 = key2 = 0x00;
        key3 = (byte)game[0];
      }
      else if (game.Length == 2)
      {
        key1 = 0x00;
        key2 = (byte)game[0];
        key3 = (byte)game[1];
      }
      else
      {
        if (game[1] < game[0])
        {
          key1 = (byte)game[1];
          key2 = (byte)game[0];
        }
        else
        {
          key1 = (byte)game[0];
          key2 = (byte)game[1];
        }
        key3 = (byte)game[2];
      }
    }
  }
}
