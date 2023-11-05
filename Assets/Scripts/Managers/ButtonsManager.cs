using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject selectButton, titleScreen;
        [SerializeField] private Sprite[] selectedSprites;
        [SerializeField] private Sprite[] originalSprites;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float volume;
        private AudioManager _audioManager;
        private LineManager _lineManager;
        private MovementManager _movementManager;
        public GameObject phaseTwoManagers, phaseTwoUI;

        private void Start()
        {

            _lineManager = FindObjectOfType<LineManager>();

        }

        public void SelectAvatar(int index)
        {
            for (var i = 0; i < buttons.Length; i++)
            {

                buttons[i].image.sprite = originalSprites[i];
            }

            buttons[index].image.sprite = selectedSprites[index];
            selectButton.SetActive(true);
        }

        public void ChangeGraphBool(int index)
        {
            switch (index)
            {
                case 0:
                    _lineManager.isSaw = true;
                    _lineManager.isSine = false;
                    _lineManager.isSquare = false;
                    break;
                case 1:
                    _lineManager.isSaw = false;
                    _lineManager.isSine = true;
                    _lineManager.isSquare = false;
                    break;
                case 2:
                    _lineManager.isSaw = false;
                    _lineManager.isSine = false;
                    _lineManager.isSquare = true;
                    break;
            }
        }

        // public void PlayMusic(AudioClip clip)
        // {
        //     if (clip != null)
        //     {
        //         _audioManager.PlaySound(clip);
        //     }
        //     
        // }
        public void Continue()
        {
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            phaseTwoManagers.SetActive(true);
            phaseTwoUI.SetActive(true);

            SelectMovingObject();
            titleScreen.SetActive(false);

        }

        private void SelectMovingObject()
        {
            _movementManager = FindObjectOfType<MovementManager>();
            _audioManager = FindObjectOfType<AudioManager>();

            _audioManager.audioSourceFish.volume = 0;
            _audioManager.audioSourceDolphin.volume = 0;
            _audioManager.audioSourceWhale.volume = 0;

            foreach (var movementManagerMovingObject in _movementManager.movingObjects)
            {
                movementManagerMovingObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }

            // Clear the selectedSounds list
            //  _audioManager.selectedSounds.Clear();

            if (_lineManager.isSaw)
            {
               _movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = true;
                _audioManager.audioSourceFish.volume = volume*0.5f;

            }
            else if (_lineManager.isSine)
            {
                _movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = true;
                _audioManager.audioSourceDolphin.volume = volume;
                // _audioManager.selectedSounds.AddRange(_audioManager.dolphinSounds);
            }
            else
            {
                _movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = true;
                _audioManager.audioSourceWhale.volume = volume;
                // _audioManager.selectedSounds.AddRange(_audioManager.whaleSounds);
            }

            // Now you have the selectedSounds list populated based on the boolean conditions
            // _movementManager.selectedMovingObject.SetActive(true);
        }


        public void LargeDisplayButtons(int index)
        {
            switch (index)
            {
                case 0:
                    _audioManager.audioSourceFish.volume = volume * .5f;
                    _audioManager.audioSourceDolphin.volume = 0;
                    _audioManager.audioSourceWhale.volume = 0;
                    _movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    _movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case 1:
                    _audioManager.audioSourceFish.volume = 0;
                    _audioManager.audioSourceDolphin.volume = volume;
                    _audioManager.audioSourceWhale.volume = 0;
                    _movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    _movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case 2:
                    _audioManager.audioSourceFish.volume = 0;
                    _audioManager.audioSourceDolphin.volume = 0;
                    _audioManager.audioSourceWhale.volume = volume;
                    _movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    _movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    break;

            }

        }
    }
}