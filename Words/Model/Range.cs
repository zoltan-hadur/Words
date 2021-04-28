using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Model
{
  public class Range
  {
    public bool IncludeMin { get; set; } = true;
    public bool IncludeMax { get; set; } = true;

    public double Min { get; set; } = double.MinValue;
    public double Max { get; set; } = double.MaxValue;

    public bool Contains(double value)
    {
      return (IncludeMin ? (Min   <= value) :
                           (Min   <  value))
                           &&
             (IncludeMax ? (value <= Max) :
                           (value <  Max));
    }

    public override string ToString()
    {
      return $"{(IncludeMin ? "[" : "(")}{Min},{Max}{(IncludeMax ? "]" : ")")}";
    }
  }
}
