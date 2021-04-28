using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words.Extension;
using Words.Model;

namespace Words.ViewModel
{
  public class ResultVM : ViewModelBase
  {
    private IReadOnlyList<Question> mQuestions;
    public IReadOnlyList<Question> Questions
    {
      get => mQuestions;
      set => Set(ref mQuestions, value);
    }

    public int Correct
    {
      get => Questions.Count(wQuestion => wQuestion.Correct);
    }

    public int Total
    {
      get => Questions.Count;
    }

    private SettingsVM mSettingsVM;
    public SettingsVM SettingsVM
    {
      get => mSettingsVM;
      set => Set(ref mSettingsVM, value);
    }

    public void SaveResult()
    {
      using (var wExcelPackage = new ExcelPackage(new FileInfo("Words.xlsx")))
      {
        var wWorksheet = wExcelPackage.Workbook.Worksheets[SettingsVM.Sheet];
        foreach(var wQuestion in Questions)
        {
          for (int wRow = 2; wRow <= wWorksheet.Dimension.End.Row; ++wRow)
          {
            var wCells = wWorksheet.Cells[$"{wRow}:{wRow}"];
            if (wCells.Any(wCell => wCell.Text == wQuestion.Word))
            {
              if (wQuestion.Correct)
              {
                wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].Value = wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].GetValue<double>() * 0.5;
              }
              else
              {
                wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].Value = wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].GetValue<double>() * 2.0;
              }
            }
          }
        }
        wExcelPackage.Save();
      }

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
