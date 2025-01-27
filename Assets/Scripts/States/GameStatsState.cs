//
using UnityEngine;

public class GameStatsState : GameState
{
    private GameStatsController _gameStatsController;
    public override void Enter()
    {
        Debug.Log("Entering GameStatsState");
        UIManager.SwitchToView(EView.GameStats);

        _gameStatsController = Object.FindAnyObjectByType<GameStatsController>();
        _gameStatsController.onRestart += HandleRestart;
    }

    public override void Exit()
    {
        Debug.Log("Exiting GameStatsState");

        _gameStatsController.onRestart -= HandleRestart;
    }

    public override void Update()
    {
    }

    private void HandleRestart()
    {
        GameStateMachine.NextState(new IdleGameState());
    }
}