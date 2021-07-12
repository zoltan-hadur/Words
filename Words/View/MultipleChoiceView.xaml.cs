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

    private List<Button> mChoices;  // Reference to the 4 choice button

    public MultipleChoiceView()
    {
      InitializeComponent();
      mChoices = new List<Button>() { Choice0, Choice1, Choice2, Choice3 };
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
      switch (e.PropertyName)
      {
        // If the question changes, clear the background color for all choice button
        case nameof(ViewModel.Answer):
          foreach (var wButton in mChoices)
          {
            wButton.ClearValue(BackgroundProperty);
          }
          break;
      }
    }

    private void ChoiceButton_Click(object sender, RoutedEventArgs e)
    {
      var wButton = sender as Button;
      if (wButton != null)
      {
        // Check if our choice is the correct answer or not
        ViewModel.Check(Convert.ToInt32(wButton.Tag));
        // If correct, indicate it with a green color
        if (ViewModel.IsCorrect)
        {
          wButton.Background = Brushes.Green;
        }
        // Otherwise, indicate it with a red color, and make the correct choice button green to indicate which choice button was the correct answer
        else
        {
          wButton.Background = Brushes.Red;
          mChoices[ViewModel.Choices.FindIndex(wChoice => wChoice == ViewModel.Answer)].Background = Brushes.Green;
        }
      }
    }

    // Allows to "click" the buttons by pressing their number on the keyboard
    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
      if (ViewModel == null || ViewModel.Answered) return;
      switch (e.Key)
      {
        case Key.D1:
        case Key.D2:
        case Key.D3:
        case Key.D4:
        case Key.NumPad1:
        case Key.NumPad2:
        case Key.NumPad3:
        case Key.NumPad4:
          var wIndex = Convert.ToInt32(e.Key.ToString().Last().ToString()) - 1;
          ChoiceButton_Click(mChoices[wIndex], new RoutedEventArgs());
          break;
      }
    }
  }
}
