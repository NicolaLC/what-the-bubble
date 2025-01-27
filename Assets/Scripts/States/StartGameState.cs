
using TMPro;
using UnityEngine;

public class StartGameState : GameState
{
    private TextMeshProUGUI _counterText = null;
    private float _currentDelta = 0;
    private int _currentCount = 3;
    public override void Enter()
    {
        UIManager.SwitchToView(EView.GameStart);

        _counterText = GameObject.Find("Text_GameStart_Counter").GetComponent<TextMeshProUGUI>();
        _counterText.text = "3";
    }

    public override void Exit()
    {
        Debug.Log("Exiting StartGameState");
    }

    public override void Update()
    {
        // Accumulate delta time
        _currentDelta += Time.deltaTime;

        // Check if a second has passed
        if (_currentDelta >= 1f)
        {
            _currentCount--; // Decrease the counter
            _currentDelta = 0f; // Reset delta time
        }

        if (_currentCount <= 0)
        {
            GameStateMachine.NextState(new NextPlayerTurnState());
            return;
        }

        // Update the counter text
        _counterText.text = $"{_currentCount}";
    }
}