using UnityEngine;
using UnityEngine.Events;

public class TutorialViewController : MonoBehaviour
{
    public UnityAction onPlay;

    public void Play()
    {
        onPlay?.Invoke();
    }
}