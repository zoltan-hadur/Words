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
  /// Interaction logic for Start.xaml
  /// </summary>
  public partial class SettingsView : Page, IView<SettingsVM>
  {
    public SettingsVM ViewModel
    {
      get => DataContext as SettingsVM;
      set => DataContext = value;
    }

    public SettingsView()
    {
      InitializeComponent();
      ViewModel = new SettingsVM();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
      btnStart.Focus();
    }

    private void btnStart_Click(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new QuestionView(new QuestionVM(ViewModel)));
    }
  }
}
