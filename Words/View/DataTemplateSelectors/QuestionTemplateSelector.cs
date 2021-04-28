using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Words.ViewModel;

namespace Words.View.DataTemplateSelectors
{
  public class QuestionTemplateSelector : DataTemplateSelector
  {
    public DataTemplate MultipleChoiceTemplate { get; set; }

    public DataTemplate ShortAnswerTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      var wAnswerVM = (AnswerVM)item;
      switch (wAnswerVM?.Mode)
      {
        case Mode.MultipleChoice:
          return MultipleChoiceTemplate;
        case Mode.ShortAnswer:
          return ShortAnswerTemplate;
        default:
          return base.SelectTemplate(item, container);
      }
    }
  }
}
