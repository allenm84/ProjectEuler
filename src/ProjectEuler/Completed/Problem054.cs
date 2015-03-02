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

namespace ProjectEuler
{
  public class Problem054 : EulerProblem
  {
    public override int Number { get { return 54; } }

    public override object Solve()
    {
      return Resources.Problem054Data
        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => PlayerOneWins(line))
        .Count(l => l);
    }

    private bool PlayerOneWins(string line)
    {
      // I want to get the first five cards. Each card is made up
      // of two characters (5*2), and there is a space between each
      // card (5*2)+4 = 14.
      var player1 = Poker.EvaluateHand(line.Substring(0, 14));

      // I want to get the last five cards. Since we stopped at 14,
      // we want to start at 15 (to exclude the space)
      var player2 = Poker.EvaluateHand(line.Substring(15));

      // if player1 is less than player2
      return player1 < player2;
    }
  }
}
