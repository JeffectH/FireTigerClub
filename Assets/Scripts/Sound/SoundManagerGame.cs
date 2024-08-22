using System;
using UnityEngine;

namespace Sound
{
    public class SoundManagerGame : MonoBehaviour, ISoundSontroller
    {
        public static SoundManagerGame Instance { get; private set; }

        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _buttonClick;
        [SerializeField] private AudioSource _fireShot;
        [SerializeField] private AudioSource _burningFlashLight;
        [SerializeField] private AudioSource _winGame;
        [SerializeField] private AudioSource _loseGame;
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
                _music.Play();
            }
        }

        public void MuteSound()
        {
            IsMuteSound = !IsMuteSound;

            _buttonClick.mute = IsMuteSound;
            _burningFlashLight.mute = IsMuteSound;
            _fireShot.mute = IsMuteSound;
            _winGame.mute = IsMuteSound;
            _loseGame.mute = IsMuteSound;
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

        public void PlaySoundWinGame()
            => _winGame.Play();

        public void PlaySoundSlotLoseGame()
            => _loseGame.Play();

        public void PlaySoundBurningFlashLight()
            => _burningFlashLight.Play();

        public void PlaySoundFireShot()
            => _fireShot.Play();
        
        
        private void OnApplicationQuit()
        {
            SaveManager.SaveSoundMuteState(false);
            SaveManager.SaveMusicMuteState(false);
        
        }
    }
    
    
    
}