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
  /// Interaction logic for QuestionView.xaml
  /// </summary>
  public partial class QuestionView : Page, IView<QuestionVM>
  {
    public QuestionVM ViewModel
    {
      get => DataContext as QuestionVM;
      set => DataContext = value;
    }

    public QuestionView(QuestionVM questionVM)
    {
      InitializeComponent();
      ViewModel = questionVM;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      if (ViewModel != null)
      {
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
      }
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e)
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
        case nameof(ViewModel.IsAnswered):
          if (ViewModel.IsAnswered)
          {
            Dispatcher.BeginInvoke(() => { btnNext.Focus(); }, DispatcherPriority.ApplicationIdle);
          }
          break;
      }
    }

    private void btnNext_Click(object sender, RoutedEventArgs e)
    {
      if (ViewModel.HasMoreQuestion())
      {
        ViewModel.NextQuestion();
      }
      else
      {
        NavigationService.Navigate(new ResultView(new ResultVM()
        {
          Correct = ViewModel.GoodAnswers,
          Total = ViewModel.SettingsVM.WantedWordCount,
          SettingsVM = ViewModel.SettingsVM
        }));
      }
    }

    private void btnGoBack_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new SettingsView());
    }
  }
}
