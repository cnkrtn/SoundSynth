using System;
using System.Linq;
using UnityEngine;

namespace Managers
{
   public class UIManager : MonoBehaviour
   {
      private LineManager _lineManager;
      private InputManager _inputManager;
      private AudioManager _audioManager;
      private MovementManager _movementManager;
      public GameObject restartButton, playButton;

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
         _audioManager.NoteTimingsCalculator();
         //_audioManager.PlayWhaleSound(_audioManager.noteTimings);
         _lineManager.lineRenderer.positionCount -= 1;
         _movementManager.GetWaypoints();
         _movementManager.canMove = true;
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
      }
   }
}
