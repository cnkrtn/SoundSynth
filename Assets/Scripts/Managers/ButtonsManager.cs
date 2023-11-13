using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Managers
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] startFishObject;
        [SerializeField] private GameObject infoText, infoText2;
        [SerializeField] private GameObject selectButton, titleScreen;
        [SerializeField] private Sprite[] selectedSprites;
        [SerializeField] private Sprite[] originalSprites;
        [SerializeField] private Button[] buttons;
        [SerializeField] private float volume;
        public AudioManager audioManager;
        private LineManager _lineManager;
        public MovementManager movementManager;
        public GameObject phaseTwoManagers, phaseTwoUI;

        private void Start()
        {

            _lineManager = FindObjectOfType<LineManager>();

        }

        public void Pause(bool isPause)
        {
            Time.timeScale = isPause ? 0 : 1;
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
            StartCoroutine(DelayContinue());
            if (_lineManager.isSaw)
            {
                startFishObject[0].SetActive(true);
            }
            else if (_lineManager.isSine)
            {
                startFishObject[1].SetActive(true);
            }
            else
            {
                startFishObject[2].SetActive(true);
            }
        }

        private IEnumerator DelayContinue()
        {
            phaseTwoUI.SetActive(true);
            infoText.SetActive(true);
            infoText2.SetActive(true);
            SelectMovingObject();
            yield return new WaitForSeconds(3);
            infoText.SetActive(false);
            infoText2.SetActive(false);
            phaseTwoManagers.SetActive(true);
            MovementManager.Instance.GetWaypoints();
            titleScreen.SetActive(false);
            for (int i = 0; i < startFishObject.Length; i++)
            {
                startFishObject[i].SetActive(false);
            }
        }

        private void SelectMovingObject()
        {
            audioManager.audioSourceFish.volume = 0;
            audioManager.audioSourceDolphin.volume = 0;
            audioManager.audioSourceWhale.volume = 0;

            foreach (var movementManagerMovingObject in movementManager.movingObjects)
            {
                movementManagerMovingObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            }

            // Clear the selectedSounds list
            //  _audioManager.selectedSounds.Clear();

            if (_lineManager.isSaw)
            {
               movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = true;
                audioManager.audioSourceFish.volume = volume*0.5f;
                MovementManager.Instance.noteGroup = 0;

            }
            else if (_lineManager.isSine)
            {
                movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = true;
                audioManager.audioSourceDolphin.volume = volume;
                // _audioManager.selectedSounds.AddRange(_audioManager.dolphinSounds);
                MovementManager.Instance.noteGroup = 1;
            }
            else
            {
                movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = true;
                audioManager.audioSourceWhale.volume = volume;
                MovementManager.Instance.noteGroup = 2;
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
                    audioManager.audioSourceFish.volume = volume * .5f;
                    audioManager.audioSourceDolphin.volume = 0;
                    audioManager.audioSourceWhale.volume = 0;
                    movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case 1:
                    audioManager.audioSourceFish.volume = 0;
                    audioManager.audioSourceDolphin.volume = volume;
                    audioManager.audioSourceWhale.volume = 0;
                    movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    break;
                case 2:
                    audioManager.audioSourceFish.volume = 0;
                    audioManager.audioSourceDolphin.volume = 0;
                    audioManager.audioSourceWhale.volume = volume;
                    movementManager.movingObjects[0].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    movementManager.movingObjects[1].GetComponentInChildren<SpriteRenderer>().enabled = false;
                    movementManager.movingObjects[2].GetComponentInChildren<SpriteRenderer>().enabled = true;
                    break;

            }

        }
    }
}