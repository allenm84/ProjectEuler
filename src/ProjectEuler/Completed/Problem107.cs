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
  public class Problem107 : EulerProblem
  {
    private class Node
    {
      public int ID;
      public override int GetHashCode()
      {
        return ID;
      }
    }

    private class Edge
    {
      public Node Node1;
      public Node Node2;
      public int Value;

      public bool Connects(Node n1, Node n2)
      {
        return
          (Node1 == n1 && Node2 == n2) ||
          (Node1 == n2 && Node2 == n1);
      }
    }

    public override int Number { get { return 107; } }

    public override object Solve()
    {
      // retrieve the matrix
      var matrix = Resources.Problem107Data
        .SplitBy('\r', '\n')
        .Select(l => l.SplitBy(','))
        .ToArray();

      // we now have a matrix that is accessed first by getting the row
      // and then column. So matrix[r][c].  This means that the length of
      // the matrix (the number of rows), will tell us how many nodes we need
      var nodes = Enumerable
        .Range(0, matrix.Length)
        .Select(i => new Node { ID = i })
        .ToArray();

      // create a table of the edges
      var edges = new List<Edge>();

      // now that we have the nodes, we need to find all of the connections
      // this node has, and add them to the edges
      for (int i = 0; i < nodes.Length; ++i)
      {
        var n1 = nodes[i];
        for (int j = 0; j < nodes.Length; ++j)
        {
          if (i == j) continue;
          var n2 = nodes[j];

          var cell = matrix[i][j];
          if (cell != "-")
          {
            // create an edge
            var edge = new Edge
            {
              Node1 = n1,
              Node2 = n2,
              Value = int.Parse(cell),
            };

            // are there any edges that connect
            if (!edges.Any(e => e.Connects(n1, n2)))
            {
              edges.Add(edge);
            }
          }
        }
      }

      // calculate the sum
      var sum = edges.Sum(e => e.Value);

      // sort the edges by value descending
      edges.Sort((a, b) => b.Value.CompareTo(a.Value));

      // go through the edges
      for(int i = 0; i < edges.Count; ++i)
      {
        // retrieve the edge
        var temp = edges.Pop(i);

        // is there a path that exists between the nodes
        // of the edge?
        if (!PathExists(nodes, edges, temp.Node1, temp.Node2))
        {
          // insert the node at i
          edges.Insert(i, temp);
        }
        else
        {
          // move back
          --i;
        }
      }

      // return the difference
      int newSum = edges.Sum(e => e.Value);
      return string.Format("{0} - {1} = {2}", sum, newSum, sum - newSum);
    }

    private bool PathExists(Node[] nodes, List<Edge> edges, Node n1, Node n2)
    {
      var visited = new HashSet<Node>();
      var dist = new Dictionary<Node, int>();
      var previous = new Dictionary<Node, Node>();

      // Initializations
      for (int i = 0; i < nodes.Length; ++i)
      {
        // Unknown distance function from source to v
        dist[nodes[i]] = int.MaxValue;

        // Previous node in optimal path from source
        previous[nodes[i]] = null;
      }

      dist[n1] = 0;
      var Q = nodes.ToList();
      while (Q.Count > 0)
      {
        var u = Q[0];
        var minIndex = 0;
        for (int m = 1; m < Q.Count; ++m)
        {
          var c = Q[m];
          if (dist[c] < dist[u])
          {
            u = c;
            minIndex = m;
          }
        }

        // all remaining vertices are inaccessible from source
        if (dist[u] == int.MaxValue) break;

        // remove u from Q ;
        Q.RemoveAt(minIndex);

        // If we are only interested in a shortest path between vertices source and 
        // target, we can terminate the search at line 13 if u = target
        if (u == n2) return true;

        // add u to the visited
        visited.Add(u);

        // for each neighbor v of u:
        // where v has not yet been removed from Q.
        var neighbors = edges
          .Where(e => e.Node1 == u || e.Node2 == u)
          .Select(e => (e.Node1 == u ? e.Node2 : e.Node1));
        foreach (var v in neighbors)
        {
          if (visited.Contains(v)) continue;
          int alt = dist[u] + Math.Abs(v.ID - u.ID);
          if (alt < dist[v])
          {
            dist[v] = alt;
            previous[v] = u;
          }
        }
      }

      // check to see if a path exists
      var t = n2;
      while (previous[t] != null)
      {
        t = previous[t];
      }
      return (t == n1);
    }
  }
}
