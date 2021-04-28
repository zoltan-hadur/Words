using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Model
{

  public class ReadOnlyWeightedList<T> : IReadOnlyList<(T Item, double Weight)>
  {
    private static Random mRandom = new Random();
    private IReadOnlyList<(T Item, Range Range)> mItems;

    public ReadOnlyWeightedList(IReadOnlyList<(T Item, double Weight)> items)
    {
      var wStart = 0D;
      mItems = items.Select(wItem =>
      {
        var wResult = (
          wItem.Item,
          new Range()
          {
            IncludeMin = true,
            IncludeMax = false,
            Min = wStart,
            Max = wStart + wItem.Weight
          }
        );
        wStart = wStart + wItem.Weight;
        return wResult;
      }).ToList();
    }

    public (T Item, double Weight) this[int index]
    {
      get => (mItems[index].Item, mItems[index].Range.Max - mItems[index].Range.Min);
    }

    public int Count
    {
      get => mItems.Count;
    }

    public IEnumerator<(T Item, double Weight)> GetEnumerator()
    {
      foreach (var wItem in mItems)
      {
        yield return (wItem.Item, wItem.Range.Max - wItem.Range.Min);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public (T Item, double Weight) GetRandomElement()
    {
      var wChoice = mRandom.NextDouble() * mItems.Last().Range.Max;
      var wChosen = mItems.Single(wItem => wItem.Range.Contains(wChoice));
      return (wChosen.Item, wChosen.Range.Max - wChosen.Range.Min);
    }

    public IReadOnlyList<(T Item, double Weight)> GetRandomElements(int count)
    {
      var wResult = new List<(T Item, double Weight)>();
      while (wResult.Count < count)
      {
        var wItem = GetRandomElement();
        if (!wResult.Contains(wItem))
        {
          wResult.Add(wItem);
        }
      }
      return wResult;
    }
  }
}
