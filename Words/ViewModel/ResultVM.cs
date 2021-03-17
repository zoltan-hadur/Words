using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words.Extension;

namespace Words.ViewModel
{
  public class ResultVM : ViewModelBase
  {
    private int mCorrect;
    public int Correct
    {
      get => mCorrect;
      set => Set(ref mCorrect, value);
    }

    private int mTotal;
    public int Total
    {
      get => mTotal;
      set => Set(ref mTotal, value);
    }

    private SettingsVM mSettingsVM;
    public SettingsVM SettingsVM
    {
      get => mSettingsVM;
      set => Set(ref mSettingsVM, value);
    }

    public void SaveResult()
    {
      var wResult = $"{DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz")}\r\n" +
                    $"  Sheet: {SettingsVM.Sheet}\r\n" +
                    $"  From: {SettingsVM.From}\r\n" +
                    $"  To: {SettingsVM.To}\r\n" +
                    $"  Mode: {SettingsVM.Mode.GetDescription()}\r\n" +
                    $"  Name: {SettingsVM.Name}\r\n" +
                    $"  Correct: {Correct}\r\n" +
                    $"  Wrong: {Total - Correct}\r\n" +
                    $"  Total: {Total}\r\n\r\n";
      File.AppendAllText("Results.txt", wResult);
    }
  }
}
