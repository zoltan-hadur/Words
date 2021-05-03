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

    private void ComboBox_KeyDown(object sender, KeyEventArgs e)
    {
      var wComboBox = sender as ComboBox;
      if (wComboBox != null)
      {
        if (!wComboBox.IsDropDownOpen && (e.Key == Key.Enter || e.Key == Key.Space))
        {
          wComboBox.IsDropDownOpen = true;
        }
        else if (wComboBox.IsDropDownOpen && e.Key == Key.Space)
        {
          var wFocusedComboboxItem = Enumerable.Range(0, wComboBox.Items.Count)
              .Select(wIndex => wComboBox.ItemContainerGenerator.ContainerFromIndex(wIndex) as ComboBoxItem)
              .FirstOrDefault(wComboBoxItem => wComboBoxItem.IsKeyboardFocused);
          wComboBox.SelectedItem = wFocusedComboboxItem.Content;
          wComboBox.IsDropDownOpen = false;
        }
      }
    }
  }
}
