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
  /// Interaction logic for MultipleChoiceView.xaml
  /// </summary>
  public partial class MultipleChoiceView : UserControl, IView<AnswerVM>
  {
    public AnswerVM ViewModel
    {
      get => DataContext as AnswerVM;
      set => DataContext = value;
    }

    private Button[] mChoices;

    public MultipleChoiceView()
    {
      InitializeComponent();
      mChoices = new Button[] { Choice0, Choice1, Choice2, Choice3 };
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
      if (ViewModel != null)
      {
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
      }
      if (Application.Current.MainWindow != null)
      {
        Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;
      }
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e)
    {
      if (ViewModel != null)
      {
        ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
      }
      if (Application.Current.MainWindow != null)
      {
        Application.Current.MainWindow.KeyDown -= MainWindow_KeyDown;
      }
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(ViewModel.Answered))
      {
        if (!ViewModel.Answered)
        {
          foreach (var wButton in mChoices)
          {
            wButton.ClearValue(BackgroundProperty);
          }
        }
      }
    }

    private void ChoiceButton_Click(object sender, RoutedEventArgs e)
    {
      var wButton = sender as Button;
      if (wButton != null)
      {
        if (wButton.Tag as string == ViewModel.Answer)
        {
          ViewModel.IsCorrect = true;
          wButton.Background = Brushes.Green;
        }
        else
        {
          wButton.Background = Brushes.Red;
          var wAnswerButton = mChoices.Single(x => x.Tag as string == ViewModel.Answer);
          wAnswerButton.Background = Brushes.Green;
        }
        ViewModel.Answered = true;
      }
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.D1:
        case Key.NumPad1:
          ChoiceButton_Click(mChoices[0], new RoutedEventArgs());
          break;
        case Key.D2:
        case Key.NumPad2:
          ChoiceButton_Click(mChoices[1], new RoutedEventArgs());
          break;
        case Key.D3:
        case Key.NumPad3:
          ChoiceButton_Click(mChoices[2], new RoutedEventArgs());
          break;
        case Key.D4:
        case Key.NumPad4:
          ChoiceButton_Click(mChoices[3], new RoutedEventArgs());
          break;
      }
    }
  }
}
