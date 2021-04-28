using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Words.ViewModel
{
  public class AnswerVM : ViewModelBase
  {
    private Mode mMode;
    public Mode Mode
    {
      get => mMode;
      set => Set(ref mMode, value);
    }

    private string mAnswer;
    public string Answer
    {
      get => mAnswer;
      private set => Set(ref mAnswer, value);
    }

    private List<string> mChoices;
    public List<string> Choices
    {
      get => mChoices;
      private set => Set(ref mChoices, value);
    }

    private bool mAnswered;
    public bool Answered
    {
      get => mAnswered;
      private set => Set(ref mAnswered, value);
    }

    private bool mIsCorrect;
    public bool IsCorrect
    {
      get => mIsCorrect;
      private set => Set(ref mIsCorrect, value);
    }

    public AnswerVM(string answer, List<string> choices, Mode mode)
    {
      Reset(answer, choices);
      Mode = mode;
    }

    public void Check(int choice)
    {
      IsCorrect = Choices[choice] == Answer;
      Answered = true;
    }

    public void Check(string answer)
    {
      IsCorrect = answer == Answer;
      Answered = true;
    }

    public void Reset(string answer, List<string> choices)
    {
      Answer = answer;
      Choices = choices;
      Answered = false;
      IsCorrect = false;
    }
  }
}
