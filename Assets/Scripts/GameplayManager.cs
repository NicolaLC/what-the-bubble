using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField]
    private List<TriviaQuestion> questions = new List<TriviaQuestion>();
    private List<TriviaQuestion> _currentQuestions = new List<TriviaQuestion>();
    private int _numberOfPlayers = 0;
    private int _currentPlayer = -1;
    private float _bubbleScale = 0.5f;

    public UnityAction<float> onBubbleScaleChanged;

    protected override void Awake()
    {
        base.Awake();

        _currentQuestions.AddRange(questions);
    }

    public static void ResetGame()
    {
        instance.Internal_ResetGame();
    }

    private void Internal_ResetGame()
    {
        _bubbleScale = 0.5f;
        _currentPlayer = -1;
        _numberOfPlayers = 0;
        _currentQuestions.Clear();
        _currentQuestions.AddRange(questions);
    }

    public void SelectPlayerNumber(int numberOfPlayers)
    {
        _numberOfPlayers = numberOfPlayers;
    }

    public static int GetNumberOfPlayers()
    {
        return instance._numberOfPlayers;
    }

    public static int GetCurrentPlayer()
    {
        return instance._currentPlayer;
    }

    public static void NextTurn()
    {
        instance._currentPlayer = (instance._currentPlayer + 1) % instance._numberOfPlayers;
    }

    public static TriviaQuestion GetRandomQuestion()
    {
        return instance.Internal_GetRandomQuestion();
    }

    private TriviaQuestion Internal_GetRandomQuestion()
    {
        if (_currentQuestions == null || _currentQuestions.Count == 0)
        {
            return null; // Or handle the case as needed
        }

        int index = UnityEngine.Random.Range(0, _currentQuestions.Count);
        TriviaQuestion question = _currentQuestions[index]; // Use the same index
        _currentQuestions.RemoveAt(index); // Remove the correct question

        return question;
    }

    public static void Apply10SecondsPenalty()
    {
        instance.Internal_Apply10SecondsPenalty();
    }

    private void Internal_Apply10SecondsPenalty()
    {
        _bubbleScale += 0.05f;
        onBubbleScaleChanged?.Invoke(_bubbleScale);
    }

    public static void ApplyWrongAnswerPenalty()
    {
        instance.Internal_ApplyWrongAnswerPenalty();
    }

    private void Internal_ApplyWrongAnswerPenalty()
    {
        _bubbleScale += 0.05f;
        onBubbleScaleChanged?.Invoke(_bubbleScale);
    }
}
