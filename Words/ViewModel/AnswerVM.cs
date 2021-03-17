using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.ViewModel
{
  public class AnswerVM : ViewModelBase
  {
    private string mAnswer;
    public string Answer
    {
      get => mAnswer;
      set => Set(ref mAnswer, value);
    }

    private string[] mChoices;
    public string[] Choices
    {
      get => mChoices;
      set => Set(ref mChoices, value);
    }

    private bool mAnswered;
    public bool Answered
    {
      get => mAnswered;
      set => Set(ref mAnswered, value);
    }

    private bool mIsCorrect;
    public bool IsCorrect
    {
      get => mIsCorrect;
      set => Set(ref mIsCorrect, value);
    }

    public AnswerVM(string answer, string[] choices)
    {
      Reset(answer, choices);
    }

    public void Reset(string answer, string[] choices)
    {
      Answer = answer;
      Choices = choices;
      Answered = false;
      IsCorrect = false;
    }
  }
}
