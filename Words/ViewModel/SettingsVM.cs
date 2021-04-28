using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Words.Model;

namespace Words.ViewModel
{
  public class SettingsVM : ViewModelBase
  {
    private readonly string mSettingsPath = "Settings.json";

    private ExcelFile mExcelFile;
    [JsonIgnore]
    public ExcelFile ExcelFile
    {
      get => mExcelFile;
      private set => Set(ref mExcelFile, value);
    }

    private WordDatabase mWordDatabase;
    [JsonIgnore]
    public WordDatabase WordDatabase
    {
      get => mWordDatabase;
      private set => Set(ref mWordDatabase, value);
    }

    private string mSheet;
    public string Sheet
    {
      get => mSheet;
      set => Set(ref mSheet, value);
    }

    private string mFrom;
    public string From
    {
      get => mFrom;
      set
      {
        if (value == mTo)
        {
          var wFrom = mFrom;
          Set(ref mFrom, value);
          To = wFrom;
        }
        else
        {
          Set(ref mFrom, value);
        }
      }
    }

    private string mTo;
    public string To
    {
      get => mTo;
      set
      {
        if (value == mFrom)
        {
          var wTo = mTo;
          Set(ref mTo, value);
          From = wTo;
        }
        else
        {
          Set(ref mTo, value);
        }
      }
    }

    [JsonIgnore]
    public Mode[] Modes => Enum.GetValues<Mode>();

    private Mode mMode;
    public Mode Mode
    {
      get => mMode;
      set => Set(ref mMode, value);
    }

    private int mWantedWordCount;
    public int WantedWordCount
    {
      get => mWantedWordCount;
      set => Set(ref mWantedWordCount, value);
    }

    private string mName;
    public string Name
    {
      get => mName;
      set => Set(ref mName, value);
    }

    public SettingsVM()
    {
      ExcelFile = new ExcelFile("Words.xlsx");

      var wSuccessful = true;

      try
      {
        var wSettings = JsonDocument.Parse(File.ReadAllText(mSettingsPath));
        Sheet           =       wSettings.RootElement.GetProperty("Sheet"          ).GetString();
        WordDatabase    =       ExcelFile.Sheets[Sheet];
        From            =       wSettings.RootElement.GetProperty("From"           ).GetString();
        To              =       wSettings.RootElement.GetProperty("To"             ).GetString();
        Mode            = (Mode)wSettings.RootElement.GetProperty("Mode"           ).GetInt32 ();
        WantedWordCount =       wSettings.RootElement.GetProperty("WantedWordCount").GetInt32 ();
        Name            =       wSettings.RootElement.GetProperty("Name"           ).GetString();
      }
      catch
      {
        wSuccessful = false;
      }

      PropertyChanged += SettingsVM_PropertyChanged;

      if (!wSuccessful)
      {
        Sheet = ExcelFile.Sheets.First().Key;
        Mode = Mode.MultipleChoice;
        WantedWordCount = 1;
        Name = "Default";
      }
    }

    ~SettingsVM()
    {
      PropertyChanged -= SettingsVM_PropertyChanged;
    }

    private void SettingsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      File.WriteAllText(mSettingsPath, JsonSerializer.Serialize(this));
      switch (e.PropertyName)
      {
        case nameof(Sheet):
          WordDatabase = ExcelFile.Sheets[Sheet];
          From = WordDatabase.Languages.First();
          To = WordDatabase.Languages.Skip(1).First();
          WantedWordCount = Math.Max(1, Math.Min(WantedWordCount, WordDatabase.Words.Count));
          break;
      }
    }
  }
}
