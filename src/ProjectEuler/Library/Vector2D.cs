using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ProjectEuler
{
  /// <summary>Defines a vector with two components.</summary>
  public struct Vector2D : IEquatable<Vector2D>
  {
    /// <summary>Gets or sets the x-component of the vector.</summary>
    public double X;

    /// <summary>Gets or sets the y-component of the vector.</summary>
    public double Y;

    private static Vector2D _zero = default(Vector2D);
    private static Vector2D _one = new Vector2D(1d, 1d);
    private static Vector2D _unitX = new Vector2D(1d, 0d);
    private static Vector2D _unitY = new Vector2D(0d, 1d);

    /// <summary>Returns a Vector2 with all of its components set to zero.</summary>
    public static Vector2D Zero
    {
      get
      {
        return Vector2D._zero;
      }
    }

    /// <summary>Returns a Vector2 with both of its components set to one.</summary>
    public static Vector2D One
    {
      get
      {
        return Vector2D._one;
      }
    }

    /// <summary>Returns the unit vector for the x-axis.</summary>
    public static Vector2D UnitX
    {
      get
      {
        return Vector2D._unitX;
      }
    }

    /// <summary>Returns the unit vector for the y-axis.</summary>
    public static Vector2D UnitY
    {
      get
      {
        return Vector2D._unitY;
      }
    }

    /// <summary>Initializes a new instance of Vector2.</summary>
    /// <param name="x">Initial value for the x-component of the vector.</param>
    /// <param name="y">Initial value for the y-component of the vector.</param>
    public Vector2D(double x, double y)
    {
      this.X = x;
      this.Y = y;
    }

    /// <summary>Creates a new instance of Vector2.</summary>
    /// <param name="value">Value to initialize both components to.</param>
    public Vector2D(double value)
    {
      this.Y = value;
      this.X = value;
    }

    /// <summary>Retrieves a string representation of the current object.</summary>
    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[]
			{
				this.X.ToString(currentCulture), 
				this.Y.ToString(currentCulture)
			});
    }

    /// <summary>Determines whether the specified Object is equal to the Vector2.</summary>
    /// <param name="other">The Object to compare with the current Vector2.</param>
    public bool Equals(Vector2D other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    /// <summary>Returns a value that indicates whether the current instance is equal to a specified object.</summary>
    /// <param name="obj">Object to make the comparison with.</param>
    public override bool Equals(object obj)
    {
      bool result = false;
      if (obj is Vector2D)
      {
        result = this.Equals((Vector2D)obj);
      }
      return result;
    }

    /// <summary>Gets the hash code of the vector object.</summary>
    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode();
    }

    /// <summary>Calculates the length of the vector.</summary>
    public double Length()
    {
      return Math.Sqrt(this.X * this.X + this.Y * this.Y);
    }

    /// <summary>Calculates the length of the vector squared.</summary>
    public double LengthSquared()
    {
      return this.X * this.X + this.Y * this.Y;
    }

    /// <summary>Calculates the distance between two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static double Distance(Vector2D value1, Vector2D value2)
    {
      double num = value1.X - value2.X;
      double num2 = value1.Y - value2.Y;
      return Math.Sqrt(num * num + num2 * num2);
    }

    /// <summary>Calculates the distance between two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The distance between the vectors.</param>
    public static void Distance(ref Vector2D value1, ref Vector2D value2, out double result)
    {
      double num = value1.X - value2.X;
      double num2 = value1.Y - value2.Y;
      result = Math.Sqrt(num * num + num2 * num2);
    }

    /// <summary>Calculates the distance between two vectors squared.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static double DistanceSquared(Vector2D value1, Vector2D value2)
    {
      double num = value1.X - value2.X;
      double num2 = value1.Y - value2.Y;
      return num * num + num2 * num2;
    }

    /// <summary>Calculates the distance between two vectors squared.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The distance between the vectors squared.</param>
    public static void DistanceSquared(ref Vector2D value1, ref Vector2D value2, out double result)
    {
      double num = value1.X - value2.X;
      double num2 = value1.Y - value2.Y;
      result = num * num + num2 * num2;
    }

    /// <summary>Calculates the dot product of two vectors. If the two vectors are unit vectors, the dot product returns a floating point value between -1 and 1 that can be used to determine some properties of the angle between two vectors. For example, it can show whether the vectors are orthogonal, parallel, or have an acute or obtuse angle between them.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static double Dot(Vector2D value1, Vector2D value2)
    {
      return value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>Calculates the dot product of two vectors and writes the result to a user-specified variable. If the two vectors are unit vectors, the dot product returns a floating point value between -1 and 1 that can be used to determine some properties of the angle between two vectors. For example, it can show whether the vectors are orthogonal, parallel, or have an acute or obtuse angle between them.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The dot product of the two vectors.</param>
    public static void Dot(ref Vector2D value1, ref Vector2D value2, out double result)
    {
      result = value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>Turns the current vector into a unit vector. The result is a vector one unit in length pointing in the same direction as the original vector.</summary>
    public void Normalize()
    {
      double num = 1d / Math.Sqrt(this.X * this.X + this.Y * this.Y);
      this.X *= num;
      this.Y *= num;
    }

    /// <summary>Creates a unit vector from the specified vector. The result is a vector one unit in length pointing in the same direction as the original vector.</summary>
    /// <param name="value">Source Vector2.</param>
    public static Vector2D Normalize(Vector2D value)
    {
      double num = 1d / Math.Sqrt(value.X * value.X + value.Y * value.Y);
      Vector2D result;
      result.X = value.X * num;
      result.Y = value.Y * num;
      return result;
    }

    /// <summary>Creates a unit vector from the specified vector, writing the result to a user-specified variable. The result is a vector one unit in length pointing in the same direction as the original vector.</summary>
    /// <param name="value">Source vector.</param>
    /// <param name="result">[OutAttribute] Normalized vector.</param>
    public static void Normalize(ref Vector2D value, out Vector2D result)
    {
      double num = 1d / Math.Sqrt(value.X * value.X + value.Y * value.Y);
      result.X = value.X * num;
      result.Y = value.Y * num;
    }

    /// <summary>Determines the reflect vector of the given vector and normal.</summary>
    /// <param name="vector">Source vector.</param>
    /// <param name="normal">Normal of vector.</param>
    public static Vector2D Reflect(Vector2D vector, Vector2D normal)
    {
      double num = vector.X * normal.X + vector.Y * normal.Y;
      Vector2D result;
      result.X = vector.X - 2d * num * normal.X;
      result.Y = vector.Y - 2d * num * normal.Y;
      return result;
    }

    /// <summary>Determines the reflect vector of the given vector and normal.</summary>
    /// <param name="vector">Source vector.</param>
    /// <param name="normal">Normal of vector.</param>
    /// <param name="result">[OutAttribute] The created reflect vector.</param>
    public static void Reflect(ref Vector2D vector, ref Vector2D normal, out Vector2D result)
    {
      double num = vector.X * normal.X + vector.Y * normal.Y;
      result.X = vector.X - 2d * num * normal.X;
      result.Y = vector.Y - 2d * num * normal.Y;
    }

    /// <summary>Returns a vector that contains the lowest value from each matching pair of components.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D Min(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
      return result;
    }

    /// <summary>Returns a vector that contains the lowest value from each matching pair of components.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The minimized vector.</param>
    public static void Min(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = ((value1.X < value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
    }

    /// <summary>Returns a vector that contains the highest value from each matching pair of components.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D Max(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
      return result;
    }

    /// <summary>Returns a vector that contains the highest value from each matching pair of components.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The maximized vector.</param>
    public static void Max(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = ((value1.X > value2.X) ? value1.X : value2.X);
      result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
    }

    /// <summary>Restricts a value to be within a specified range.</summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static Vector2D Clamp(Vector2D value1, Vector2D min, Vector2D max)
    {
      double numX = value1.X;
      numX = ((numX > max.X) ? max.X : numX);
      numX = ((numX < min.X) ? min.X : numX);
      double numY = value1.Y;
      numY = ((numY > max.Y) ? max.Y : numY);
      numY = ((numY < min.Y) ? min.Y : numY);
      Vector2D result;
      result.X = numX;
      result.Y = numY;
      return result;
    }

    /// <summary>Restricts a value to be within a specified range.</summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    /// <param name="result">[OutAttribute] The clamped value.</param>
    public static void Clamp(ref Vector2D value1, ref Vector2D min, ref Vector2D max, out Vector2D result)
    {
      double numX = value1.X;
      numX = ((numX > max.X) ? max.X : numX);
      numX = ((numX < min.X) ? min.X : numX);
      double numY = value1.Y;
      numY = ((numY > max.Y) ? max.Y : numY);
      numY = ((numY < min.Y) ? min.Y : numY);
      result.X = numX;
      result.Y = numY;
    }

    /// <summary>Performs a linear interpolation between two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
    public static Vector2D Lerp(Vector2D value1, Vector2D value2, double amount)
    {
      Vector2D result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      return result;
    }

    /// <summary>Performs a linear interpolation between two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
    /// <param name="result">[OutAttribute] The result of the interpolation.</param>
    public static void Lerp(ref Vector2D value1, ref Vector2D value2, double amount, out Vector2D result)
    {
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
    }

    /// <summary>Returns a Vector2 containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</summary>
    /// <param name="value1">A Vector2 containing the 2D Cartesian coordinates of vertex 1 of the triangle.</param>
    /// <param name="value2">A Vector2 containing the 2D Cartesian coordinates of vertex 2 of the triangle.</param>
    /// <param name="value3">A Vector2 containing the 2D Cartesian coordinates of vertex 3 of the triangle.</param>
    /// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
    /// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
    public static Vector2D Barycentric(Vector2D value1, Vector2D value2, Vector2D value3, double amount1, double amount2)
    {
      Vector2D result;
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
      return result;
    }

    /// <summary>Returns a Vector2 containing the 2D Cartesian coordinates of a point specified in barycentric (areal) coordinates relative to a 2D triangle.</summary>
    /// <param name="value1">A Vector2 containing the 2D Cartesian coordinates of vertex 1 of the triangle.</param>
    /// <param name="value2">A Vector2 containing the 2D Cartesian coordinates of vertex 2 of the triangle.</param>
    /// <param name="value3">A Vector2 containing the 2D Cartesian coordinates of vertex 3 of the triangle.</param>
    /// <param name="amount1">Barycentric coordinate b2, which expresses the weighting factor toward vertex 2 (specified in value2).</param>
    /// <param name="amount2">Barycentric coordinate b3, which expresses the weighting factor toward vertex 3 (specified in value3).</param>
    /// <param name="result">[OutAttribute] The 2D Cartesian coordinates of the specified point are placed in this Vector2 on exit.</param>
    public static void Barycentric(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, double amount1, double amount2, out Vector2D result)
    {
      result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
      result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
    }

    /// <summary>Interpolates between two values using a cubic equation.</summary>
    /// <param name="value1">Source value.</param>
    /// <param name="value2">Source value.</param>
    /// <param name="amount">Weighting value.</param>
    public static Vector2D SmoothStep(Vector2D value1, Vector2D value2, double amount)
    {
      amount = ((amount > 1d) ? 1d : ((amount < 0d) ? 0d : amount));
      amount = amount * amount * (3d - 2d * amount);
      Vector2D result;
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
      return result;
    }

    /// <summary>Interpolates between two values using a cubic equation.</summary>
    /// <param name="value1">Source value.</param>
    /// <param name="value2">Source value.</param>
    /// <param name="amount">Weighting value.</param>
    /// <param name="result">[OutAttribute] The interpolated value.</param>
    public static void SmoothStep(ref Vector2D value1, ref Vector2D value2, double amount, out Vector2D result)
    {
      amount = ((amount > 1d) ? 1d : ((amount < 0d) ? 0d : amount));
      amount = amount * amount * (3d - 2d * amount);
      result.X = value1.X + (value2.X - value1.X) * amount;
      result.Y = value1.Y + (value2.Y - value1.Y) * amount;
    }

    /// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
    /// <param name="value1">The first position in the interpolation.</param>
    /// <param name="value2">The second position in the interpolation.</param>
    /// <param name="value3">The third position in the interpolation.</param>
    /// <param name="value4">The fourth position in the interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    public static Vector2D CatmullRom(Vector2D value1, Vector2D value2, Vector2D value3, Vector2D value4, double amount)
    {
      double num = amount * amount;
      double num2 = amount * num;
      Vector2D result;
      result.X = 0.5d * (2d * value2.X + (-value1.X + value3.X) * amount + (2d * value1.X - 5d * value2.X + 4d * value3.X - value4.X) * num + (-value1.X + 3d * value2.X - 3d * value3.X + value4.X) * num2);
      result.Y = 0.5d * (2d * value2.Y + (-value1.Y + value3.Y) * amount + (2d * value1.Y - 5d * value2.Y + 4d * value3.Y - value4.Y) * num + (-value1.Y + 3d * value2.Y - 3d * value3.Y + value4.Y) * num2);
      return result;
    }

    /// <summary>Performs a Catmull-Rom interpolation using the specified positions.</summary>
    /// <param name="value1">The first position in the interpolation.</param>
    /// <param name="value2">The second position in the interpolation.</param>
    /// <param name="value3">The third position in the interpolation.</param>
    /// <param name="value4">The fourth position in the interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <param name="result">[OutAttribute] A vector that is the result of the Catmull-Rom interpolation.</param>
    public static void CatmullRom(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, ref Vector2D value4, double amount, out Vector2D result)
    {
      double num = amount * amount;
      double num2 = amount * num;
      result.X = 0.5d * (2d * value2.X + (-value1.X + value3.X) * amount + (2d * value1.X - 5d * value2.X + 4d * value3.X - value4.X) * num + (-value1.X + 3d * value2.X - 3d * value3.X + value4.X) * num2);
      result.Y = 0.5d * (2d * value2.Y + (-value1.Y + value3.Y) * amount + (2d * value1.Y - 5d * value2.Y + 4d * value3.Y - value4.Y) * num + (-value1.Y + 3d * value2.Y - 3d * value3.Y + value4.Y) * num2);
    }

    /// <summary>Performs a Hermite spline interpolation.</summary>
    /// <param name="value1">Source position vector.</param>
    /// <param name="tangent1">Source tangent vector.</param>
    /// <param name="value2">Source position vector.</param>
    /// <param name="tangent2">Source tangent vector.</param>
    /// <param name="amount">Weighting factor.</param>
    public static Vector2D Hermite(Vector2D value1, Vector2D tangent1, Vector2D value2, Vector2D tangent2, double amount)
    {
      double num = amount * amount;
      double num2 = amount * num;
      double num3 = 2d * num2 - 3d * num + 1d;
      double num4 = -2d * num2 + 3d * num;
      double num5 = num2 - 2d * num + amount;
      double num6 = num2 - num;
      Vector2D result;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
      return result;
    }

    /// <summary>Performs a Hermite spline interpolation.</summary>
    /// <param name="value1">Source position vector.</param>
    /// <param name="tangent1">Source tangent vector.</param>
    /// <param name="value2">Source position vector.</param>
    /// <param name="tangent2">Source tangent vector.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <param name="result">[OutAttribute] The result of the Hermite spline interpolation.</param>
    public static void Hermite(ref Vector2D value1, ref Vector2D tangent1, ref Vector2D value2, ref Vector2D tangent2, double amount, out Vector2D result)
    {
      double num = amount * amount;
      double num2 = amount * num;
      double num3 = 2f * num2 - 3f * num + 1f;
      double num4 = -2f * num2 + 3f * num;
      double num5 = num2 - 2f * num + amount;
      double num6 = num2 - num;
      result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
      result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
    }

    /// <summary>Returns a vector pointing in the opposite direction.</summary>
    /// <param name="value">Source vector.</param>
    public static Vector2D Negate(Vector2D value)
    {
      Vector2D result;
      result.X = -value.X;
      result.Y = -value.Y;
      return result;
    }

    /// <summary>Returns a vector pointing in the opposite direction.</summary>
    /// <param name="value">Source vector.</param>
    /// <param name="result">[OutAttribute] Vector pointing in the opposite direction.</param>
    public static void Negate(ref Vector2D value, out Vector2D result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
    }

    /// <summary>Adds two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D Add(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      return result;
    }

    /// <summary>Adds two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] Sum of the source vectors.</param>
    public static void Add(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
    }

    /// <summary>Subtracts a vector from a vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D Subtract(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      return result;
    }

    /// <summary>Subtracts a vector from a vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The result of the subtraction.</param>
    public static void Subtract(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
    }

    /// <summary>Multiplies the components of two vectors by each other.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D Multiply(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      return result;
    }

    /// <summary>Multiplies the components of two vectors by each other.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    /// <param name="result">[OutAttribute] The result of the multiplication.</param>
    public static void Multiply(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
    }

    /// <summary>Multiplies a vector by a scalar value.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    public static Vector2D Multiply(Vector2D value1, double scaleFactor)
    {
      Vector2D result;
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
      return result;
    }

    /// <summary>Multiplies a vector by a scalar value.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <param name="result">[OutAttribute] The result of the multiplication.</param>
    public static void Multiply(ref Vector2D value1, double scaleFactor, out Vector2D result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
    }

    /// <summary>Divides the components of a vector by the components of another vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Divisor vector.</param>
    public static Vector2D Divide(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      return result;
    }

    /// <summary>Divides the components of a vector by the components of another vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">The divisor.</param>
    /// <param name="result">[OutAttribute] The result of the division.</param>
    public static void Divide(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
    }

    /// <summary>Divides a vector by a scalar value.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="divider">The divisor.</param>
    public static Vector2D Divide(Vector2D value1, double divider)
    {
      double num = 1d / divider;
      Vector2D result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      return result;
    }

    /// <summary>Divides a vector by a scalar value.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="divider">The divisor.</param>
    /// <param name="result">[OutAttribute] The result of the division.</param>
    public static void Divide(ref Vector2D value1, double divider, out Vector2D result)
    {
      double num = 1d / divider;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
    }

    /// <summary>Returns a vector pointing in the opposite direction.</summary>
    /// <param name="value">Source vector.</param>
    public static Vector2D operator -(Vector2D value)
    {
      Vector2D result;
      result.X = -value.X;
      result.Y = -value.Y;
      return result;
    }

    /// <summary>Tests vectors for equality.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static bool operator ==(Vector2D value1, Vector2D value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y;
    }

    /// <summary>Tests vectors for inequality.</summary>
    /// <param name="value1">Vector to compare.</param>
    /// <param name="value2">Vector to compare.</param>
    public static bool operator !=(Vector2D value1, Vector2D value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y;
    }

    /// <summary>Adds two vectors.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D operator +(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
      return result;
    }

    /// <summary>Subtracts a vector from a vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">source vector.</param>
    public static Vector2D operator -(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
      return result;
    }

    /// <summary>Multiplies the components of two vectors by each other.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Source vector.</param>
    public static Vector2D operator *(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
      return result;
    }

    /// <summary>Multiplies a vector by a scalar value.</summary>
    /// <param name="value">Source vector.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    public static Vector2D operator *(Vector2D value, double scaleFactor)
    {
      Vector2D result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      return result;
    }

    /// <summary>Multiplies a vector by a scalar value.</summary>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <param name="value">Source vector.</param>
    public static Vector2D operator *(double scaleFactor, Vector2D value)
    {
      Vector2D result;
      result.X = value.X * scaleFactor;
      result.Y = value.Y * scaleFactor;
      return result;
    }

    /// <summary>Divides the components of a vector by the components of another vector.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="value2">Divisor vector.</param>
    public static Vector2D operator /(Vector2D value1, Vector2D value2)
    {
      Vector2D result;
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
      return result;
    }

    /// <summary>Divides a vector by a scalar value.</summary>
    /// <param name="value1">Source vector.</param>
    /// <param name="divider">The divisor.</param>
    public static Vector2D operator /(Vector2D value1, double divider)
    {
      double num = 1d / divider;
      Vector2D result;
      result.X = value1.X * num;
      result.Y = value1.Y * num;
      return result;
    }

    /// <summary></summary>
    /// <param name="radians"></param>
    public void Rotate(double radians)
    {
      double num = Math.Cos(radians);
      double num2 = Math.Sin(radians);
      var matrix = new
      {
        M11 = num, M12 = num2, M13 = 0d, M14 = 0d,
        M21 = -num2, M22 = num, M23 = 0d, M24 = 0d,
        M31 = 0d, M32 = 0d, M33 = 1d, M34 = 0d,
        M41 = 0d, M42 = 0d, M43 = 0d, M44 = 1d,
      };

      double x = this.X * matrix.M11 + this.Y * matrix.M21 + matrix.M41;
      double y = this.X * matrix.M12 + this.Y * matrix.M22 + matrix.M42;

      this.X = x;
      this.Y = y;
    }
  }
}
