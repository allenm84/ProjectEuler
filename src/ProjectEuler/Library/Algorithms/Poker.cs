using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public static partial class Poker
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public static ushort EvaluateHand(string hand)
    {
      return EvaluateHand(hand
        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(c => toKevCard(c))
        .ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    public static ushort EvaluateHand(int[] hand)
    {
      int c1, c2, c3, c4, c5;

      c1 = hand[0];
      c2 = hand[1];
      c3 = hand[2];
      c4 = hand[3];
      c5 = hand[4];

      return (EvaluateHand(c1, c2, c3, c4, c5));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <param name="c3"></param>
    /// <param name="c4"></param>
    /// <param name="c5"></param>
    /// <returns></returns>
    public static ushort EvaluateHand(int c1, int c2, int c3, int c4, int c5)
    {
      uint q = (uint)(c1 | c2 | c3 | c4 | c5) >> 16;
      ushort s;

      // check for flushes and straight flushes
      if ((c1 & c2 & c3 & c4 & c5 & 0xf000) != 0)
      {
        return flushes[q];
      }

      // check for straights and high card hands
      s = unique5[q];
      if ((s != 0))
      {
        return s;
      }

      return hash_values[find_fast((uint)((c1 & 0xff) * (c2 & 0xff) * (c3 & 0xff) * (c4 & 0xff) * (c5 & 0xff)))];
    }

    private static uint find_fast(uint u)
    {
      uint a, b, r;
      u += 0xe91aaa35;
      u ^= u >> 16;
      u += u << 8;
      u ^= u >> 4;
      b = (u >> 8) & 0x1ff;
      a = (u + (u << 2)) >> 19;
      r = a ^ hash_adjust[b];
      return r;
    }

    private static int toKevCard(string card)
    {
      // bit format of cards:
      // xxxAKQJT 98765432 CDHSrrrr xxPPPPPP

      // retrieve what the rank is
      int j = RANKS[card[0]];

      // retrieve the suit mask
      int suit = 0;
      switch (card[1])
      {
        case 'C': { suit = CLUB; break; }
        case 'D': { suit = DIAMOND; break; }
        case 'H': { suit = HEART; break; }
        case 'S': { suit = SPADE; break; }
      }

      // return the card mask
      return primes[j] | (j << 8) | suit | (1 << (16 + j));
    }
  }
}
