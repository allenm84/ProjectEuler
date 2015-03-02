using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem068 : EulerProblem
  {
    public override int Number
    {
      get { return 68; }
    }

    public override object Solve()
    {
      // create a variable to hold the index
      var z = 0;

      // create the n-gon graph array
      var graph = Enumerable.Repeat(0, 5).Select(i => new GonNode()).ToArray();

      // set up the indices. The indices indicate which number to retrieve from
      // the set, and in what order. Starting with the external node, then moving
      // inwards.
      graph[z++].Indices = new[] {7, 1, 0};
      graph[z++].Indices = new[] {6, 0, 4};
      graph[z++].Indices = new[] {5, 4, 3};
      graph[z++].Indices = new[] {9, 3, 2};
      graph[z++].Indices = new[] {8, 2, 1};

      // create a function that will fill a gon node
      Action<GonNode, IList<byte>> fillNode = (node, data) =>
      {
        node.Value1 = (byte)(data[node.Indices[0]] + 1);
        node.Value2 = (byte)(data[node.Indices[1]] + 1);
        node.Value3 = (byte)(data[node.Indices[2]] + 1);
      };

      // link the graph together
      for (var i = 0; i < graph.Length; ++i)
      {
        graph[i].Next = graph[(i + 1) % graph.Length];
      }

      // generate the digits allowed in this graph
      var digits = Enumerable.Range(0, 10).Select(i => (byte)i).ToList();

      // create a table to hold the matches
      long maximum = 0;

      // create a permutation generator
      var generator = new Permutations<byte>(digits);

      // go through the sets
      foreach (IList<byte> set in generator)
      {
        // fill the nodes
        foreach (var n in graph)
        {
          fillNode(n, set);
        }

        // retrieve the match sum
        var target = graph[0].Sum;

        // verify that each node has the same value
        if (!graph.All(g => g.Sum == target)) { continue; }

        // order the graph by value1 (the external value)
        var min = graph.OrderBy(g => g.Value1).First();

        // create a list to hold the values
        var bytes = new List<byte>();

        // iterate the tree
        var current = min;
        do
        {
          bytes.Add(current.Value1);
          bytes.Add(current.Value2);
          bytes.Add(current.Value3);
          current = current.Next;
        } while (current != min);

        // create a string from the bytes
        var concat = string.Concat(bytes);

        // skip if greater than 16
        if (concat.Length > 16) { continue; }

        // create a long from the values
        var match = Convert.ToInt64(concat);

        // did we already generate this value
        maximum = Math.Max(match, maximum);
      }

      // return the maximum
      return maximum;
    }

    #region Nested type: GonNode

    protected class GonNode
    {
      public byte Value1 { get; set; }
      public byte Value2 { get; set; }
      public byte Value3 { get; set; }
      public int[] Indices { get; set; }
      public GonNode Next { get; set; }

      public int Sum
      {
        get { return Value1 + Value2 + Value3; }
      }
    }

    #endregion
  }
}