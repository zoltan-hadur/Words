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
  public class ExcelFile
  {
    public IReadOnlyDictionary<string, WordDatabase> Sheets { get; }  // Each sheet is a word database

    public ExcelFile(string path)
    {
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      using (var wStream = File.Open(path, FileMode.Open, FileAccess.Read))
      using (var wReader = ExcelReaderFactory.CreateReader(wStream))
      using (var wDataSet = wReader.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = wExcelReader => new ExcelDataTableConfiguration() { UseHeaderRow = true } }))
      {
        Sheets = wDataSet.Tables.Cast<DataTable>()
          .ToDictionary(wTable => wTable.TableName, wTable =>
          {
            var wLanguages = wTable.Columns.Cast<DataColumn>()
              .Select(wDataColumn => wDataColumn.ColumnName)
              .ToList();
            var wWords = wTable.Rows.Cast<DataRow>()
              .Select(wDataRow => wDataRow.ItemArray.Cast<string>().ToList())
              .ToList();
            return new WordDatabase(wLanguages, wWords);
          });
      }
    }
  }
}
