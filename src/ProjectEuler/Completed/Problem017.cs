using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem017 : EulerProblem
  {
    static string[] Text1 = new string[]
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

    static string[] TextTeens = new string[]
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
        "Nineteen",
      };

    static string[] Text10 = new string[]
      {
        "Twenty",
        "Thirty",
        "Forty",
        "Fifty",
        "Sixty",
        "Seventy",
        "Eighty",
        "Ninety",
      };

    static Problem017() { }

    public override int Number { get { return 17; } }

    public override object Solve()
    {
      // we include the number of letters for "one thousand" without spaces
      int letterCount = 11;
      for (int n = 1; n < 1000; ++n)
      {
        string text = Print(n);
        letterCount += text.Count(c => char.IsLetter(c));
      }
      return letterCount;
    }

    private string Print(int n)
    {
      int nMod100 = n % 100;

      int hundreds = n / 100;
      int tens = (nMod100 / 10);
      int ones = n % 10;

      StringBuilder sb = new StringBuilder();
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

        if(tens < 2)
        {
          sb.AppendFormat("{0}", TextTeens[nMod100 - 10]);
        }
        else
        {
          sb.AppendFormat("{0}", Text10[(nMod100 / 10) - 2]);
          if(ones > 0)
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
