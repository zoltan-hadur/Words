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
using System.Windows.Threading;
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
      switch (e.PropertyName)
      {
        case nameof(ViewModel.Answer):
          // If the question changes, clear the previous answer
          txtAnswer.Text = string.Empty;
          Dispatcher.BeginInvoke(() => { txtAnswer.Focus(); }, DispatcherPriority.ApplicationIdle);
          break;
      }
    }

    // Allows to "click" the check button by pressing enter on the keyboard
    private void txtAnswer_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        btnCheckAnswer_Click(btnCheckAnswer, new RoutedEventArgs());
      }
    }

    private void btnCheckAnswer_Click(object sender, RoutedEventArgs e)
    {
      ViewModel.Check(txtAnswer.Text);
    }
  }
}
