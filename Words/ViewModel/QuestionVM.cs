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
    public IReadOnlyList<Question> Questions
    {
      get => mQuestions;
    }

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

    public QuestionVM(SettingsVM settingsVM)
    {
      SettingsVM = settingsVM;
      var wFromIndex = SettingsVM.WordDatabase.Languages.ToList().IndexOf(SettingsVM.From);
      var wToIndex = SettingsVM.WordDatabase.Languages.ToList().IndexOf(SettingsVM.To);
      var wCandidates = SettingsVM.WordDatabase.Words.GetRandomElements(SettingsVM.WantedWordCount).ToList();
      var wAllChoice = SettingsVM.WordDatabase.Words.Select(wWords => wWords.Item[wToIndex]);
      mQuestions = wCandidates.Select(wCandidate => new Question()
      {
        Word = wCandidate.Item[wFromIndex],
        Answer = wCandidate.Item[wToIndex],
        Choices = wAllChoice.Where(wChoice => wChoice != wCandidate.Item[wToIndex]).Shuffle().Take(3).Append(wCandidate.Item[wToIndex]).Shuffle().ToList(),
        Correct = false
      }).ToList().AsReadOnly();
      CurrentQuestionIndex = 0;
      CurrentQuestion = mQuestions[CurrentQuestionIndex];
      AnswerVM = new AnswerVM(CurrentQuestion.Answer, CurrentQuestion.Choices);
      AnswerVM.PropertyChanged += AnswerVM_PropertyChanged;
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
            CurrentQuestion.Correct = true;
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
