using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriviaViewController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _questionText = null;
    [SerializeField]
    private TextMeshProUGUI _answer1Text = null;
    [SerializeField]
    private TextMeshProUGUI _answer2Text = null;
    [SerializeField]
    private TextMeshProUGUI _answer3Text = null;
    [SerializeField]
    private TextMeshProUGUI _answer4Text = null;
    [SerializeField]
    private Button _answer1Button = null;
    [SerializeField]
    private Button _answer2Button = null;
    [SerializeField]
    private Button _answer3Button = null;
    [SerializeField]
    private Button _answer4Button = null;
    [SerializeField]
    private TextMeshProUGUI _playerNumberText = null;
    [SerializeField]
    private TextMeshProUGUI _turnTotalTimeText = null;
    [SerializeField]
    private TextMeshProUGUI _gameTotalTimeText = null;

    [SerializeField]
    private RectTransform _bubbleRectTransform = null;
    [SerializeField]
    private Image _bubbleSprite = null;

    [SerializeField]
    private RectTransform _villainRectTransform = null;

    private TriviaQuestion _triviaQuestion = null;

    public Action<int> onAnswerSelected;

    private void OnEnable()
    {
        _playerNumberText.text = $"Player {GameplayManager.GetCurrentPlayer() + 1} turn";

        _answer1Button.image.color = Color.white;
        _answer2Button.image.color = Color.white;
        _answer3Button.image.color = Color.white;
        _answer4Button.image.color = Color.white;

        TimeManager.Instance.onTick += HandleTick;
        GameplayManager.Instance.onBubbleScaleChanged += SetBubbleScale;
    }

    private void OnDisable()
    {
        TimeManager.Instance.onTick -= HandleTick;
        GameplayManager.Instance.onBubbleScaleChanged -= SetBubbleScale;
    }

    public void SetQuestion(TriviaQuestion triviaQuestion)
    {
        _questionText.text = triviaQuestion.question;
        _answer1Text.text = triviaQuestion.answer1;
        _answer2Text.text = triviaQuestion.answer2;
        _answer3Text.text = triviaQuestion.answer3;
        _answer4Text.text = triviaQuestion.answer4;

        _triviaQuestion = triviaQuestion;
    }

    public void OnAnswerSelected(int choice)
    {
        onAnswerSelected?.Invoke(choice);

        bool bCorrect = _triviaQuestion.correctAnswerIndex == choice;

        if (bCorrect)
        {
            _villainRectTransform.DOShakeAnchorPos(2f, 10);
        }

        _answer1Button.image.color = _triviaQuestion.correctAnswerIndex == 0 ? Color.green : choice == 0 ? Color.yellow : Color.red;
        _answer2Button.image.color = _triviaQuestion.correctAnswerIndex == 1 ? Color.green : choice == 1 ? Color.yellow : Color.red;
        _answer3Button.image.color = _triviaQuestion.correctAnswerIndex == 2 ? Color.green : choice == 2 ? Color.yellow : Color.red;
        _answer4Button.image.color = _triviaQuestion.correctAnswerIndex == 3 ? Color.green : choice == 3 ? Color.yellow : Color.red;
    }

    private void HandleTick(float gameTime, float turnTime)
    {
        string gameTimeFormatted = ConvertToMinutesAndSeconds(gameTime);
        string turnTimeFormatted = ConvertToMinutesAndSeconds(turnTime);

        _gameTotalTimeText.text = gameTimeFormatted;
        _turnTotalTimeText.text = turnTimeFormatted;
    }

    private string ConvertToMinutesAndSeconds(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Calculate total minutes
        int seconds = Mathf.FloorToInt(time % 60); // Remaining seconds

        return $"{minutes:D2}:{seconds:D2}"; // Format as mm:ss
    }

    private void SetBubbleScale(float scale)
    {
        float progress = (scale - 0.5f) * 2f;

        _bubbleRectTransform.DOScale(new Vector3(scale, scale, 1), 0.25f).SetEase(Ease.InOutBounce);
        _bubbleRectTransform.DOShakePosition(1f, Mathf.Lerp(1, 10, progress)).SetLoops(-1);
        _bubbleSprite.color = Color.Lerp(Color.white, Color.red, progress);
    }
}