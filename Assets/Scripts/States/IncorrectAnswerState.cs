using UnityEngine;

public class IncorrectAnswerState : GameState
{
    private float _currentDelta = 0;

    public override void Enter()
    {
        UIManager.SwitchToView(EView.Trivia);
        GameplayManager.ApplyWrongAnswerPenalty();
        ScoreManager.CalculateEndTurnScore(false);
        AudioManager.PlayVFX(EAudioClip.WrongAnswer);
    }

    public override void Exit()
    {
        Debug.Log("Exiting IncorrectAnswerState");
    }

    public override void Update()
    {
        _currentDelta += Time.deltaTime;

        if (_currentDelta >= 3f)
        {
            GameStateMachine.NextState(new NextPlayerTurnState());
        }
    }
}