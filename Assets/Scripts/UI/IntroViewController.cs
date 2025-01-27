using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroViewController : MonoBehaviour
{
    [SerializeField]
    private List<Image> _checkedMarks = new List<Image>();

    private void OnEnable()
    {
        for (int i = 0; i < _checkedMarks.Count; ++i)
        {
            _checkedMarks[i].gameObject.SetActive(false);
        }
    }

    public void SelectPlayerNumber(int numberOfPlayers)
    {
        for (int i = 0; i < _checkedMarks.Count; ++i)
        {
            _checkedMarks[i].gameObject.SetActive(i == numberOfPlayers - 1);
        }
        GameplayManager.Instance.SelectPlayerNumber(numberOfPlayers);
    }
}