using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public abstract class euler
  {
    public abstract void Run();

    public static void Solve<T>() where T : euler, new()
    {
      new T().Run();
    }
  }
}
