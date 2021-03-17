using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Words.Rule
{
  /// <summary>
  /// <see cref="DependencyObject"/> with <see cref="DependencyProperty"/> so we can use bindings in XAML.
  /// Implements the <see cref="INotifyPropertyChanged"/> interface so we will know when a binding updates a property.
  /// </summary>
  public class Range : DependencyObject, INotifyPropertyChanged
  {
    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(double), typeof(Range), new PropertyMetadata(OnPropertyChanged));
    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(double), typeof(Range), new PropertyMetadata(OnPropertyChanged));

    public double Min
    {
      get => (double)GetValue(MinProperty);
      set => SetValue(MinProperty, value);
    }

    public double Max
    {
      get => (double)GetValue(MaxProperty);
      set => SetValue(MaxProperty, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      (sender as Range)?.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.Property.Name));
    }
  }

  [ContentProperty("Range")]
  public class MustBeWithinRangeValidationRule : ValidationRule
  {
    private Range mRange = null;
    public Range Range
    {
      get => mRange;
      set
      {
        // We are changing the range so we unsubscribe the event
        if (mRange != null)
        {
          mRange.PropertyChanged -= Range_PropertyChanged;
        }
        mRange = value;
        // If we did not assign null to the property, we subscribe the event
        if (mRange != null)
        {
          mRange.PropertyChanged += Range_PropertyChanged;
        }
      }
    }

    private void Range_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      mBindingExpression?.UpdateSource(); // Forces the validation rule to be executed when the ranges changes
    }

    private BindingExpression mBindingExpression = null;

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      try
      {
        mBindingExpression = value as BindingExpression;
        // Get the updated bound value
        var wDataItem = mBindingExpression.DataItem;
        var wPropertyName = mBindingExpression.ParentBinding.Path.Path;
        var wNumber = (int)wDataItem.GetType().GetProperty(wPropertyName).GetValue(wDataItem, null);
        // And then check if it is within range
        if (Range.Min <= wNumber && wNumber <= Range.Max)
        {
          return new ValidationResult(true, null);
        }
        else
        {
          return new ValidationResult(false, $"The number must be within {Range.Min} and {Range.Max}!");
        }
      }
      catch (Exception e)
      {
        return new ValidationResult(false, e.ToString());
      }
    }
  }
}
