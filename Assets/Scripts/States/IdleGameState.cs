
using UnityEngine;
using UnityEngine.UI;

public class IdleGameState : GameState
{
    private Button playButton = null;

    public override void Enter()
    {
        Debug.Log("Entering IdleGameState");

        GameplayManager.ResetGame();
        UIManager.SwitchToView(EView.Intro);
        AudioManager.PlayBackgroundMusic(EAudioClip.MenuMusic);

        playButton = GameObject.Find("Button_StartPlay").GetComponent<Button>();
        playButton.interactable = false;
    }

    public override void Exit()
    {
        Debug.Log("Exiting IdleGameState");
    }

    public override void Update()
    {
        if (GameplayManager.GetNumberOfPlayers() != 0)
        {
            GameStateMachine.NextState(new EnterGameState());
        }
    }
}