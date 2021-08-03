using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace Words.Model
{
  public class ExcelFile
  {
    public IReadOnlyDictionary<string, WordDatabase> Sheets { get; }  // Each sheet is a word database

    public ExcelFile(string path)
    {
      ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
      using (var wExcelPackage = new ExcelPackage(new FileInfo(path)))
      {
        var wFixed = false;
        foreach (var wWorksheet in wExcelPackage.Workbook.Worksheets)
        {
          // If there is no "Weight" column
          if (wWorksheet.Cells["1:1"].Last().Text != "Weight")
          {
            // Create it
            var wColumn = wWorksheet.Dimension.End.Column + 1;
            wWorksheet.Cells[1, wColumn].Value = "Weight";
            wFixed = true;
          }
          // Fill the cells with default weights where the weights are missing
          for (int wRow = 2; wRow <= wWorksheet.Dimension.End.Row; ++wRow)
          {
            // But only for those rows whose actually contain any data and they are not null or whitespace
            var wCells = wWorksheet.Cells[$"{wRow}:{wRow}"];
            if (wCells.Any() && wCells.All(wCell => !string.IsNullOrWhiteSpace(wCell.Text)) &&
                string.IsNullOrWhiteSpace(wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].Text))
            {
              wWorksheet.Cells[wRow, wWorksheet.Dimension.End.Column].Value = 1D;
              wFixed = true;
            }
          }
        }
        Sheets = wExcelPackage.Workbook.Worksheets.ToDictionary(
          wWorksheet =>
          {
            return wWorksheet.Name;
          },
          wWorksheet =>
          {
            var wLanguages = wWorksheet.Cells["1:1"].SkipLast(1).Select(wCell => wCell.Text).ToList();
            var wRows = Enumerable
              .Range(2, wWorksheet.Dimension.End.Row - 1)                                                           // All row index except first
              .Select(wRowIndex => wWorksheet.Cells[$"{wRowIndex}:{wRowIndex}"])                                    // All row except first
              .ToList();
            var wValidRows = wRows
              .Where(wCells => wCells.Any() &&                                                                      // Only those rows whose actually contain any data
                               wCells.All(wCell => !string.IsNullOrWhiteSpace(wCell.Text)))                         // And they are not null or whitespace
              .ToList();
            var wWords = wValidRows
              .Select(wCells => (wCells.SkipLast(1).Select(wCell => wCell.Text).ToList() as IReadOnlyList<string>,  // Words with same meaning in different languages
                                 wCells.Last().GetValue<double>()))                                                 // Their weight
              .ToList();
            return new WordDatabase(wLanguages, wWords);
          });
        if (wFixed)
        {
          wExcelPackage.Save();
        }
      }
    }
  }
}
