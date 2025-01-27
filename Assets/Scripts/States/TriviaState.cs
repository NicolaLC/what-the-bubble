using TMPro;
using UnityEngine;

public class TriviaState : GameState
{
    private TriviaViewController _triviaViewController;
    private TriviaQuestion _triviaQuestion;
    private int _lastPenaltyTime = 0;

    public TriviaState(TriviaQuestion triviaQuestion)
    {
        _triviaQuestion = triviaQuestion;
    }

    public override void Enter()
    {
        AudioManager.PlayBackgroundMusic(EAudioClip.GameMusic);

        TimeManager.StartTurnTime();
        UIManager.SwitchToView(EView.Trivia);

        _triviaViewController = Object.FindAnyObjectByType<TriviaViewController>();
        _triviaViewController.SetQuestion(_triviaQuestion);
        _triviaViewController.onAnswerSelected += HandleAnswerSelected;

        _lastPenaltyTime = (int)TimeManager.GetGameTime();

        GameplayManager.Instance.onBubbleScaleChanged += HandleBubbleScaleChanged;
    }

    public override void Exit()
    {
        Debug.Log("Exiting TriviaState");
        TimeManager.PauseTurnTime();

        _triviaViewController.onAnswerSelected -= HandleAnswerSelected;
        GameplayManager.Instance.onBubbleScaleChanged += HandleBubbleScaleChanged;
    }

    public override void Update()
    {
        int gameTime = Mathf.FloorToInt(TimeManager.GetGameTime());

        if (gameTime >= _lastPenaltyTime + 10)
        {
            GameplayManager.Apply10SecondsPenalty();
            _lastPenaltyTime += 10;
        }
    }

    private void HandleBubbleScaleChanged(float currentScale)
    {
        float progress = (currentScale - 0.5f) * 2f;
        AudioManager.SetBackgroundMusicPitch(1 + progress / 2f);
        if (currentScale >= 1.0f)
        {
            GameStateMachine.NextState(new GameEndState(EGameEndedReason.BubbleExploded));
        }
    }

    private void HandleAnswerSelected(int choice)
    {
        if (_triviaQuestion.correctAnswerIndex == choice)
        {
            GameStateMachine.NextState(new CorrectAnswerState());
        }
        else
        {
            GameStateMachine.NextState(new IncorrectAnswerState());
        }
    }


}