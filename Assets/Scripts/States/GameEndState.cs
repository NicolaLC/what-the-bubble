using UnityEngine;

public enum EGameEndedReason
{
    NoMoreQuestions,
    BubbleExploded
}

public class GameEndState : GameState
{
    private EGameEndedReason _gameEndedReason;
    private GameEndController _gameEndController;
    public GameEndState(EGameEndedReason gameEndedReason)
    {
        _gameEndedReason = gameEndedReason;
    }

    public override void Enter()
    {
        AudioManager.PlayVFX(EAudioClip.BubbleExploded);
        AudioManager.PlayBackgroundMusic(EAudioClip.MenuMusic);
        AudioManager.SetBackgroundMusicPitch(1);
        Debug.Log("Entering GameEndedState");
        UIManager.SwitchToView(EView.GameEnd);

        _gameEndController = Object.FindAnyObjectByType<GameEndController>();
        _gameEndController.SetGameEndReason(_gameEndedReason);

        _gameEndController.onContinue += HandleContinue;
    }

    private void HandleContinue()
    {
        GameStateMachine.NextState(new GameStatsState());
    }

    public override void Exit()
    {
        Debug.Log("Exiting GameEndedState");
        _gameEndController.onContinue -= HandleContinue;
    }

    public override void Update()
    {
    }
}