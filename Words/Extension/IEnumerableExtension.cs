using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Extension
{
  public static class IEnumerableExtension
  {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
      var wRandom = new Random();
      return enumerable.OrderBy((wItem) => wRandom.Next());
    }
  }
}
