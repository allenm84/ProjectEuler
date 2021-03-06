﻿using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0017 : euler
  {
    private static string[] Text1 =
    {
      "Zero",
      "One",
      "Two",
      "Three",
      "Four",
      "Five",
      "Six",
      "Seven",
      "Eight",
      "Nine"
    };

    private static string[] TextTeens =
    {
      "Ten",
      "Eleven",
      "Twelve",
      "Thirteen",
      "Fourteen",
      "Fifteen",
      "Sixteen",
      "Seventeen",
      "Eighteen",
      "Nineteen"
    };

    private static string[] Text10 =
    {
      "Twenty",
      "Thirty",
      "Forty",
      "Fifty",
      "Sixty",
      "Seventy",
      "Eighty",
      "Ninety"
    };

    public override void Run()
    {
      // we include the number of letters for "one thousand" without spaces
      var letterCount = 11;
      for (var n = 1; n < 1000; ++n)
      {
        var text = Print(n);
        letterCount += text.Count(char.IsLetter);
      }

      Console.WriteLine(letterCount);
    }

    private string Print(int n)
    {
      var nMod100 = n % 100;

      var hundreds = n / 100;
      var tens = (nMod100 / 10);
      var ones = n % 10;

      var sb = new StringBuilder();
      if (hundreds > 0)
      {
        sb.AppendFormat("{0} Hundred", Text1[hundreds]);
      }

      if (tens > 0)
      {
        if (hundreds > 0)
        {
          sb.Append(" and ");
        }

        if (tens < 2)
        {
          sb.AppendFormat("{0}", TextTeens[nMod100 - 10]);
        }
        else
        {
          sb.AppendFormat("{0}", Text10[(nMod100 / 10) - 2]);
          if (ones > 0)
          {
            sb.AppendFormat(" {0}", Text1[ones]);
          }
        }
      }

      if (ones > 0)
      {
        if (hundreds > 0 && tens == 0)
        {
          sb.AppendFormat(" and {0}", Text1[ones]);
        }
      }

      if (tens == 0 && hundreds == 0)
      {
        sb.AppendFormat("{0}", Text1[ones]);
      }
      return sb.ToString();
    }
  }
}
