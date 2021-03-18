using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words.Extension;
using Words.Model;

namespace Words.ViewModel
{
  public class QuestionVM : ViewModelBase
  {
    private SettingsVM mSettingsVM;
    public SettingsVM SettingsVM
    {
      get => mSettingsVM;
      private set => Set(ref mSettingsVM, value);
    }

    private IReadOnlyList<Question> mQuestions;

    private Question mCurrentQuestion;
    public Question CurrentQuestion
    {
      get => mCurrentQuestion;
      private set => Set(ref mCurrentQuestion, value);
    }

    private int mCurrentQuestionIndex;
    public int CurrentQuestionIndex
    {
      get => mCurrentQuestionIndex;
      private set => Set(ref mCurrentQuestionIndex, value);
    }

    private bool mIsAnswered;
    public bool IsAnswered
    {
      get => mIsAnswered;
      private set => Set(ref mIsAnswered, value);
    }

    private AnswerVM mAnserVM;
    public AnswerVM AnswerVM
    {
      get => mAnserVM;
      private set => Set(ref mAnserVM, value);
    }

    private int mGoodAnswers;
    public int GoodAnswers
    {
      get => mGoodAnswers;
      set => Set(ref mGoodAnswers, value);
    }

    public QuestionVM(SettingsVM settingsVM)
    {
      SettingsVM = settingsVM;
      var wFromIndex = SettingsVM.WordDatabase.Languages.ToList().IndexOf(SettingsVM.From);
      var wToIndex = SettingsVM.WordDatabase.Languages.ToList().IndexOf(SettingsVM.To);
      var wCandidates = SettingsVM.WordDatabase.Words.Shuffle().Take(SettingsVM.WantedWordCount).ToList();
      var wAllChoice = SettingsVM.WordDatabase.Words.Select(wWords => wWords[wToIndex]);
      mQuestions = wCandidates.Select(wCandidate => new Question()
      {
        Word = wCandidate[wFromIndex],
        Answer = wCandidate[wToIndex],
        Choices = wAllChoice.Where(wChoice => wChoice != wCandidate[wToIndex]).Shuffle().Take(3).Append(wCandidate[wToIndex]).Shuffle().ToList(),
      }).ToList().AsReadOnly();
      CurrentQuestionIndex = 0;
      CurrentQuestion = mQuestions[CurrentQuestionIndex];
      AnswerVM = new AnswerVM(CurrentQuestion.Answer, CurrentQuestion.Choices);
      AnswerVM.PropertyChanged += AnswerVM_PropertyChanged;
      GoodAnswers = 0;
    }

    private void AnswerVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case nameof(AnswerVM.Answered):
          IsAnswered = AnswerVM.Answered;
          break;
        case nameof(AnswerVM.IsCorrect):
          if (AnswerVM.IsCorrect)
          {
            GoodAnswers++;
          }
          break;
      }
    }

    public bool HasMoreQuestion()
    {
      return CurrentQuestionIndex < SettingsVM.WantedWordCount - 1;
    }

    public void NextQuestion()
    {
      CurrentQuestion = mQuestions[++CurrentQuestionIndex];
      AnswerVM.Reset(CurrentQuestion.Answer, CurrentQuestion.Choices);
    }
  }
}
