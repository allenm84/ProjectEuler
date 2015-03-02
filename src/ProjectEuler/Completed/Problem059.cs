using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem059 : EulerProblem
  {
    private static char[] whitespace = " \t\r\n".ToArray();
    private static char[] punctuation = ";:'\",.?!-()[]{}".ToArray();
    private static char[] numbers = "0123456789".ToArray();

    private static char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".SelectMany(c =>
      new[] {char.ToUpper(c), char.ToLower(c)}).ToArray();

    static Problem059() {}

    public override int Number
    {
      get { return 59; }
    }

    public override object Solve()
    {
      // the min and max characters
      const char Min = 'a';
      const char Max = 'z';

      // get the text that is encrypted
      var cipherText = Encoding.ASCII.GetString(Resources
        .Problem059Data
        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
        .Select(t => Convert.ToByte(t.Trim()))
        .ToArray());

      // generate the keys
      for (var a = Min; a <= Max; ++a)
      {
        for (var b = Min; b <= Max; ++b)
        {
          for (var c = Min; c <= Max; ++c)
          {
            // create the key to use
            var key = new[] {a, b, c};

            // go through the cipher text XOR with the key
            var message = new char[cipherText.Length];
            for (var i = 0; i < message.Length; ++i)
            {
              message[i] = (char)(key[i % 3] ^ cipherText[i]);
            }

            // now that we have the message, convert to a string
            var text = new string(message);

            // retrieve the words
            var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            // see if all the words are valid
            if (words.All(word =>
            {
              return word
                .Except(punctuation)
                .Except(numbers)
                .Except(whitespace)
                .All(l => letters.Contains(l));
            }))
            {
              return string.Format("{0}\r\n\r\nSum: {1}",
                text,
                text.Sum(t => Convert.ToInt32(t)));
            }
          }
        }
      }

      return "<NONE>";
    }
  }
}