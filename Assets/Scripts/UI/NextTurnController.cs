using TMPro;
using UnityEngine;

public class NextTurnController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerNumberText = null;

    private void OnEnable()
    {
        _playerNumberText.text = $"Player {GameplayManager.GetCurrentPlayer() + 1} turn";
    }
}