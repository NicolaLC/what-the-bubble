using System;

public class GameStateMachine : Singleton<GameStateMachine>
{
    private GameState activeGameState = new IdleGameState();

    private void Start()
    {
        activeGameState.Enter();
    }

    private void Update()
    {
        activeGameState.Update();
    }

    public static void NextState(GameState next)
    {
        instance.Internal_NextState(next);
    }

    private void Internal_NextState(GameState next)
    {
        if (next == activeGameState)
        {
            return;
        }
        activeGameState.Exit();
        activeGameState = next;
        activeGameState.Enter();
    }
}