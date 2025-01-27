using TMPro;
using UnityEngine;

public class NextPlayerTurnState : GameState
{
    private float _currentDelta = 0;
    private TriviaQuestion _triviaQuestion;
    public override void Enter()
    {
        GameplayManager.NextTurn();
        UIManager.SwitchToView(EView.NextPlayerTurn);

        _triviaQuestion = GameplayManager.GetRandomQuestion();

        if (!_triviaQuestion)
        {
            GameStateMachine.NextState(new GameEndState(EGameEndedReason.NoMoreQuestions));
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting NextPlayerTurnState");
    }

    public override void Update()
    {
        _currentDelta += Time.deltaTime;

        if (_currentDelta >= 3f)
        {
            GameStateMachine.NextState(new TriviaState(_triviaQuestion));
            return;
        }
    }
}