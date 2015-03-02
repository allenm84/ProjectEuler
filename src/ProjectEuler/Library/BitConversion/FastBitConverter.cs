using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public static partial class FastBitConverter
  {
    public static short ToInt16(byte byte1, byte byte2)
    {
      return new Byte2Int16 {b1 = byte1, b2 = byte2}.value;
    }

    public static int ToInt32(byte byte1, byte byte2, byte byte3, byte byte4)
    {
      return new Byte4Int32 {b1 = byte1, b2 = byte2, b3 = byte3, b4 = byte4}.value;
    }

    public static int ToInt32(short short1, short short2)
    {
      return new Short2Int32 {s1 = short1, s2 = short2}.value;
    }

    public static long ToInt64(short short1, short short2, short short3, short short4)
    {
      return new Short4Int64 {s1 = short1, s2 = short2, s3 = short3, s4 = short4}.value;
    }

    public static int ToInt32(byte[] bytes)
    {
      return new Byte4Int32 {b1 = bytes[0], b2 = bytes[1], b3 = bytes[2], b4 = bytes[3]}.value;
    }
  }
}