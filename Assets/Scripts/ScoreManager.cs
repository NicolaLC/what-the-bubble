using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct PlayerScore
{
    public int score;
    public float bestTurnTime;
    public float totalGameTime;
    public int correctAnswers;
}

[Serializable]
public struct GameScoreResult
{
    public int fastestPlayer;
    public int slowestPlayer;
    public int mostAccuratePlayer;
    public int lessAccuratePlayer;
}

public class ScoreManager : Singleton<ScoreManager>
{
    private int _totalScore;
    private List<PlayerScore> _playerScores = new List<PlayerScore>();

    private readonly PlayerScore defaultScore = new PlayerScore
    {
        score = 0,
        bestTurnTime = float.PositiveInfinity,
        totalGameTime = float.PositiveInfinity,
        correctAnswers = 0
    };

    private int timedScore = 6;
    private int correctAnswerScore = 10;

    protected override void Awake()
    {
        base.Awake();

        for (var i = 0; i < 4; ++i)
        {
            _playerScores.Add(defaultScore);
        }
    }

    public static void CalculateEndTurnScore(bool bAnswerCorrect)
    {
        instance.Internal_CalculateScore(bAnswerCorrect);
    }

    private void Internal_CalculateScore(bool bAnswerCorrect)
    {
        int currentPlayerIndex = GameplayManager.GetCurrentPlayer();
        int turnScore = 0;
        PlayerScore playerScore = _playerScores[currentPlayerIndex];

        float turnTime = TimeManager.GetTurnTime();
        turnScore += Math.Max(0, timedScore - ((int)turnTime / 10));

        if (bAnswerCorrect)
        {
            turnScore += correctAnswerScore;
            playerScore.correctAnswers++;
        }

        _totalScore += turnScore;

        playerScore.score += turnScore;
        playerScore.totalGameTime += turnScore;

        if (playerScore.bestTurnTime == float.PositiveInfinity || turnTime < playerScore.bestTurnTime)
        {
            playerScore.bestTurnTime = turnTime;
        }
    }

    public static int GetTotalScore()
    {
        return instance._totalScore;
    }

    public static List<PlayerScore> GetPlayersScore()
    {
        return instance._playerScores;
    }

    public static PlayerScore GetPlayerScore(int playerIndex)
    {
        return instance._playerScores[playerIndex];
    }

    public static GameScoreResult GetGameScore()
    {
        return instance.Internal_GetGameScore();
    }

    public static int GetAccuratePlayerIndex()
    {
        return instance.Internal_GetAccuratePlayer();
    }

    private GameScoreResult Internal_GetGameScore()
    {
        GameScoreResult gameScoreResult = new GameScoreResult
        {
            lessAccuratePlayer = 0,
            mostAccuratePlayer = 0,
            fastestPlayer = 0,
            slowestPlayer = 0,
        };

        float bestTurnTime = _playerScores[0].bestTurnTime;
        float worstTurnTime = _playerScores[0].bestTurnTime;
        int bestCorrectAnswersCount = 0;
        int worstCorrectAnswersCount = 0;

        for (var i = 0; i < _playerScores.Count; ++i)
        {
            if (_playerScores[i].bestTurnTime < bestTurnTime)
            {
                bestTurnTime = _playerScores[i].bestTurnTime;
                gameScoreResult.fastestPlayer = i;
            }

            if (_playerScores[i].bestTurnTime > worstTurnTime)
            {
                worstTurnTime = _playerScores[i].bestTurnTime;
                gameScoreResult.slowestPlayer = i;
            }

            if (_playerScores[i].correctAnswers > bestCorrectAnswersCount)
            {
                bestCorrectAnswersCount = _playerScores[i].correctAnswers;
                gameScoreResult.mostAccuratePlayer = i;
            }

            if (_playerScores[i].correctAnswers < worstCorrectAnswersCount)
            {
                worstCorrectAnswersCount = _playerScores[i].correctAnswers;
                gameScoreResult.lessAccuratePlayer = i;
            }
        }

        return gameScoreResult;
    }

    private int Internal_GetAccuratePlayer()
    {
        int fastestPlayerIndex = 0;
        float bestTurnTime = _playerScores[0].bestTurnTime;

        for (var i = 0; i < _playerScores.Count; ++i)
        {
            if (_playerScores[i].bestTurnTime < bestTurnTime)
            {
                bestTurnTime = _playerScores[i].bestTurnTime;
                fastestPlayerIndex = i;
            }
        }

        return fastestPlayerIndex;
    }
}
