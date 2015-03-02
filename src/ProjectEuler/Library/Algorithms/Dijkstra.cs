using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public enum Movement
  {
    Left,
    Right,
    Up,
    Down,
  };

  public class Index
  {
    public int C;
    public int R;

    public Index(int c, int r)
    {
      C = c;
      R = r;
    }

    public bool Valid(int cols, int rows)
    {
      return (-1 < C && C < cols) && (-1 < R && R < rows);
    }

    public static Index Offset(int c, int r, int[] offset)
    {
      return new Index(c + offset[0], r + offset[1]);
    }
  }

  public class DijkstraNode
  {
    private static int[][] MovementOffsets = new int[4][];

    private DijkstraNode[] neighbors;

    static DijkstraNode()
    {
      MovementOffsets[(int)Movement.Down] = new[] {0, 1};
      MovementOffsets[(int)Movement.Left] = new[] {-1, 0};
      MovementOffsets[(int)Movement.Right] = new[] {1, 0};
      MovementOffsets[(int)Movement.Up] = new[] {0, -1};
    }

    public DijkstraNode(int c, int r, int value)
    {
      Column = c;
      Row = r;
      Value = value;
    }

    public DijkstraNode[] Neighbors
    {
      get { return neighbors; }
    }

    public int Row { get; private set; }
    public int Column { get; private set; }
    public int Value { get; private set; }

    public int Distance { get; set; }
    public DijkstraNode Previous { get; set; }
    public bool Removed { get; set; }

    public void Reset(DijkstraNode[,] nodes, Movement[] moves, int cols, int rows)
    {
      Distance = int.MaxValue;
      Previous = null;
      Removed = false;

      if (neighbors != null)
      {
        Array.Clear(neighbors, 0, neighbors.Length);
        neighbors = null;
      }

      neighbors = moves
        .Select(m => Index.Offset(Column, Row, MovementOffsets[(int)m]))
        .Where(i => i.Valid(cols, rows))
        .Select(i => nodes[i.C, i.R])
        .ToArray();
    }
  }

  public static class Dijkstra
  {
    public static DijkstraNode[,] Solve(int[,] values, Movement[] moves, Index start)
    {
      var cols = values.GetLength(0);
      var rows = values.GetLength(1);

      var Q = new List<DijkstraNode>();
      var nodes = new DijkstraNode[cols, rows];

      for (var r = 0; r < rows; ++r)
      {
        for (var c = 0; c < cols; ++c)
        {
          var node = new DijkstraNode(c, r, values[c, r]);
          nodes[c, r] = node;
          Q.Add(node);
        }
      }

      foreach (var node in Q)
      {
        node.Reset(nodes, moves, cols, rows);
      }

      var source = nodes[start.C, start.R];
      source.Distance = 0;

      while (Q.Count > 0)
      {
        // u := vertex in Q with smallest dist[] ;
        var u = PopMin(ref Q);

        // if dist[u] = infinity:
        if (u.Distance == int.MaxValue)
        {
          // all remaining vertices are inaccessible from source
          break;
        }

        // remove u from Q;
        u.Removed = true;

        // go through the neighbors
        foreach (var v in u.Neighbors)
        {
          if (v.Removed) { continue; }

          var alt = u.Distance + (u.Value + v.Value);
          if (alt < v.Distance)
          {
            v.Distance = alt;
            v.Previous = u;
          }
        }
      }

      return nodes;
    }

    private static DijkstraNode PopMin(ref List<DijkstraNode> Q)
    {
      var min = Q[0];
      var minIndex = 0;
      for (var i = 1; i < Q.Count; ++i)
      {
        var node = Q[i];
        if (node.Distance < min.Distance)
        {
          min = node;
          minIndex = i;
        }
      }
      Q.RemoveAt(minIndex);
      return min;
    }

    public static IEnumerable<DijkstraNode> GetPath(DijkstraNode[,] nodes, Index start, Index end)
    {
      var source = nodes[start.C, start.R];
      var target = nodes[end.C, end.R];

      while (target.Previous != null)
      {
        yield return target;
        target = target.Previous;
      }

      yield return source;
    }
  }
}