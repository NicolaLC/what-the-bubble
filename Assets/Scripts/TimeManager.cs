using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : Singleton<TimeManager>
{
    private float _totalGameTime = 0f;
    private float _turnGameTime = 0f;
    private bool _bGamePaused = true;

    public UnityAction<float, float> onTick;

    private void Update()
    {
        if (_bGamePaused)
        {
            return;
        }

        _totalGameTime += Time.deltaTime;
        _turnGameTime += Time.deltaTime;

        onTick?.Invoke(_totalGameTime, _turnGameTime);
    }

    public static void StartTurnTime()
    {
        instance.Internal_StartTurnTime();
    }

    public static void PauseTurnTime()
    {
        instance.Internal_PauseTurnTime();
    }

    private void Internal_StartTurnTime()
    {
        _bGamePaused = false;
    }

    private void Internal_PauseTurnTime()
    {
        _bGamePaused = true;
        _turnGameTime = 0f;
        _totalGameTime = (int)_totalGameTime; // round tick
    }

    public static float GetTurnTime()
    {
        return instance._turnGameTime;
    }

    public static float GetGameTime()
    {
        return instance._totalGameTime;
    }
}
