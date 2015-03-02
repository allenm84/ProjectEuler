using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public abstract class EulerProblem
  {
    public abstract int Number { get; }

    public virtual string Url
    {
      get { return string.Format("http://projecteuler.net/problem={0}", Number); }
    }

    public string Display
    {
      get { return string.Format("Problem {0:000}", Number); }
    }

    private Action<string> mAddMessage;
    public void Initialize(Action<string> addMessage)
    {
      mAddMessage = addMessage;
    }

    public abstract object Solve();

    protected void AddMessage(string message)
    {
      mAddMessage(message);
    }

    protected void AddMessage(string format, params object[] args)
    {
      mAddMessage(string.Format(format, args));
    }
  }
}
