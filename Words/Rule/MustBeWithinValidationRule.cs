using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Words.Rule
{
  class MustBeWithinValidationRule : ValidationRule
  {
    public double Min { get; set; }
    public double Max { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      try
      {
        var wBinding = value as BindingExpression;
        var wDataItem = wBinding.DataItem;
        var wPropertyName = wBinding.ParentBinding.Path.Path;
        var wNumber = (int)wDataItem.GetType().GetProperty(wPropertyName).GetValue(wDataItem, null);
        if (Min <= wNumber && wNumber <= Max)
        {
          return new ValidationResult(true, null);
        }
        else
        {
          return new ValidationResult(false, $"The number must be within {Min} and {Max}!");
        }
      }
      catch
      {
        return new ValidationResult(false, $"{value} is not a number!");
      }
    }
  }
}
