using System;
using System.Collections.Generic;
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
  /// Interaction logic for ResultView.xaml
  /// </summary>
  public partial class ResultView : Page, IView<ResultVM>
  {
    public ResultVM ViewModel
    {
      get => DataContext as ResultVM;
      set => DataContext = value;
    }

    public ResultView(ResultVM resultVM)
    {
      InitializeComponent();
      ViewModel = resultVM;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      btnRestart.Focus();
    }

    private void btnRestart_Click(object sender, RoutedEventArgs e)
    {
      ViewModel.SaveResult();
      NavigationService.Navigate(new SettingsView());
    }
  }
}
