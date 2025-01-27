//
using UnityEngine;

public class TutorialState : GameState
{
    private TutorialViewController _tutorialViewController;
    public override void Enter()
    {
        Debug.Log("Entering TutorialState");
        UIManager.SwitchToView(EView.Tutorial);

        _tutorialViewController = Object.FindAnyObjectByType<TutorialViewController>();
        _tutorialViewController.onPlay += HandlePlay;
    }

    public override void Exit()
    {
        Debug.Log("Exiting TutorialState");

        _tutorialViewController.onPlay -= HandlePlay;
    }

    public override void Update()
    {
    }

    private void HandlePlay()
    {
        GameStateMachine.NextState(new StartGameState());
    }
}