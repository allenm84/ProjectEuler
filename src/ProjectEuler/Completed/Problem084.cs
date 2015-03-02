using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem084 : EulerProblem
  {
    private static readonly MonopolySquare[] Railways;
    private static readonly MonopolySquare[] Utilities;

    private static readonly Dictionary<MonopolySquare, Func<MonopolySquare, MonopolySquare>> SquareFunctions = new Dictionary<MonopolySquare, Func<MonopolySquare, MonopolySquare>>();
    private static readonly MonopolySquare[] Squares = Enum.GetValues(typeof(MonopolySquare)).Cast<MonopolySquare>().ToArray();
    private static Random random = new Random();

    static Problem084()
    {
      Railways = new MonopolySquare[4];
      Railways[0] = MonopolySquare.R1;
      Railways[1] = MonopolySquare.R2;
      Railways[2] = MonopolySquare.R3;
      Railways[3] = MonopolySquare.R4;

      Utilities = new MonopolySquare[2];
      Utilities[0] = MonopolySquare.U1;
      Utilities[1] = MonopolySquare.U2;

      SquareFunctions[MonopolySquare.G2J] = (s => MonopolySquare.JAIL);
      SquareFunctions[MonopolySquare.CC1] = CommunityChest;
      SquareFunctions[MonopolySquare.CC2] = CommunityChest;
      SquareFunctions[MonopolySquare.CC3] = CommunityChest;
      SquareFunctions[MonopolySquare.CH1] = Chance;
      SquareFunctions[MonopolySquare.CH2] = Chance;
      SquareFunctions[MonopolySquare.CH3] = Chance;

      var remaining = Squares.Except(SquareFunctions.Keys).ToArray();
      foreach (var square in remaining)
      {
        SquareFunctions[square] = SameSquare;
      }
    }

    public override int Number
    {
      get { return 84; }
    }

    private static MonopolySquare ApplyMovement(MonopolySquare square, int amount)
    {
      var value = (((int)square) + amount) % Squares.Length;
      return (MonopolySquare)value;
    }

    private static MonopolySquare SameSquare(MonopolySquare square)
    {
      return square;
    }

    private static MonopolySquare CommunityChest(MonopolySquare square)
    {
      var card = random.Next(16) + 1;
      if (card == 1) { return MonopolySquare.GO; }
      if (card == 2) { return MonopolySquare.JAIL; }
      return square;
    }

    private static MonopolySquare Chance(MonopolySquare square)
    {
      var retval = square;
      var card = random.Next(16) + 1;

      switch (card)
      {
        case 1:
        {
          retval = MonopolySquare.GO;
          break;
        }
        case 2:
        {
          retval = MonopolySquare.JAIL;
          break;
        }
        case 3:
        {
          retval = MonopolySquare.C1;
          break;
        }
        case 4:
        {
          retval = MonopolySquare.E3;
          break;
        }
        case 5:
        {
          retval = MonopolySquare.H2;
          break;
        }
        case 6:
        {
          retval = MonopolySquare.R1;
          break;
        }
        case 7:
        {
          retval = Next(square, Railways);
          break;
        }
        case 8:
        {
          retval = Next(square, Railways);
          break;
        }
        case 9:
        {
          retval = Next(square, Utilities);
          break;
        }
        case 10:
        {
          retval = ApplyMovement(square, -3);
          break;
        }
      }

      return retval;
    }

    private static MonopolySquare Next(MonopolySquare square, params MonopolySquare[] choices)
    {
      // select the closest square
      return (from c in choices
        select new {Choice = c, Diff = Math.Abs(((int)c) - ((int)square))})
        .OrderBy(a => a.Diff)
        .First()
        .Choice;
    }

    public override object Solve()
    {
      // play a monopoly game with 1000 rolls
      var rolls = 500000;
      var counts = PlayMonopolyGame(rolls, 4);

      // convert the counts to percentages
      var mostPopular = (from kvp in counts
        select new
        {
          Square = kvp.Key,
          Percent = decimal.Divide(kvp.Value, rolls)
        })
        .OrderByDescending(a => a.Percent)
        .Take(3)
        .Select(a => a.Square)
        .ToArray();

      // create a modal string from the values
      return string.Format("{0:00}{1:00}{2:00}",
        ((int)mostPopular[0]),
        ((int)mostPopular[1]),
        ((int)mostPopular[2]));
    }

    protected Dictionary<MonopolySquare, int> PlayMonopolyGame(int rolls, int numSides)
    {
      var retval = Squares.ToDictionary(k => k, v => 0);
      var current = MonopolySquare.GO;
      retval[current]++;

      var doubleRollCount = 0;
      for (var r = 0; r < rolls; ++r)
      {
        // roll two numSided dice
        var dice1 = random.Next(numSides) + 1;
        var dice2 = random.Next(numSides) + 1;

        // if the two numbers are the same, then update the double rollCount
        doubleRollCount = (dice1 == dice2) ? doubleRollCount + 1 : 0;

        // if the double roll count is 3, then go to jail
        if (doubleRollCount == 3)
        {
          // reset the roll count
          doubleRollCount = 0;

          // don't apply the dice movement, just go directly to jail
          current = MonopolySquare.JAIL;
        }
        else
        {
          // move that number of squares
          var next = ApplyMovement(current, dice1 + dice2);

          // apply the function to the next square and set it as the current
          current = SquareFunctions[next](next);
        }

        // update the count
        retval[current]++;
      }

      return retval;
    }

    #region Nested type: MonopolySquare

    protected enum MonopolySquare : short
    {
      GO,
      A1,
      CC1,
      A2,
      T1,
      R1,
      B1,
      CH1,
      B2,
      B3,
      JAIL,
      C1,
      U1,
      C2,
      C3,
      R2,
      D1,
      CC2,
      D2,
      D3,
      FP,
      E1,
      CH2,
      E2,
      E3,
      R3,
      F1,
      F2,
      U2,
      F3,
      G2J,
      G1,
      G2,
      CC3,
      G3,
      R4,
      CH3,
      H1,
      T2,
      H2,
    };

    #endregion
  }
}