using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class SudokuGrid
  {
    private int[,] grid = new int[9, 9];

    public int this[int c, int r]
    {
      get { return grid[c, r]; }
    }

    public SudokuGrid(string[] rows)
    {
      for (int r = 0; r < rows.Length; ++r)
      {
        var row = rows[r];
        for (int c = 0; c < row.Length; ++c)
        {
          grid[c, r] = row[c] - 48;
        }
      }
    }

    public void Solve()
    {
      // create a list to hold the candidates for each cell in the grid
      var candidates = new HashSet<int>[9, 9];

      // go through the grid and fill the candidates
      for (int r = 0; r < 9; ++r)
      {
        for (int c = 0; c < 9; ++c)
        {
          // create a set to hold the candidates
          var set = new HashSet<int>();

          // get the current value of the cell
          int cell = grid[c, r];
          if (cell == 0)
          {
            // the cell has no value yet, so let's go through and
            // find the possibilities for this cell
            for (int n = 1; n <= 9; ++n)
            {
              // is it possible for this cell to have 'n' has the value?
              if (IsAvailable(n, c, r))
              {
                set.Add(n);
              }
            }
          }

          // add the candidates for this cell
          candidates[c, r] = set;
        }
      }

      while (true)
      {
        // use the simple method
        bool candidateFound = false;
        for (int r = 0; r < 9; ++r)
        {
          for (int c = 0; c < 9; ++c)
          {
            // retrieve the candidates for this cell
            var set = candidates[c, r];
            if (set.Count == 1)
            {
              // a candidate has been found!
              candidateFound = true;

              // retrieve the value
              int v = set.First();

              // remove this value from the set
              set.Remove(v);

              // set this value as the grid value
              grid[c, r] = v;

              // also, remove this value from all the related cells
              // get the related indices
              var areas = GetAreas(c, r);
              foreach (var index in areas)
              {
                // retrieve the col,row indices
                int col = index[0];
                int row = index[1];
                candidates[col, row].Remove(v);
              }
            }
          }
        }

        // did we find a candidate?
        if (!candidateFound)
        {
          // we didn't find a candidate! Did we solve the grid?
          if (IsFilled() && IsSolved())
          {
            break;
          }
          else
          {
            // we didn't solve the grid, let's employ guessing
            GuessSolve();
            break;
          }
        }
      }
    }

    private IEnumerable<int[]> GetRow(int c, int r)
    {
      for (int row = 0; row < 9; ++row)
      {
        // we skip our current row
        if (row == r) continue;

        // get the value in this column
        yield return new int[] { c, row };
      }
    }

    private IEnumerable<int[]> GetColumn(int c, int r)
    {
      for (int col = 0; col < 9; ++col)
      {
        // we skip our current column
        if (col == c) continue;

        // get the value in this row
        yield return new int[] { col, r };
      }
    }

    private IEnumerable<int[]> GetBox(int c, int r)
    {
      int startCol = c - (c % 3);
      int startRow = r - (r % 3);
      int endCol = startCol + 3;
      int endRow = startRow + 3;

      for (int row = startRow; row < endRow; ++row)
      {
        for (int col = startCol; col < endCol; ++col)
        {
          // we skip our current cell
          if ((col == c) && (row == r)) continue;

          // get the value in this cell
          yield return new int[] { col, row };
        }
      }
    }

    private IEnumerable<int[]> GetAreas(int c, int r)
    {
      foreach (var v in GetRow(c, r)) yield return v;
      foreach (var v in GetColumn(c, r)) yield return v;
      foreach (var v in GetBox(c, r)) yield return v;
    }

    private bool IsAvailable(int n, int c, int r)
    {
      // create a variable to say if 'n is available
      bool available = true;

      // get the related indices
      var areas = GetAreas(c, r);
      foreach (var index in areas)
      {
        // retrieve the col,row indices
        int col = index[0];
        int row = index[1];

        // get the value of this cell
        var v = grid[col, row];

        // as long as it isn't n, then n is available
        available &= (v != n);

        // if no longer available, then stop
        if (!available) break;
      }

      // return if we're available
      return available;
    }

    private bool IsFilled()
    {
      for (int r = 0; r < 9; ++r)
      {
        for (int c = 0; c < 9; ++c)
        {
          if (grid[c, r] == 0) return false;
        }
      }

      // if we got here, it means that every value only appeared once in every row,col
      // and 3x3 grid
      return true;
    }

    private bool IsSolved()
    {
      for (int r = 0; r < 9; ++r)
      {
        for (int c = 0; c < 9; ++c)
        {
          var areas = from area in GetAreas(c, r)
                      group area by grid[area[0], area[1]] into areaGroup
                      select areaGroup.Count();
          if (areas.Any(a => a != 3))
          {
            return false;
          }
        }
      }
      return true;
    }

    private void GuessSolve()
    {
      var indices = new List<int[]>();
      for (int r = 0, i = 0; r < 9; ++r)
      {
        for (int c = 0; c < 9; ++c, ++i)
        {
          if (grid[c, r] == 0)
          {
            indices.Add(new int[] { c, r });
          }
        }
      }

      for (int i = 0; i < indices.Count; ++i)
      {
        // retrieve the row and column for this iteration
        var arr = indices[i];
        var c = arr[0];
        var r = arr[1];

        // place n
        var n = grid[c, r] + 1;
        while (n < 10 && !IsAvailable(n, c, r)) ++n;

        // if n is 10, then undo
        if (n == 10)
        {
          grid[c, r] = 0;
          i -= 2;
        }
        else
        {
          grid[c, r] = n;
        }
      }
    }
  }
}
