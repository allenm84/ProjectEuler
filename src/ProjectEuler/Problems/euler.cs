using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public abstract class euler
  {
    static Dictionary<int, Func<euler>> sFactoryCache = new Dictionary<int, Func<euler>>();

    public abstract void Run();

    public static void Solve<T>() where T : euler, new()
    {
      new T().Run();
    }

    public static void Solve(int number)
    {
      Func<euler> factory;

      if (!sFactoryCache.TryGetValue(number, out factory))
      {
        var type = Type.GetType(string.Format("ProjectEuler.p{0:0000}", number));
        var exp = Expression.New(type);
        var lambda = LambdaExpression.Lambda<Func<euler>>(exp);
        factory = lambda.Compile();
        sFactoryCache[number] = factory;
      }

      factory().Run();
    }
  }
}
