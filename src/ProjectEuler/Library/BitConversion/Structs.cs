using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ProjectEuler
{
  public static partial class FastBitConverter
  {
    [StructLayout(LayoutKind.Explicit)]
    private struct Byte2Int16
    {
      [FieldOffset(0)]
      public byte b1;
      [FieldOffset(1)]
      public byte b2;
      [FieldOffset(0)]
      public short value;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct Byte4Int32
    {
      [FieldOffset(0)]
      public byte b1;
      [FieldOffset(1)]
      public byte b2;
      [FieldOffset(2)]
      public byte b3;
      [FieldOffset(3)]
      public byte b4;
      [FieldOffset(0)]
      public int value;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct Short2Int32
    {
      [FieldOffset(0)]
      public short s1;
      [FieldOffset(2)]
      public short s2;
      [FieldOffset(0)]
      public int value;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct Short4Int64
    {
      [FieldOffset(0)]
      public short s1;
      [FieldOffset(2)]
      public short s2;
      [FieldOffset(4)]
      public short s3;
      [FieldOffset(6)]
      public short s4;
      [FieldOffset(0)]
      public long value;
    }
  }
}
