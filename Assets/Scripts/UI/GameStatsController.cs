
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameStatsController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _fastestText = null;
    [SerializeField]
    private TextMeshProUGUI _moreAccurateText = null;
    [SerializeField]
    private TextMeshProUGUI _slowestText = null;
    [SerializeField]
    private TextMeshProUGUI _lessAccurateText = null;

    public UnityAction onRestart;

    private void OnEnable()
    {
        GameScoreResult gameScoreResult = ScoreManager.GetGameScore();
        _fastestText.text = $"Player {gameScoreResult.fastestPlayer + 1}";
        _slowestText.text = $"Player {gameScoreResult.slowestPlayer + 1}";
        _moreAccurateText.text = $"Player {gameScoreResult.mostAccuratePlayer + 1}";
        _lessAccurateText.text = $"Player {gameScoreResult.lessAccuratePlayer + 1}";
    }

    public void RestartGame()
    {
        onRestart?.Invoke();
    }
}