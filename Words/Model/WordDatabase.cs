using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.Model
{
  public class WordDatabase
  {
    public IReadOnlyList<string> Languages { get; }
    public IReadOnlyList<IReadOnlyList<string>> Words { get; }

    public WordDatabase(IReadOnlyList<string> languages, IReadOnlyList<IReadOnlyList<string>> words)
    {
      Languages = languages;
      Words = words;
    }
  }
}
