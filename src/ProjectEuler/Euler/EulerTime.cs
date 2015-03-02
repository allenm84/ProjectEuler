using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class EulerTime
  {
    public Func<TimeSpan, double> Time { get; set; }
    public double Maximum { get; set; }
    public string Units { get; set; }

    public static EulerTime Create(Func<TimeSpan, double> time, double maximum, string units)
    {
      return new EulerTime
      {
        Time = time,
        Maximum = maximum,
        Units = units,
      };
    }
  }
}