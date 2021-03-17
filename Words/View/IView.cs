using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words.ViewModel;

namespace Words.View
{
  public interface IView<T> where T : ViewModelBase
  {
    T ViewModel { get; set; }
  }
}
