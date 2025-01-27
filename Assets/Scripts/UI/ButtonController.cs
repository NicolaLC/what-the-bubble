using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        AudioManager.PlayVFX(EAudioClip.ButtonClick);
    }
}