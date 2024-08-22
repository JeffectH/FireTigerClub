using Sound;
using UnityEngine;

public class SoundManagerMenu : MonoBehaviour, ISoundSontroller
{
    public static SoundManagerMenu Instance { get; private set; }

    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _buttonClick;
    [SerializeField] private AudioSource _slotMachine;
    [SerializeField] private AudioSource _slotMachineWin;

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
            if (SaveManager.LoadFirstRun())
                _music.Play();
        }
    }

    public void MuteSound()
    {
        IsMuteSound = !IsMuteSound;

        _buttonClick.mute = IsMuteSound;
        _slotMachine.mute = IsMuteSound;
        _slotMachineWin.mute = IsMuteSound;

        SaveManager.SaveSoundMuteState(IsMuteSound);
    }

    public void MuteMusic()
    {
        IsMuteMusic = !IsMuteMusic;

        _music.mute = IsMuteMusic;

        SaveManager.SaveMusicMuteState(IsMuteMusic);
    }

    public void PlaySoundSlotMachine()
        => _slotMachine.Play();

    public void PlaySoundSlotMachineWin()
        => _slotMachineWin.Play();

    private void OnApplicationQuit()
    {
        SaveManager.SaveSoundMuteState(false);
        SaveManager.SaveMusicMuteState(false);
    }
}