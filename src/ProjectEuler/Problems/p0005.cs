using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0005 : euler
  {
    public override void Run()
    {
      var numbers = Enumerable.Range(1, 20).ToArray();

      var result = 0;
      var i = 20;

      while (result == 0)
      {
        if (numbers.All(n => (i % n) == 0))
        {
          result = i;
        }
        ++i;
      }

      Console.WriteLine(result);
    }
  }
}
