using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject selectButton;
        [SerializeField] private GameObject[] selectedBackgrounds;

        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }

        public void SelectAvatar(GameObject selectedBackground)
        {
            selectButton.SetActive(true);
            foreach (var background in selectedBackgrounds)
            {
                background.SetActive(false);
            }
            selectedBackground.SetActive(true);
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip != null)
            {
                _audioManager.PlaySound(clip);
            }
            
        }
        public void Devam()
        {
            selectButton.SetActive(false);
            foreach (var background in selectedBackgrounds)
            {
                background.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
