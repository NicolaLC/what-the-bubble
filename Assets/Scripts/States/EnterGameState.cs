
using UnityEngine;
using UnityEngine.UI;

public class EnterGameState : GameState
{
    private Button playButton = null;
    public override void Enter()
    {
        Debug.Log("Entering EnterGameState");

        playButton = GameObject.Find("Button_StartPlay").GetComponent<Button>();
        playButton.interactable = true;

        playButton.onClick.AddListener(HandlePlayButtonClick);
    }

    public override void Exit()
    {
        Debug.Log("Exiting EnterGameState");
    }

    public override void Update()
    {
    }

    private void HandlePlayButtonClick()
    {
        GameStateMachine.NextState(new StartGameState());
    }
}