using TMPro;
using UnityEngine;

public class CorrectAnswerState : GameState
{
    private float _currentDelta = 0;

    public override void Enter()
    {
        UIManager.SwitchToView(EView.Trivia);
        ScoreManager.CalculateEndTurnScore(true);
        AudioManager.PlayVFX(EAudioClip.CorrectAnswer);
    }

    public override void Exit()
    {
        Debug.Log("Exiting CorrectAnswerState");
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