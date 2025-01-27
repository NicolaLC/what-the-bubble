using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameEndController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _gameEndReasonText = null;
    [SerializeField]
    private TextMeshProUGUI _totalScoreText = null;

    public UnityAction onContinue;

    private void OnEnable()
    {
        _gameEndReasonText.text = "";
        _totalScoreText.text = $"Final Score:\n{ScoreManager.GetTotalScore()}";
    }

    public void SetGameEndReason(EGameEndedReason reason)
    {
        _gameEndReasonText.text = reason == EGameEndedReason.NoMoreQuestions ? "You completed all the questions!" : "Bubble exploded!";
    }

    public void GoToResults()
    {
        onContinue?.Invoke();
    }
}