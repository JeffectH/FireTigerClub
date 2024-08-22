
    using System;
    using System.IO;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    public class UISelectLevelManager:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bestScore;
        [Space] [SerializeField] private Button _muteSound;
        [Space] [SerializeField] private Button _muteMusic;

        private const string SoundSpritePath = "Sprites/Setting";
        private const string SoundOffSprite = "OffSound";
        private const string SoundOnSprite = "OnSound";

        private void Start()
        {
            _bestScore.text = SaveManager.LoadBestScore().ToString();
            UpdateSoundButton();
        }

        private void OnEnable()
        {
            _muteMusic.onClick.AddListener(MuteMusic);
            _muteSound.onClick.AddListener(MuteSound);
        }

        private void OnDisable()
        {
            _muteMusic.onClick.RemoveListener(MuteMusic);
            _muteSound.onClick.RemoveListener(MuteSound);
        }

        private void MuteSound()
        {
            SoundManagerSelectLevel.Instance.MuteSound();
        UpdateSoundButton();
        }
    
        private void MuteMusic()
        {
            SoundManagerSelectLevel.Instance.MuteMusic();
        UpdateSoundButton();
        }

        private void UpdateSoundButton()
        {
            if (SoundManagerSelectLevel.Instance.IsMuteSound)
            {
                _muteSound.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOffSprite));
            }
            else
            {
                _muteSound.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOnSprite));
            }
            
            if (SoundManagerSelectLevel.Instance.IsMuteMusic)
            {
                _muteMusic.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOffSprite));
            }
            else
            {
                _muteMusic.GetComponent<Image>().sprite =
                    Resources.Load<Sprite>(Path.Combine(SoundSpritePath, SoundOnSprite));
            }
            
        }
    }
