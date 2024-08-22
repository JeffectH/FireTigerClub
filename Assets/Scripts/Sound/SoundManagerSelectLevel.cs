using System;
using Sound;
using UnityEngine;

public class SoundManagerSelectLevel : MonoBehaviour, ISoundSontroller
{
    public static SoundManagerSelectLevel Instance { get; private set; }

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _buttonClick;

    public bool IsMuteSound { get; set; }

    public bool IsMuteMusic { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (SaveManager.LoadSoundMuteState())
            MuteSound();

        if (SaveManager.LoadMusicMuteState())
        {
            MuteMusic();
        }
        else
        {
            _music.Play();
        }
    }

    public void MuteSound()
    {
        IsMuteSound = !IsMuteSound;

        _buttonClick.mute = IsMuteSound;

        SaveManager.SaveSoundMuteState(IsMuteSound);
    }

    public void MuteMusic()
    {
        IsMuteMusic = !IsMuteMusic;

        _music.mute = IsMuteMusic;

        SaveManager.SaveMusicMuteState(IsMuteMusic);
    }


    private void OnApplicationQuit()
    {
        SaveManager.SaveSoundMuteState(false);
        SaveManager.SaveMusicMuteState(false);
        
    }
}