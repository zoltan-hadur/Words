using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Words.ViewModel;

namespace Words.View
{
  /// <summary>
  /// Interaction logic for ShortAnswerView.xaml
  /// </summary>
  public partial class ShortAnswerView : UserControl, IView<AnswerVM>
  {
    public AnswerVM ViewModel
    {
      get => DataContext as AnswerVM;
      set => DataContext = value;
    }

    public ShortAnswerView()
    {
      InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      if (ViewModel != null)
      {
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
      }
      txtAnswer.Focus();
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e)
    {
      if (ViewModel != null)
      {
        ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
      }
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(ViewModel.Answered))
      {
        if (!ViewModel.Answered)
        {
          txtAnswer.Text = string.Empty;
          btnCheckAnswer.ClearValue(BackgroundProperty);
          txtAnswer.Focus();
        }
      }
    }

    private void CheckAnswer()
    {
      if (txtAnswer.Text == ViewModel.Answer)
      {
        ViewModel.IsCorrect = true;
        btnCheckAnswer.Background = Brushes.Green;
      }
      else
      {
        btnCheckAnswer.Background = Brushes.Red;
      }
      ViewModel.Answered = true;
    }

    private void txtAnswer_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        CheckAnswer();
      }
    }

    private void btnCheckAnswer_Click(object sender, RoutedEventArgs e)
    {
      CheckAnswer();
    }
  }
}
