using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem089 : EulerProblem
  {
    private static readonly Dictionary<char, short> table = new Dictionary<char, short>();

    static Problem089()
    {
      table.Add('I', 1);
      table.Add('V', 5);
      table.Add('X', 10);
      table.Add('L', 50);
      table.Add('C', 100);
      table.Add('D', 500);
      table.Add('M', 1000);
    }

    public override int Number
    {
      get { return 89; }
    }

    public override object Solve()
    {
      return Resources
        .Problem089Data
        .SplitBy('\r', '\n')
        .Sum(l => l.Length - ToRomanNumeral(FromRomanNumeral(l)).Length);
    }

    private string ToRomanNumeral(int n)
    {
      var sb = new StringBuilder();

      // M = 1000. So, subtract until below 1000
      while (n >= 1000)
      {
        sb.Append('M');
        n -= 1000;
      }

      // CM = 900. So, if n is 900-999
      if (900 <= n && n < 1000)
      {
        sb.Append("CM");
        n -= 900;
      }

      // D = 500. So, if n is 500-899
      if (500 <= n && n < 900)
      {
        sb.Append("D");
        n -= 500;
      }

      // CD = 400. So, if n is 400-499
      if (400 <= n && n < 500)
      {
        sb.Append("CD");
        n -= 400;
      }

      // C = 100. So, subtract until below 100
      while (n >= 100)
      {
        sb.Append('C');
        n -= 100;
      }

      // XC = 90. So, if n is 90-99
      if (90 <= n && n < 100)
      {
        sb.Append("XC");
        n -= 90;
      }

      // L = 50. So, if n is 50-89
      if (50 <= n && n < 90)
      {
        sb.Append("L");
        n -= 50;
      }

      // XL = 40. So, if n is 40-49
      if (40 <= n && n < 50)
      {
        sb.Append("XL");
        n -= 40;
      }

      // X = 10. So, subtract until below 10
      while (n >= 10)
      {
        sb.Append('X');
        n -= 10;
      }

      // IX = 9. So, if n is 9
      if (n == 9)
      {
        sb.Append("IX");
        n -= 9;
      }

      // V = 5, So if n is 5-8
      if (5 <= n && n < 9)
      {
        sb.Append("V");
        n -= 5;
      }

      // IV = 4. So, if n is 4
      if (n == 4)
      {
        sb.Append("IV");
        n -= 4;
      }

      // I = 1, So, subtract until below 1
      while (n >= 1)
      {
        sb.Append("I");
        n -= 1;
      }

      // return the text
      return sb.ToString();
    }

    private int FromRomanNumeral(string line)
    {
      var chars = line.ToArray();
      var value = 0;

      for (var i = 0; i < chars.Length; ++i)
      {
        var c = chars[i];
        var next = chars.ElementAtOrDefault(i + 1);

        if (c == 'I' && (next == 'X' || next == 'V'))
        {
          // I can only be placed before V and X.
          value -= 1;
          continue;
        }

        if (c == 'X' && (next == 'L' || next == 'C'))
        {
          // X can only be placed before L and C.
          value -= 10;
          continue;
        }

        if (c == 'C' && (next == 'D' || next == 'M'))
        {
          // C can only be placed before D and M.
          value -= 100;
          continue;
        }

        value += table[c];
      }

      return value;
    }
  }
}