using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem092 : EulerProblem
  {
    public override int Number
    {
      get { return 92; }
    }

    public override object Solve()
    {
      const int TenMillion = 10000000;

      // the biggest number is 9999999.  Totaling the square of the digits
      // gives us a maximum number of 567.
      var nodes = new ChainNode[580];
      for (var n = 0; n < nodes.Length; ++n)
      {
        nodes[n] = new ChainNode {Value = n};
      }

      // next, we need to link each of the nodes together
      for (var n = 0; n < nodes.Length; ++n)
      {
        var node = nodes[n];
        var text = node.Value.ToString().ToCharArray();

        var sum = 0;
        foreach (var c in text)
        {
          switch (c)
          {
            case '0':
              sum += 0;
              break;
            case '1':
              sum += 1;
              break;
            case '2':
              sum += 4;
              break;
            case '3':
              sum += 9;
              break;
            case '4':
              sum += 16;
              break;
            case '5':
              sum += 25;
              break;
            case '6':
              sum += 36;
              break;
            case '7':
              sum += 49;
              break;
            case '8':
              sum += 64;
              break;
            case '9':
              sum += 81;
              break;
          }
        }

        node.Next = nodes[sum];
      }

      // keep track of the count
      var count = 0;

      // next, go through the n values
      for (var n = 2; n < TenMillion; ++n)
      {
        var text = n.ToString().ToCharArray();
        var sum = 0;
        foreach (var c in text)
        {
          switch (c)
          {
            case '0':
              sum += 0;
              break;
            case '1':
              sum += 1;
              break;
            case '2':
              sum += 4;
              break;
            case '3':
              sum += 9;
              break;
            case '4':
              sum += 16;
              break;
            case '5':
              sum += 25;
              break;
            case '6':
              sum += 36;
              break;
            case '7':
              sum += 49;
              break;
            case '8':
              sum += 64;
              break;
            case '9':
              sum += 81;
              break;
          }
        }

        // retrieve the node
        var node = nodes[sum];
        if (node.HitsOne) { continue; }
        ++count;
      }

      // return the count
      return count;
    }

    #region Nested type: ChainNode

    protected class ChainNode
    {
      public ChainNode Next;
      public int Value;
      private Lazy<bool> hitsOne;

      public ChainNode()
      {
        hitsOne = new Lazy<bool>(GetHitOne);
      }

      public bool HitsOne
      {
        get { return hitsOne.Value; }
      }

      private bool GetHitOne()
      {
        if (Value == 1) { return true; }
        if (Value == 89) { return false; }
        return Next.HitsOne;
      }
    }

    #endregion
  }
}