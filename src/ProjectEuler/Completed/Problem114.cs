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
  public class Problem114 : EulerProblem
  {
    public override int Number { get { return 114; } }

    public override object Solve()
    {
      // used Solve001() program to generate these values,
      // and I noticed a pattern:
      // 
      // 5, 06, 07, 08, 09, 10, 011, 012, 013, 014, 015
      // 6, 11, 17, 27, 44, 72, 117, 189, 305, 493, 798

      // 011 + 017 = 028    (-1)
      // 017 + 027 = 044    (+0)
      // 027 + 044 = 071    (+1)
      // 044 + 072 = 116    (+1)
      // 072 + 117 = 189    (+0)
      // 117 + 189 = 306    (-1)
      // 189 + 305 = 494    (-1)
      // 305 + 493 = 798    (+0)

      var set = new BigInteger[] { 0, 1, 1, 0, -1, -1 };
      var i = 3;

      BigInteger f1 = 72;
      BigInteger f2 = 117;

      for (int n = 11; n < 50; ++n, i = (i + 1) % set.Length)
      {
        var f3 = f1 + f2 + set[i];
        f1 = f2;
        f2 = f3;
      }

      return f2;
    }

    private abstract class Block : IComparable<Block>
    {
      public int Size;
      public override string ToString()
      {
        return Size.ToString();
      }

      public static string t(Block b)
      {
        return string.Concat(b is RedBlock ? "r(" : "b(", b.Size, ")");
      }

      public int CompareTo(Block other)
      {
        return t(this).CompareTo(t(other));
      }
    }

    private class RedBlock : Block { }
    private class BlackBlock : Block { }

    private string Solve001()
    {
      const int Length = 15;

      var possibleRedBlocks = Enumerable
        .Range(3, Length - 3)
        .Select(i => (Block)new RedBlock { Size = i });

      var possibleBlackBlocks = Enumerable
        .Range(1, Length - 3)
        .Select(i => (Block)new BlackBlock { Size = i });

      var possibleBlocks = possibleRedBlocks
        .Concat(possibleBlackBlocks)
        .ToArray();

      BigInteger count = 2;

      int cEnd = Length - 3;
      for (int c = 2; c <= cEnd; ++c)
      {
        var combinations = new Combinations<Block>(possibleBlocks, c, GenerateOption.WithRepetition);
        foreach (var value in combinations)
        {
          var combination = value.ToArray();

          int sum = combination.Sum(b => b.Size);
          if (sum != Length) continue;

          int redCount = combination.Count(b => b is RedBlock);

          // if there are no red blocks, then continue
          if (redCount == 0) continue;

          // if there is only 1 red block, but more than 2 black blocks
          // then continue
          if (redCount == 1 && (combination.Length - 1) > 2) continue;

          var permutations = new Permutations<Block>(combination);
          foreach (IList<Block> arrangement in permutations)
          {
            if (IsValid(arrangement))
            {
              var text = string.Join(" ", arrangement.Select(b => Block.t(b)));
              ++count;
            }
          }
        }
      }
      return count.ToString();
    }

    private bool IsValid(IList<Block> p)
    {
      bool valid = true;
      bool expectRed = p[0] is BlackBlock;

      for (int i = 1; valid && i < p.Count; ++i)
      {
        if (expectRed)
        {
          if (p[i] is BlackBlock)
          {
            valid = false;
            break;
          }
          else
          {
            expectRed = false;
            continue;
          }
        }
        else
        {
          if (p[i] is RedBlock)
          {
            valid = false;
            break;
          }
          else
          {
            expectRed = true;
            continue;
          }
        }
      }
      return valid;
    }
  }
}
