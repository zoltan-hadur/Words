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

namespace Words
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
      var wResult = MessageBox.Show("Are you sure you want to quit the application?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
      e.Cancel = wResult != MessageBoxResult.Yes;
    }
  }
}
