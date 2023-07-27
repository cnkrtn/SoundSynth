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
        private LineManager _lineManager;
        private MovementManager _movementManager;
        public GameObject phaseTwoManagers,phaseTwoUI;

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            _lineManager = FindObjectOfType<LineManager>();
            


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
            selectButton.SetActive(false);
            foreach (var background in selectedBackgrounds)
            {
                background.transform.parent.gameObject.SetActive(false);
            }
            phaseTwoManagers.SetActive(true);
            phaseTwoUI.SetActive(true);

            SelectMovingObject();
        }

        private void SelectMovingObject()
        {
            _movementManager = FindObjectOfType<MovementManager>();
            foreach (var movementManagerMovingObject in _movementManager.movingObjects)
            {
                movementManagerMovingObject.SetActive(false);
            }
            if(_lineManager.isSaw)
            {
                _movementManager.selectedMovingObject = _movementManager.movingObjects[0];
            }
            else if(_lineManager.isSine){_movementManager.selectedMovingObject = _movementManager.movingObjects[1];}
            else{_movementManager.selectedMovingObject = _movementManager.movingObjects[2];}
            _movementManager.selectedMovingObject.SetActive(true);
        }
    }
}
