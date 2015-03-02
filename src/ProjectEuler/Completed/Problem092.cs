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

namespace ProjectEuler
{
  public class Problem092 : EulerProblem
  {
    protected class ChainNode
    {
      private Lazy<bool> hitsOne;

      public int Value;
      public ChainNode Next;
      public bool HitsOne { get { return hitsOne.Value; } }

      public ChainNode()
      {
        hitsOne = new Lazy<bool>(GetHitOne);
      }

      private bool GetHitOne()
      {
        if (Value == 1) return true;
        if (Value == 89) return false;
        return Next.HitsOne;
      }
    }

    public override int Number { get { return 92; } }

    public override object Solve()
    {
      const int TenMillion = 10000000;

      // the biggest number is 9999999.  Totaling the square of the digits
      // gives us a maximum number of 567.
      var nodes = new ChainNode[580];
      for (int n = 0; n < nodes.Length; ++n)
      {
        nodes[n] = new ChainNode { Value = n };
      }

      // next, we need to link each of the nodes together
      for (int n = 0; n < nodes.Length; ++n)
      {
        var node = nodes[n];
        var text = node.Value.ToString().ToCharArray();

        var sum = 0;
        foreach (var c in text)
        {
          switch (c)
          {
            case '0': sum += 0; break;
            case '1': sum += 1; break;
            case '2': sum += 4; break;
            case '3': sum += 9; break;
            case '4': sum += 16; break;
            case '5': sum += 25; break;
            case '6': sum += 36; break;
            case '7': sum += 49; break;
            case '8': sum += 64; break;
            case '9': sum += 81; break;
          }
        }

        node.Next = nodes[sum];
      }

      // keep track of the count
      int count = 0;

      // next, go through the n values
      for (int n = 2; n < TenMillion; ++n)
      {
        var text = n.ToString().ToCharArray();
        var sum = 0;
        foreach (var c in text)
        {
          switch (c)
          {
            case '0': sum += 0; break;
            case '1': sum += 1; break;
            case '2': sum += 4; break;
            case '3': sum += 9; break;
            case '4': sum += 16; break;
            case '5': sum += 25; break;
            case '6': sum += 36; break;
            case '7': sum += 49; break;
            case '8': sum += 64; break;
            case '9': sum += 81; break;
          }
        }

        // retrieve the node
        var node = nodes[sum];
        if (node.HitsOne) continue;
        ++count;
      }

      // return the count
      return count;
    }
  }
}
