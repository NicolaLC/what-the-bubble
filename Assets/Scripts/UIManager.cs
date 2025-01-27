using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[Serializable]
public enum EView
{
    Intro,
    GameStart,
    NextPlayerTurn,
    Trivia,
    GameEnd,
    GameStats,
    Tutorial
}

[Serializable]
public class ViewMapping
{
    public EView viewType;
    public GameObject viewObject;
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private List<ViewMapping> viewMappings = new List<ViewMapping>();
    private Dictionary<EView, GameObject> views = new Dictionary<EView, GameObject>();

    private EView activeView = EView.Intro;

    protected override void Awake()
    {
        base.Awake();

        DOTween.Init();

        // Convert the list to a dictionary at runtime
        foreach (var mapping in viewMappings)
        {
            mapping.viewObject.SetActive(false);
            if (!views.ContainsKey(mapping.viewType))
            {
                views.Add(mapping.viewType, mapping.viewObject);
            }
            else
            {
                Debug.LogWarning($"Duplicate viewType {mapping.viewType} found. Skipping.");
            }
        }

    }

    public static void SwitchToView(EView nextView)
    {
        instance.Internal_SwitchToView(nextView);
    }

    private void Internal_SwitchToView(EView nextView)
    {
        if (views.TryGetValue(nextView, out GameObject nextViewGameObject))
        {
            if (views.TryGetValue(activeView, out GameObject currentViewGameObject))
            {
                currentViewGameObject.SetActive(false);
            }

            nextViewGameObject.SetActive(true);
            activeView = nextView;
        }
        else
        {
            Debug.LogWarning($"View {nextView} not found in the dictionary!");
        }
    }
}
