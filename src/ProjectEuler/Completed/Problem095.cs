using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem095 : Problem092
  {
    public override int Number
    {
      get { return 95; }
    }

    public override object Solve()
    {
      const int Max = 1000000;

      // create a table for the chain
      var nodes = new ChainNode[Max + 1];
      nodes[0] = new ChainNode {Value = 0};

      // create a table of the factors for all the possible N values
      var table = new int[Max + 1];
      table[0] = 0;

      // fill the tables
      for (var n = 1; n <= Max; ++n)
      {
        var sum = 0;
        var factors = n.Factors();
        foreach (var f in factors)
        {
          if (f == n) { continue; }
          sum += f;
        }

        table[n] = sum;
        nodes[n] = new ChainNode {Value = n};
      }

      // go through the numbers and setup the chain
      for (var i = 1; i <= Max; ++i)
      {
        var j = table[i];
        if (j <= Max)
        {
          nodes[i].Next = nodes[j];
        }
      }

      // clear the table
      Array.Clear(table, 0, table.Length);
      table = null;

      // create a list to hold the chain
      var maxChain = new List<int>();

      // go through and generate the chain
      for (var N = 28; N <= Max; ++N)
      {
        // create a dictionary to hold the chain
        var chain = new Dictionary<int, bool>();

        // create the values
        var value = N;
        var node = nodes[N];

        do
        {
          // check to see if we've added this value
          if (chain.ContainsKey(value))
          {
            chain.Clear();
            break;
          }

          // add the value
          chain[value] = true;

          // retrieve the next node
          node = node.Next;
          if (node == null)
          {
            chain.Clear();
            break;
          }

          // set the value
          value = node.Value;
          if (value == N)
          {
            // this is an amicable chain, so stop
            break;
          }
        } while (true);

        // if the chain is longer, than save it
        if (chain.Count > maxChain.Count)
        {
          maxChain = chain.Keys.ToList();
        }
      }

      return maxChain.PopMin();
    }
  }
}