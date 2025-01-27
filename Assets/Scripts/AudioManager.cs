using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum EAudioClip
{
    None,
    MenuMusic,
    GameMusic,
    ButtonClick,
    CorrectAnswer,
    WrongAnswer,
    GameCompleted,
    BubbleExploded,
}

[Serializable]
public struct AudioClipSource
{
    public EAudioClip clipType;
    public AudioClip clip;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private List<AudioClipSource> sources = new List<AudioClipSource>();

    [SerializeField]
    private AudioSource bgMusicSource = null;

    [SerializeField]
    private AudioSource vfxMusicSource = null;

    private EAudioClip currentBGMusic;

    public static void PlayBackgroundMusic(EAudioClip clipToPlay)
    {
        instance.Internal_PlayBackgroundMusic(clipToPlay);
    }

    private void Internal_PlayBackgroundMusic(EAudioClip clipToPlay)
    {
        if (currentBGMusic == clipToPlay)
        {
            return;
        }

        try
        {
            AudioClipSource source = FindAudioClipByType(clipToPlay);

            if (bgMusicSource.isPlaying)
            {
                bgMusicSource.Stop();
            }

            bgMusicSource.clip = source.clip;
            bgMusicSource.Play();

            currentBGMusic = clipToPlay;
        }
        catch (Exception)
        {

        }
    }

    private AudioClipSource FindAudioClipByType(EAudioClip clipToPlay)
    {
        return sources.Find(c => c.clipType == clipToPlay);
    }

    public static void PlayVFX(EAudioClip clipToPlay)
    {
        instance.Internal_PlayVFX(clipToPlay);
    }

    private void Internal_PlayVFX(EAudioClip clipToPlay)
    {
        try
        {
            AudioClipSource source = FindAudioClipByType(clipToPlay);

            if (vfxMusicSource.isPlaying)
            {
                vfxMusicSource.Stop();
            }

            vfxMusicSource.PlayOneShot(source.clip);
        }
        catch (Exception)
        {

        }
    }

    public static void SetBackgroundMusicPitch(float pitch)
    {
        instance.bgMusicSource.pitch = pitch;
    }
}
