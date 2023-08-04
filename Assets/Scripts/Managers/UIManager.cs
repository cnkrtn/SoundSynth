using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
   public class UIManager : MonoBehaviour
   {
      private LineManager _lineManager;
      private InputManager _inputManager;
      private AudioManager _audioManager;
      private MovementManager _movementManager;
      private MoveBackgrounds _moveBackgrounds;
      private MoveCave _moveCave;
      public GameObject restartButton, playButton;
      public Slider cutSlider, reverbSlider;

      private void OnEnable()
      {
         _moveBackgrounds = FindObjectOfType<MoveBackgrounds>();
         _moveCave = FindObjectOfType<MoveCave>();
         cutSlider.onValueChanged.AddListener(_moveBackgrounds.RotateBackgroundParent);
         cutSlider.onValueChanged.AddListener(_moveBackgrounds.ChangeTiling);
         cutSlider.onValueChanged.AddListener(_moveBackgrounds.MoveBackgroundPosition);
         reverbSlider.onValueChanged.AddListener((_moveCave.MoveObjectsPosition));
         _moveBackgrounds.MoveBackgroundPosition(cutSlider.value);
      }

      private void Start()
      {
         _lineManager = FindObjectOfType<LineManager>();
         _inputManager = FindObjectOfType<InputManager>();
         _audioManager = FindObjectOfType<AudioManager>();
         _movementManager = FindObjectOfType<MovementManager>();
       
      }

      private void Update()
      {
         if (_lineManager.pointsList.Count <= 1) return;
         restartButton.SetActive(true);

         foreach (var col in _lineManager.pointsList.Select(cellScript => cellScript.GetColumn()).Where(col => col == 15))
         {
            playButton.SetActive(true);
         }
         
         
      }

      public void PlayButton()
      {

         StartCoroutine(_audioManager.PlaySound());
         _lineManager.lineRenderer.positionCount -= 1;
         _movementManager.GetWaypoints();
         _movementManager.canMove = true;
         _inputManager.canInput = false;
      }

      public void RestartButton()
      {
         foreach (var cellScript in _lineManager.pointsList)
         {
            cellScript.isOccupied = false;
         }
         _lineManager.pointsList.Clear();
         for (int i = 0; i < _lineManager.lineRenderer.positionCount; i++)
         {
            _lineManager.lineRenderer.SetPosition(i,Vector2.zero);
            restartButton.SetActive(false);
            
         }
         
         
         _inputManager.isLineStarted = false;
         _movementManager.canMove = false;
         _inputManager.canInput = true;
         _movementManager.selectedMovingObject.SetActive(false);
         
      }
   }
}
