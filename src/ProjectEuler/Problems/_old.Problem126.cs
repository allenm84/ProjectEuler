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
using System.Runtime.InteropServices;

namespace ProjectEuler
{
  public class Problem126 : EulerProblem
  {
    public override int Number { get { return 126; } }

    public override string Solve()
    {
      // from the limit, generate a list of cubes and their formulas
      const int Limit = 10;
      var formulas = GenerateFormulas(Limit).AsParallel().ToList();
      return "";
    }

    private IEnumerable<CubeFormula> GenerateFormulas(int limit)
    {
      var should = new DistinctInt3Iter();
      for (int l = 1; l <= limit; ++l)
      {
        for (int w = 1; w <= limit; ++w)
        {
          for (int h = 1; h <= limit; ++h)
          {
            // if we should not iter, then continue
            if (!should.Iter(l, w, h)) continue;

            // create a list to store the number in each layer
            var counts = new List<int>();

            // now that we're here, lets generate the formula
            // calculate the number of cubes in the first layer
            int firstLayer = ((l * h) << 1) + ((l * w) << 1) + ((w * h) << 1);

            // add the number to the list
            counts.Add(firstLayer);

            // create the first two layers
            var solid = Cube.CreateCuboid(l, w, h);
            var layer = Cube.CreateLayer(solid);
            solid.RemoveAll(c => !c.IsAtLeastOneFaceFree);
            solid.AddRange(layer);
            layer = Cube.CreateLayer(solid);

            // now, we know how many cubes are in the second layer
            int layer0 = firstLayer;
            int layer1 = layer.Count;

            // add the count to the list
            counts.Add(layer1);

            // calculate the diff
            int diff = (layer1 - layer0) + 8;

            // clean up
            solid.Clear();
            layer.Clear();
            solid = null;
            layer = null;

            // add the third layer
            counts.Add(layer1 + diff);

            // now we have the counts for the first three layers, lets generate
            // the formula
            var coeffs = Formula.FindFormula(counts);
            Console.WriteLine("({0} x {1} x {2}):", l, w, h);
            Console.WriteLine("* {0}", string.Join(", ", coeffs));

            int x = ((h - (2 - w)) * 4) + ((l - 1) * 4);
            int y = (((w * 2) - 2) * (h + ((l - 1) * 2))) + ((2 - w) * 2);

            Console.WriteLine("* 4, {0}, {1}", x, y);

            yield return new CubeFormula
            {
              B = (int)coeffs[1],
              C = (int)coeffs[2],
              Height = h,
              Length = l,
              Width = w,
            };
          }
        }
      }
    }

    #region Helper Classes

    private struct MatrixSize
    {
      private int mRows;

      public int Rows
      {
        get { return mRows; }
        set { mRows = value; }
      }

      private int mColumns;

      public int Columns
      {
        get { return mColumns; }
        set { mColumns = value; }
      }

      public MatrixSize(int rows, int columns)
      {
        mRows = rows;
        mColumns = columns;
      }
    }

    private class Matrix
    {
      /// <summary>
      /// The jagged array containing the matrix values
      /// </summary>
      private decimal[][] mMatrix;

      /// <summary>
      /// Returns the value inside this matrix at the specified row and column
      /// </summary>
      /// <param name="row">The row where the value is located</param>
      /// <param name="column">The column where the value is located</param>
      /// <returns>The indexed value</returns>
      public decimal this[int row, int column]
      {
        get { return mMatrix[row][column]; }
        set { mMatrix[row][column] = value; }
      }

      /// <summary>
      /// Returns a row vector at the specified row index
      /// </summary>
      /// <param name="row">The row index where the row is located</param>
      /// <returns>The indexed row vector</returns>
      public RowVector this[int row]
      {
        get { return new RowVector(mMatrix[row]); }
        set { mMatrix[row] = value.ToArray(); }
      }

      /// <summary>
      /// The number of columns this matrix contains
      /// </summary>
      public int ColumnCount
      {
        get { return mMatrix[0].Length; }
      }

      /// <summary>
      /// The number of rows this matrix contains
      /// </summary>
      public int RowCount
      {
        get { return mMatrix.Length; }
      }

      /// <summary>
      /// Initializes this matrix
      /// </summary>
      /// <param name="p">The value each matrix value should be initialized as</param>
      /// <param name="r">The number of rows</param>
      /// <param name="c">The number of columns</param>
      private void InitializeMatrix(decimal p, int r, int c)
      {
        // create a temporary list of decimal arrays to store the matrix
        List<decimal[]> temp = new List<decimal[]>();

        // loop through the desired number of rows
        for (int i = 0; i < r; ++i)
        {
          // create a new decimal array that has the desired number of columns
          decimal[] arr = new decimal[c];

          // loop through the desired number of columns
          for (int j = 0; j < c; ++j)
          {
            // set the value inside the array to the desired initial value passed in
            arr[j] = p;
          }

          // add the decimal array
          temp.Add(arr);
        }

        // initialize this matrix from the temporary list of decimal arrays
        mMatrix = temp.ToArray();
      }

      /// <summary>
      /// Combines two matrices together either through subtraction or addition. This assumes
      /// that the two matrices passed in are the same size.
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <param name="subtract">A boolean indicating whether to subtract or add</param>
      /// <returns>A CMatrix containing the values of the two combined matrices</returns>
      private static Matrix CombineMatrices(Matrix matrix1, Matrix matrix2, bool subtract)
      {
        // create a temporary matrix to hold the combined values
        Matrix _temp = new Matrix(matrix1.RowCount, matrix2.ColumnCount);

        // cycle through the rows of the matrices
        for (int i = 0; i < matrix1.RowCount; ++i)
        {
          // cycle through the columns of the matrices
          for (int j = 0; j < matrix2.ColumnCount; ++j)
          {
            // combine the values into the temp matrix
            _temp[i, j] = (subtract ?
                matrix1[i, j] - matrix2[i, j] :
                matrix1[i, j] + matrix2[i, j]);
          }
        }

        // return the temporary matrix containing the combined values
        return _temp;
      }

      /// <summary>
      /// Create a new rows by columns Matrix object
      /// </summary>
      /// <param name="rows"></param>
      /// <param name="columns"></param>
      public Matrix(MatrixSize size)
      {
        int rows = size.Rows;
        int columns = size.Columns;

        List<decimal[]> matrix = new List<decimal[]>();
        for (int r = 0; r < rows; ++r)
        {
          // add a new row with the specified amount of columns
          matrix.Add(new decimal[columns]);
        }
        mMatrix = matrix.ToArray();
      }

      /// <summary>
      /// Create a new Matrix object from a list of deciaml parameters
      /// </summary>
      /// <param name="arguments">
      /// The parameters to create the matrix. Passing in (3) would create a 3x3 matrix. Passing in (3,5)
      /// would create a 3x5 matrix with 0 in each element of the matrix. Passing in (3,4,1.5m) would 
      /// create a 3x4 matrix with 1.5 in each element of the matrix. There is a minimum of 1 and a maximum 
      /// of 3 arguments accepted.
      /// </param>
      public Matrix(params decimal[] arguments)
      {
        // create two variables to hold the desired number of rows
        int rows, columns = 0;

        // if there is only one argument
        if (arguments.Length == 1)
        {
          // initialize the row and column count
          rows = columns = (int)arguments[0];
        }
        // else if there is 2 or 3 arguments
        else if (arguments.Length == 2 || arguments.Length == 3)
        {
          // initialize the row count
          rows = (int)arguments[0];

          // initialize the column count
          columns = (int)arguments[1];
        }
        // else there are more than 3 or less than 1 arguments
        else
        {
          // throw an exception
          throw new ArgumentException("Need at least 1 argument and no more than 3 arguments to create a CMatrix");
        }

        // create a variable to hold what value to initialize the matrix to
        decimal val = (arguments.Length == 3 ? arguments[2] : 0);

        // initialize this matrix
        InitializeMatrix(val, rows, columns);
      }

      /// <summary>
      /// Create a new Matrix object from a the list of decimal arrays
      /// </summary>
      /// <param name="matrix">
      /// The list of decimal arrays. This will check to make sure that each decimal array inside
      /// the array is the same size. This also assumes that the first deciaml array in the list
      /// is the correct size.
      /// </param>
      public Matrix(List<decimal[]> matrix)
      {
        // do an integrity check on the matrix
        int numColumns = matrix[0].Length;

        // cycle through the remaning decimal arrays
        for (int i = 1; i < matrix.Count; ++i)
        {
          // if the decimal array at this position isn't the correct size
          if (matrix[i].Length != numColumns)
          {
            // throw an exception
            throw new ArgumentException("The passed in list has mismatching columns; this isn't a valid matrix.");
          }
        }

        // initialize this matrix
        mMatrix = matrix.ToArray();
      }

      /// <summary>
      /// Extract a row from this matrix as a decimal array at the specified index
      /// </summary>
      /// <param name="row">The desired row index</param>
      /// <returns>A decimal array containg the values at the specified index</returns>
      public decimal[] GetRow(int row)
      {
        // return the decimal array at the passed in row
        return mMatrix[row];
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="rowIdx"></param>
      /// <param name="row"></param>
      public void SetRow(int rowIdx, decimal[] row)
      {
        // set the row
        mMatrix[rowIdx] = row;
      }

      /// <summary>
      /// Extract a column from this matrix as a decimal array at the specified index
      /// </summary>
      /// <param name="column">The desired column index</param>
      /// <returns>A decimal array containg the values at the specified index</returns>
      public decimal[] GetColumn(int column)
      {
        // create a new decimal array big enough to hold all the column values
        decimal[] col = new decimal[RowCount];

        // cycle through each row of this matrix
        for (int i = 0; i < RowCount; ++i)
        {
          // set the array value to be the column value at this row
          col[i] = this[i, column];
        }

        // return the decimal array
        return col;
      }

      /// <summary>
      /// Row augment this matrix with another matrix
      /// </summary>
      /// <param name="matrix">The matrix to row augment with this matrix</param>
      /// <returns>This matrix row augmented with the passed in matrix</returns>
      public Matrix RowAugment(Matrix matrix)
      {
        // if the number of columns don't match
        if (matrix.ColumnCount != ColumnCount)
        {
          // throw an exception
          throw new ArgumentException("Can't row augment the two matrices; the column count does not match");
        }

        // create a list of decimal arrays containing this matrix
        List<decimal[]> retval = new List<decimal[]>(mMatrix);

        // cycle through the rows of the passed in matrix
        for (int r = 0; r < matrix.RowCount; ++r)
        {
          // add the row of the passed in matrix to the list of decimal arrays
          retval.Add(matrix.GetRow(r));
        }

        // return the row augmented matrix
        return new Matrix(retval);
      }

      /// <summary>
      /// Column augment this matrix with another matrix
      /// </summary>
      /// <param name="matrix">The matrix to column augment with this matrix</param>
      /// <returns>This matrix column augmented with the passed in matrix</returns>
      public Matrix ColumnAugment(Matrix matrix)
      {
        // if the number of rows don't match
        if (matrix.RowCount != RowCount)
        {
          // throw an exception
          throw new ArgumentException("Can't column augment the two matrices; the row count does not match");
        }

        // create a list of decimal arrays to hold the values
        List<decimal[]> retval = new List<decimal[]>();

        // create a temporary list of decimals to hold a row of the augmented matrix
        List<decimal> newRow = new List<decimal>();

        // cycle through this matrix
        for (int i = 0; i < mMatrix.Length; ++i)
        {
          // clear the row
          newRow.Clear();

          // add all of the values at this row
          newRow.AddRange(mMatrix[i]);

          // cycle through the values of the passed in matrix
          for (int c = 0; c < matrix.ColumnCount; ++c)
          {
            // add the value at the ith row and the cth column to the row
            newRow.Add(matrix[i, c]);
          }

          // add the row to the list of decimal arrays
          retval.Add(newRow.ToArray());
        }

        // return the column augmented matrix
        return new Matrix(retval);
      }

      /// <summary>
      /// Pivot or Swap two rows inside this matrix
      /// </summary>
      /// <param name="row1">The row index of the row to swap</param>
      /// <param name="row2">The row index of the row to swap with</param>
      public void Pivot(int row1, int row2)
      {
        // if the row indexes aren't the same
        if (row1 != row2)
        {
          // if the row indexes are invalid
          if (row1 >= RowCount || row2 >= RowCount)
          {
            // throw an exception
            throw new ArgumentOutOfRangeException("The row indices to pivot can't exceed the maximum row index of the matrix");
          }

          // create a temporary decimal array containg the row at row1
          decimal[] temp = mMatrix[row1];

          // set the row at row1 to row2
          mMatrix[row1] = mMatrix[row2];

          // set the row at ro2 to the temp decimal array (which is row1). This is a standard swap
          mMatrix[row2] = temp;
        }
      }

      /// <summary>
      /// Transpose this matrix
      /// </summary>
      /// <returns>This matrix tranposed</returns>
      public Matrix Transpose()
      {
        // create a list of decimal arrays to hold this matrix
        List<decimal[]> transposed = new List<decimal[]>();

        // cycle through this matrix's columns
        for (int c = 0; c < ColumnCount; ++c)
        {
          // add the columns of this matrix represented as a row
          transposed.Add(GetColumn(c));
        }

        // return the transposed matrix
        return new Matrix(transposed);
      }

      /// <summary>
      /// Creates a deep copy of this matrix
      /// </summary>
      /// <returns>A deep copy of this matrix</returns>
      public Matrix Clone()
      {
        // create a new list from this matrix's values
        List<decimal[]> matrix = new List<decimal[]>(mMatrix);

        // return a new matrix from the list of the values
        return new Matrix(matrix);
      }

      /// <summary>
      /// This is an implicit operator which creates a CMatrix from a list of decimal arrays
      /// </summary>
      /// <param name="matrix">The list of decimal arrays</param>
      /// <returns>A new matrix from the list of decimal arrays</returns>
      public static implicit operator Matrix(List<decimal[]> matrix)
      {
        // return a new matrix
        return new Matrix(matrix);
      }

      /// <summary>
      /// This is an implicit operator which creates a CMatrix from a jagged array of decimals
      /// </summary>
      /// <param name="matrix">The jagged array of decimals</param>
      /// <returns>A new matrix from the jagged array decimals</returns>
      public static implicit operator Matrix(decimal[][] matrix)
      {
        // return a new matrix
        return new Matrix(new List<decimal[]>(matrix));
      }

      /// <summary>
      /// This is an implicit operator which creates a CMatrix from a string array
      /// </summary>
      /// <param name="matrix">
      /// The string array containing the specifics of the matrix. The string array must be
      /// in the following format: string[]{ n, m, data[...]} where n and m represent the 
      /// dimensions of an n by m matrix. All values inside the string array must be able to
      /// be converted to the decimal format
      /// </param>
      /// <returns>A new matrix from the string array</returns>
      public static implicit operator Matrix(string[] matrix)
      {
        // if the length of the string array is less than 3 (covering the case where the values are 1,1,n)
        if (matrix.Length < 3)
        {
          // throw an exception
          throw new ArgumentException("The passed in string array must match the format {n,m,data[...]} where n and m represent the dimensions of an nxm matrix");
        }

        // create variables to hold the number of rows and columns
        int rows, columns = 0;

        try
        {
          // initialize the number of rows
          rows = Convert.ToInt32(matrix[0]);

          // initialize the number of columns
          columns = Convert.ToInt32(matrix[1]);
        }
        catch (FormatException e)
        {
          // if either couldn't be converted, throw an exception
          throw new ArgumentException(string.Format("The passed in string array had the following error for the row or column number: {0}", e.Message));
        }

        // create a value to represented the number of entires inside the matrix based on the rows and columns
        int numEntries = rows * columns;

        // if the length of the passed in string array isn't the correct size
        if (matrix.Length < (numEntries + 2) || matrix.Length > (numEntries + 2))
        {
          // throw an exception
          throw new ArgumentException("The passed in string array doesn't contain the correct number of values to construct a valid matrix");
        }

        // create a list of decimals to hold the matrix values
        List<decimal[]> mat = new List<decimal[]>();

        // create a list of decimals to hold the values of a matrix row
        List<decimal> row = new List<decimal>();

        // cycle through the string array
        for (int i = 2; i < matrix.Length; ++i)
        {
          // get the value; no exception handling, leave it to the caller
          row.Add(Convert.ToInt64(matrix[i]));

          // if the row is the desired size
          if (row.Count == columns)
          {
            // add the row as an array of decimal values
            mat.Add(row.ToArray());

            // clear the row holder
            row.Clear();
          }
        }

        // return a new matrix
        return new Matrix(mat);
      }

      /// <summary>
      /// Subtract two matrices
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <returns>A matrix containing the the values of the two matrices subtracted</returns>
      public static Matrix operator -(Matrix matrix1, Matrix matrix2)
      {
        // if the matrices aren't the same size
        if ((matrix1.RowCount != matrix2.RowCount) || (matrix1.ColumnCount != matrix2.ColumnCount))
        {
          // throw an exception
          throw new ArgumentException("Can't perform subtraction; The matrices are not the same size");
        }

        // return the two matrices combined by subtraction
        return CombineMatrices(matrix1, matrix2, true);
      }

      /// <summary>
      /// Add two matrices
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <returns>A matrix containing the the values of the two matrices added</returns>
      public static Matrix operator +(Matrix matrix1, Matrix matrix2)
      {
        // if the matrices aren't the same size
        if ((matrix1.RowCount != matrix2.RowCount) || (matrix1.ColumnCount != matrix2.ColumnCount))
        {
          // throw an exception
          throw new ArgumentException("Can't perform addition; The matrices are not the same size");
        }

        // return the two matrices combined by addition
        return CombineMatrices(matrix1, matrix2, false);
      }

      /// <summary>
      /// Multiply two matrices
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <returns>A matrix containing the the values of the two matrices multiplied together</returns>
      public static Matrix operator *(Matrix matrix1, Matrix matrix2)
      {
        // if the two matrices aren't valid for multiplication
        if ((matrix1.ColumnCount != matrix2.RowCount))
        {
          // throw an exception
          throw new ArgumentException("Can't perform multiplication; Matrices must be in format (n by m) and (m by p)");
        }

        // create a temporary matrix big enough for the two matrices multiplied together
        Matrix _temp = new Matrix(matrix1.RowCount, matrix2.ColumnCount);

        // cycle through the columns
        for (int q = 0; q < matrix1.ColumnCount; ++q)
        {
          // cycle through the rows
          for (int r = 0; r < matrix1.RowCount; ++r)
          {
            // cycle through the values of the matrix
            for (int c = 0; c < matrix2.ColumnCount; ++c)
            {
              // multiply the appropriate values together and sum them together
              _temp[r, c] += (matrix1[r, q] * matrix2[q, c]);
            }
          }
        }

        // return the multiplied matrices together
        return _temp;
      }

      /// <summary>
      /// Checks to see if two matrices are equal
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <returns>A boolean indicating if the two matrices are equal</returns>
      public static bool operator ==(Matrix matrix1, Matrix matrix2)
      {
        // call the Equals function
        return matrix1.Equals(matrix2);
      }

      /// <summary>
      /// Checks to see if two matrices are not equal
      /// </summary>
      /// <param name="matrix1">The first matrix</param>
      /// <param name="matrix2">The second matrix</param>
      /// <returns>A boolean indicating if the two matrices are not equal</returns>
      public static bool operator !=(Matrix matrix1, Matrix matrix2)
      {
        // call the Equals function and negate the return value
        return !matrix1.Equals(matrix2);
      }

      /// <summary>
      /// Converts this matrix to a string
      /// </summary>
      /// <returns>A string representation of this matrix</returns>
      public override string ToString()
      {
        // create a string builder for faster appends
        StringBuilder sb = new StringBuilder();

        // cycle through the rows of this matrix
        for (int r = 0; r < RowCount; ++r)
        {
          // append the starting "[" for a row
          sb.Append("[");

          // cycle through the columns of this matrix
          for (int c = 0; c < ColumnCount; ++c)
          {
            // append on value and if applicable a "," and a tab
            sb.AppendFormat("{0}{1}", this[r, c], (c == ColumnCount - 1 ? string.Empty : ",\t"));
          }

          // append the ending "[" for a row
          sb.Append("]\n");
        }

        // return the string representation of this matrix
        return sb.ToString();
      }

      /// <summary>
      /// Checks to see if this matrix is equal to the passed in object
      /// </summary>
      /// <param name="obj">The object to check against</param>
      /// <returns>A boolean indicating if this is equal to the object</returns>
      public override bool Equals(object obj)
      {
        // if the passed in object isn't the same type
        if (!GetType().Equals(obj.GetType()))
        {
          // return false
          return false;
        }

        // cast the passed in object to a CMatrix
        Matrix matrix = (Matrix)obj;

        // if the row/column count isn't the same
        if (matrix.RowCount != RowCount || matrix.ColumnCount != ColumnCount)
        {
          // return false
          return false;
        }

        // cycle through the rows of the matrices
        for (int r = 0; r < RowCount; ++r)
        {
          // cycle through the columns of the matrices
          for (int c = 0; c < matrix.ColumnCount; ++c)
          {
            // if the values at the indexes aren't the same
            if (matrix[r, c] != this[r, c])
            {
              // return false
              return false;
            }
          }
        }

        // if this point is reached, return true
        return true;
      }

      /// <summary>
      /// Gets a hashcode to represent this matrix
      /// </summary>
      /// <returns>A hashcode of this matrix</returns>
      public override int GetHashCode()
      {
        // call the implementation from the jagged array
        return mMatrix.GetHashCode();
      }
    }

    private abstract class Vector : List<decimal>
    {
      /// <summary>
      /// Converts this vector to a CMatrix object
      /// </summary>
      /// <returns>A CMatrix representing this vector</returns>
      public abstract Matrix ToCMatrix();

      /// <summary>
      /// Combines two vectors together either through subtraction or addition. This assumes
      /// that the two vectors passed in are the same size.
      /// </summary>
      /// <param name="vector1">The first vector</param>
      /// <param name="vector2">The second vector</param>
      /// <param name="subtract">A boolean indicating whether to subtract or add</param>
      /// <returns>A decimal array containing the values of the two combined vectors</returns>
      protected static decimal[] CombineVectors(Vector vector1, Vector vector2, bool subtract)
      {
        // create a temporary array to hold the vectors
        decimal[] temp = new decimal[vector1.Count];

        // cycle through the values of the first vector
        for (int i = 0; i < vector1.Count; ++i)
        {
          // set the value at this location to the combined values of the vectos
          temp[i] = (subtract ?
              vector1[i] - vector2[i] :
              vector1[i] + vector2[i]);
        }

        // return the array
        return temp;
      }

      /// <summary>
      /// Creates a new vector
      /// </summary>
      /// <param name="vector">The values of the vector</param>
      public Vector(decimal[] vector)
      {
        this.AddRange(vector);
      }

      /// <summary>
      /// Scales this vector by a value
      /// </summary>
      /// <param name="value">The value to scale by</param>
      public void Scale(decimal value)
      {
        // cycle through the vector values
        for (int i = 0; i < Count; ++i)
        {
          // scale each value
          this[i] *= value;
        }
      }

      /// <summary>
      /// 
      /// </summary>
      public void Round()
      {
        for (int i = 0; i < this.Count; ++i)
        {
          this[i] = Math.Round(this[i]);
        }
      }
    }

    private class RowVector : Vector
    {
      /// <summary>
      /// Creates a new Row Vector from the values
      /// </summary>
      /// <param name="vector">A decimal array containing the values of the vector</param>
      public RowVector(decimal[] vector) : base(vector) { }

      /// <summary>
      /// Converts this row vector to a CMatrix
      /// </summary>
      /// <returns>The CMatrix object</returns>
      public override Matrix ToCMatrix()
      {
        // initialize a new list of decimal arrays
        List<decimal[]> retval = new List<decimal[]>();

        // add this row vector to the decimal array
        retval.Add(this.ToArray());

        // return a new constructed CMatrix from the list of decimal arrays
        return new Matrix(retval);
      }

      /// <summary>
      /// Converts this row vector to a string
      /// </summary>
      /// <returns>A string representation of this vector</returns>
      public override string ToString()
      {
        // create a string builder for fast appends
        StringBuilder sb = new StringBuilder();

        // append on the starting "["
        sb.Append("[");

        // cycle through this vector
        for (int i = 0; i < Count; ++i)
        {
          // append the value at this location and if applicable a "," and a tab
          sb.AppendFormat("{0}{1}", this[i], (i == Count - 1 ? string.Empty : ",\t"));
        }

        // append the ending "]"
        sb.Append("]");

        // return the appended string
        return sb.ToString();
      }

      /// <summary>
      /// Subtract two row vectors
      /// </summary>
      /// <param name="vect1">The first vector</param>
      /// <param name="vect2">The second vector</param>
      /// <returns>A vector containing the the values of the two vectors subtracted</returns>
      public static RowVector operator -(RowVector vect1, RowVector vect2)
      {
        // if the vectors aren't the same size
        if (vect1.Count != vect1.Count)
        {
          // throw an exception
          throw new ArgumentException("Can't perform subtraction; The vectors are not the same size");
        }

        // use the base CombineVectors method to subtract the vectors and return a new row vector
        return new RowVector(CombineVectors(vect1, vect2, true));
      }

      /// <summary>
      /// Add two row vectors
      /// </summary>
      /// <param name="vect1">The first vector</param>
      /// <param name="vect2">The second vector</param>
      /// <returns>A vector containing the the values of the two vectors added</returns>
      public static RowVector operator +(RowVector vect1, RowVector vect2)
      {
        // if the vectors aren't the same size
        if (vect1.Count != vect1.Count)
        {
          // throw an exception
          throw new ArgumentException("Can't perform addition; The vectors are not the same size");
        }

        // use the base CombineVectors method to add the vectors and return a new row vector
        return new RowVector(CombineVectors(vect1, vect2, false));
      }

      /// <summary>
      /// Scale a row vector by a factor
      /// </summary>
      /// <param name="factor">The factor to scale by</param>
      /// <param name="vector">The vector to scale</param>
      /// <returns>A scaled row vector</returns>
      public static RowVector operator *(decimal factor, RowVector vector)
      {
        // call the base scale method on the passed in vector
        vector.Scale(factor);

        // return the scaled vector
        return vector;
      }
    }

    private class ColumnVector : Vector
    {
      /// <summary>
      /// Creates a new Column Vector from the values
      /// </summary>
      /// <param name="vector">A decimal array containing the values of the vector</param>
      public ColumnVector(decimal[] vector) : base(vector) { }

      /// <summary>
      /// Converts this column vector to a CMatrix
      /// </summary>
      /// <returns>The CMatrix object</returns>
      public override Matrix ToCMatrix()
      {
        // create a new list of decimal arrays
        List<decimal[]> retval = new List<decimal[]>();

        // cycle through the values of this vector
        for (int i = 0; i < Count; ++i)
        {
          // add a new decimal array containing only the value at this location
          retval.Add(new decimal[] { this[i] });
        }

        // return a new constructed CMatrix from the list of decimal arrays
        return new Matrix(retval);
      }

      /// <summary>
      /// Converts this column vector to a string
      /// </summary>
      /// <returns>A string representation of this vector</returns>
      public override string ToString()
      {
        // create a string builder for fast appends
        StringBuilder sb = new StringBuilder();

        // cycle through this vector
        for (int i = 0; i < Count; ++i)
        {
          // append on one row for this value and if applicable a newline
          sb.AppendFormat("[{0}]{1}", this[i], (i == Count - 1 ? string.Empty : "\n"));
        }

        // return the appended string
        return sb.ToString();
      }

      /// <summary>
      /// Subtract two column vectors
      /// </summary>
      /// <param name="vect1">The first vector</param>
      /// <param name="vect2">The second vector</param>
      /// <returns>A vector containing the the values of the two vectors subtracted</returns>
      public static ColumnVector operator -(ColumnVector vect1, ColumnVector vect2)
      {
        // if the vectors aren't the same size
        if (vect1.Count != vect1.Count)
        {
          // throw an exception
          throw new ArgumentException("Can't perform subtraction; The vectors are not the same size");
        }

        // use the base CombineVectors method to subtract the vectors and return a new row vector
        return new ColumnVector(CombineVectors(vect1, vect2, true));
      }

      /// <summary>
      /// Add two column vectors
      /// </summary>
      /// <param name="vect1">The first vector</param>
      /// <param name="vect2">The second vector</param>
      /// <returns>A vector containing the the values of the two vectors added</returns>
      public static ColumnVector operator +(ColumnVector vect1, ColumnVector vect2)
      {
        // if the vectors aren't the same size
        if (vect1.Count != vect1.Count)
        {
          // throw an exception
          throw new ArgumentException("Can't perform addition; The vectors are not the same size");
        }

        // use the base CombineVectors method to subtract the vectors and return a new row vector
        return new ColumnVector(CombineVectors(vect1, vect2, false));
      }

      /// <summary>
      /// Scale a column vector by a factor
      /// </summary>
      /// <param name="factor">The factor to scale by</param>
      /// <param name="vector">The vector to scale</param>
      /// <returns>A scaled column vector</returns>
      public static ColumnVector operator *(decimal factor, ColumnVector vector)
      {
        // call the base scale method on the passed in vector
        vector.Scale(factor);

        // return the scaled vector
        return vector;
      }
    }

    private class DistinctInt3Iter
    {
      private Dictionary<int, Dictionary<int, Dictionary<int, bool>>> table =
        new Dictionary<int, Dictionary<int, Dictionary<int, bool>>>();

      public bool Iter(int a, int b, int c)
      {
        int[] keys = new int[] { a, b, c };
        Array.Sort(keys);

        int k1 = keys[0];
        int k2 = keys[1];
        int k3 = keys[2];

        Dictionary<int, Dictionary<int, bool>> tier2;
        if (!table.TryGetValue(k1, out tier2))
        {
          tier2 = new Dictionary<int, Dictionary<int, bool>>();
          table.Add(k1, tier2);
        }

        Dictionary<int, bool> tier3;
        if (!tier2.TryGetValue(k2, out tier3))
        {
          tier3 = new Dictionary<int, bool>();
          tier2.Add(k2, tier3);
        }

        // if tier3 contains the key, then return false. Meaning, don't iter
        if (tier3.ContainsKey(k3)) return false;

        // otherwise, add the value to the table
        tier3.Add(k3, true);

        // return true. Meaning, do iter
        return true;
      }
    }

    private class CubeFormula
    {
      public int Length;
      public int Width;
      public int Height;

      public int B;
      public int C;
    }

    private class Point3
    {
      public static readonly Point3 Up = new Point3(0, 1, 0);
      public static readonly Point3 Down = new Point3(0, -1, 0);
      public static readonly Point3 Right = new Point3(1, 0, 0);
      public static readonly Point3 Left = new Point3(-1, 0, 0);
      public static readonly Point3 Forward = new Point3(0, 0, -1);
      public static readonly Point3 Backward = new Point3(0, 0, 1);
      public static readonly Point3 Zero = new Point3(0, 0, 0);
      static Point3() { }

      private int mX;
      private int mY;
      private int mZ;
      private long key;

      public int X { get { return mX; } }
      public int Y { get { return mY; } }
      public int Z { get { return mZ; } }
      public long Key { get { return key; } }

      public Point3(int x, int y, int z)
      {
        mX = x;
        mY = y;
        mZ = z;

        key = FastBitConverter.ToInt64((short)x, (short)y, (short)z, 0);
      }

      public Point3(int x, int y, int z, Point3 offset)
        : this(x + offset.mX, y + offset.mY, z + offset.mZ)
      {

      }

      public Point3(Point3 other, Point3 offset)
        : this(other.mX + offset.mX, other.mY + offset.mY, other.mZ + offset.mZ)
      {

      }

      public Point3(int x, int y, int z, int scale)
        : this(x * scale, y * scale, z * scale)
      {

      }

      public Point3(Point3 other)
        : this(other.mX, other.mY, other.mZ)
      {

      }

      public Point3(Point3 other, int scale)
        : this(other.mX * scale, other.mY * scale, other.mZ * scale)
      {

      }

      public override int GetHashCode()
      {
        return key.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        Point3 other = obj as Point3;
        if (other == null) return false;
        return other.key.Equals(key);
      }
    }

    private class Face
    {
      public Point3 Center;
      public bool Taken;

      public override int GetHashCode()
      {
        return Center.GetHashCode();
      }

      public override bool Equals(object obj)
      {
        Face other = obj as Face;
        if (other == null) return false;
        return other.Center.Equals(Center);
      }
    }

    private class Cube
    {
      public Point3 Center;

      public Face Left;
      public Face Right;
      public Face Top;
      public Face Bottom;
      public Face Front;
      public Face Back;

      public bool IsAtLeastOneFaceFree
      {
        get
        {
          if (!Back.Taken) return true;
          if (!Bottom.Taken) return true;
          if (!Front.Taken) return true;
          if (!Left.Taken) return true;
          if (!Right.Taken) return true;
          if (!Top.Taken) return true;
          return false;
        }
      }

      public static List<Cube> CreateLayer(List<Cube> solid)
      {
        Dictionary64<Cube> layer = new Dictionary64<Cube>();
        for (int i = 0; i < solid.Count; ++i)
        {
          Cube cube1 = solid[i];
          for (int j = 0; j < solid.Count; ++j)
          {
            if (i == j) continue;
            Cube cube2 = solid[j];

            // the front face is taken if the back face is the same as the front

            cube1.Back.Taken |= cube2.Front.Equals(cube1.Back);
            cube1.Bottom.Taken |= cube2.Top.Equals(cube1.Bottom);
            cube1.Front.Taken |= cube2.Back.Equals(cube1.Front);
            cube1.Left.Taken |= cube2.Right.Equals(cube1.Left);
            cube1.Right.Taken |= cube2.Left.Equals(cube1.Right);
            cube1.Top.Taken |= cube2.Bottom.Equals(cube1.Top);
          }

          var values = new Tuple<Face, Point3, Func<Cube, Face>>[]
          {
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Back, Point3.Backward, c => c.Front),
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Bottom, Point3.Down, c => c.Top), 
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Front, Point3.Forward, c => c.Back),
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Left, Point3.Left, c => c.Right),
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Right, Point3.Right, c => c.Left),
            new Tuple<Face, Point3, Func<Cube, Face>>(cube1.Top, Point3.Up, c => c.Bottom),
          };

          foreach (var value in values)
          {
            var face = value.Item1;
            var offset = value.Item2;
            var opposite = value.Item3;

            if (!face.Taken)
            {
              Cube cube = CreateCuboid(1, 1, 1, new Point3(face.Center, offset))[0];
              long key = cube.Center.Key;

              Cube existing;
              if (!layer.TryGetValue(key, out existing))
              {
                existing = cube;
                layer[key] = existing;
              }

              // the opposite face of the existing cube is now taken
              opposite(existing).Taken = true;
            }
          }
        }

        // retrieve the values
        return layer.Values.ToList();
      }

      public static List<Cube> CreateCuboid(int length, int width, int height)
      {
        return CreateCuboid(length, width, height, Point3.Zero);
      }

      public static List<Cube> CreateCuboid(int length, int width, int height, Point3 offset)
      {
        const int Size = 2;
        List<Cube> cubes = new List<Cube>();
        for (int l = 0; l < length; ++l)
        {
          int x = l * Size;
          for (int w = 0; w < width; ++w)
          {
            int z = w * Size;
            for (int h = 0; h < height; ++h)
            {
              int y = h * Size;

              // the x,y,z is the center of the cube
              Point3 center = new Point3(x, y, z, offset);

              // each face is just moving away from the center
              cubes.Add(new Cube
              {
                Center = center,
                Back = new Face { Center = new Point3(center, Point3.Backward) },
                Bottom = new Face { Center = new Point3(center, Point3.Down) },
                Front = new Face { Center = new Point3(center, Point3.Forward) },
                Left = new Face { Center = new Point3(center, Point3.Left) },
                Right = new Face { Center = new Point3(center, Point3.Right) },
                Top = new Face { Center = new Point3(center, Point3.Up) },
              });
            }
          }
        }
        return cubes;
      }
    }

    private static class Gauss
    {
      /// <summary>
      /// Changes a matrix to row echelon form
      /// </summary>
      /// <param name="matrix">The matrix to operate on</param>
      /// <returns>The matrix in row echelon form</returns>
      public static Matrix RowEchelonMatrix(Matrix matrix)
      {
        // clone it
        Matrix temp = matrix.Clone();

        // the pivot column
        int pC = 0;

        // the pivot row
        int pR = 0;

        // while the pivot row and pivot column are valid
        while (pR < matrix.RowCount && pC < matrix.ColumnCount)
        {
          #region Check Pivot Point
          // if the pivot point is zero
          if (temp[pR, pC] == 0)
          {
            // cycle through the pivot point column looking for a value that isn't zero
            int row1 = pR;
            int row2 = pR;

            for (int r = pR; r < matrix.RowCount; ++r)
            {
              if (temp[r, pC] != 0)
              {
                // setup a row swap
                row2 = r;
                break;
              }
            }

            if (row1 == row2)
            {
              // this entire column is zero, skip it
              ++pC;
              continue;
            }
            else
            {
              // pivot/swap the rows
              temp.Pivot(row1, row2);
            }
          }
          #endregion

          #region Make Pivot Point 1
          // get the value at the pivot point
          decimal number = temp[pR, pC];

          // if the pivot point value isn't equal to 1
          if (number != 1)
          {
            // cycle through the pivot row
            for (int c = pC; c < temp.ColumnCount; ++c)
            {
              // divide the entire row by the pivot value
              temp[pR, c] = temp[pR, c] / number;
            }
          }
          #endregion

          #region Make values Zero
          // cycle through the values under the pivot point
          for (int r = pR + 1; r < temp.RowCount; ++r)
          {
            // get the number below the pivot point
            decimal below = temp[r, pC];

            // get the row
            RowVector row = temp[r];

            // adjust the row
            temp[r] = row - (below * temp[pR]);
          }
          #endregion

          // increment the pivot
          ++pR;
          ++pC;
        }

        // return the row echelon matrix
        return temp;
      }

      /// <summary>
      /// Changes a matrix to reduced row echelon form
      /// </summary>
      /// <param name="matrix">The matrix to operate on</param>
      /// <returns>The matrix in reduced row echelon form</returns>
      public static Matrix ReducedRowEchelonMatrix(Matrix matrix)
      {
        // row echelon the matrix
        Matrix temp = RowEchelonMatrix(matrix);

        // create the pivot column
        int pC = matrix.ColumnCount - 2;

        // create the pivot row
        int pR = matrix.RowCount - 1;

        // while the pivot row and column are valid
        while (pR > -1 && pC > -1)
        {
          #region Make values Zero
          // cycle through the values above the pivot point
          for (int r = pR - 1; r > -1; --r)
          {
            // get the number above the pivot point
            decimal above = temp[r, pC];

            // get the row
            RowVector row = temp[r];

            // adjust the row
            temp[r] = row - (above * temp[pR]);
          }
          #endregion

          // decrement the pivot
          --pR;
          --pC;
        }

        // return the reduced row echelon matrix
        return temp;
      }

      /// <summary>
      /// Use guassian elimination on a matrix representing a system of linear equations. This
      /// assumes that the matrix isn't in row echelon or reduced row echelon form.
      /// </summary>
      /// <param name="matrix">The matrix representing the equations</param>
      /// <returns>A column vector containing the solution</returns>
      public static ColumnVector GaussianEliminate(Matrix matrix)
      {
        // check the matrix
        if ((matrix.RowCount + 1) != matrix.ColumnCount)
        {
          throw new ArgumentException("The passed in matrix must be n by (n+1) where the 1 is a column vector");
        }

        // row reduce the matrix
        Matrix echelon = RowEchelonMatrix(matrix);

        // create a solution array
        decimal[] solution = new decimal[echelon.RowCount];

        // set the known value
        solution[solution.Length - 1] = echelon[echelon.RowCount - 1, echelon.ColumnCount - 1];

        // create a column indexer
        int c = 2;//echelon.ColumnCount - 3;

        // cycle through the rest of the echelon matrix and back substitue
        for (int r = echelon.RowCount - 2; r > -1; --r)
        {
          // get the part its equaled to
          decimal equaled = echelon[r, echelon.ColumnCount - 1];

          // create a variable to hold the left hand side of the "equation"
          decimal lhs = 0;

          // create a variable for cycling through the solution array
          int s = r + 1;

          // get the entire left hand side of the equation (without the variable)
          for (int i = c + 1; i < echelon.ColumnCount - 1; ++i)
          {
            // update the left hand side part
            lhs += (echelon[r, i] * solution[s]);

            // update the solution indexer
            ++s;
          }

          // get the variable value
          decimal value = echelon[r, c];

          // set the solution
          solution[r] = (equaled - lhs) / value;

          // update the column position
          --c;
        }

        // return the solution vector
        return new ColumnVector(solution);
      }

      /// <summary>
      /// Uses gauss-jordan on a matrix representing a system of linear equations. This
      /// assumes that the matrix isn't in row echelon or reduced row echelon form.
      /// </summary>
      /// <param name="matrix">The matrix representing the equations</param>
      /// <returns>A column vector containing the solution</returns>
      public static ColumnVector GaussianJordan(Matrix matrix)
      {
        // check the matrix
        if ((matrix.RowCount + 1) != matrix.ColumnCount)
        {
          throw new ArgumentException("The passed in matrix must be able to become upper triangular when reduced");
        }

        // get the last column of the reduced row echelon matrix
        ColumnVector columnVector = new ColumnVector(ReducedRowEchelonMatrix(matrix).GetColumn(matrix.ColumnCount - 1));

        // return that column as the solution
        return columnVector;
      }
    }

    private static class Formula
    {
      public static decimal[] FindFormula(List<int> sequence)
      {
        if (sequence.Count < 3) return null;

        List<int> current = new List<int>(sequence);
        for (int level = 1; current.Count > 1; ++level)
        {
          // calculate the differences
          List<int> differences = new List<int>();
          for (int i = 1; i < current.Count; ++i)
          {
            differences.Add(current[i] - current[i - 1]);
          }

          // the current is now the differences
          current = differences;

          // are all the differences the same
          if (new HashSet<int>(differences).Count == 1)
          {
            int numTerms = level + 1;
            Matrix matrix = new Matrix(new MatrixSize(numTerms, numTerms + 1));
            for (int i = 0; i < numTerms; ++i)
            {
              var row = new List<decimal>();
              row.AddRange(Terms(numTerms, (i + 1)));
              row.Add(sequence[i]);
              matrix.SetRow(i, row.ToArray());
            }

            ColumnVector solution = Gauss.GaussianJordan(matrix);
            solution.Round();
            return solution.ToArray();
          }
        }

        return null;
      }

      private static IEnumerable<decimal> Terms(int numTerms, int n)
      {
        for (int pow = numTerms - 1; pow > 0; --pow)
        {
          yield return (decimal)BigInteger.Pow(n, pow);
        }
        yield return 1m;
      }
    }

    #endregion
  }
}
