using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
   public class UIManager : MonoBehaviour
   {
      [SerializeField] private GameObject finishImage1, finishImage2;
      [SerializeField] private Transform playerPosition;
      private LineManager _lineManager;
      private InputManager _inputManager;
      private AudioManager _audioManager;
      private MovementManager _movementManager;
      private MoveBackgrounds _moveBackgrounds;
      private MoveCave _moveCave;
      public Button restartButton, playButton;
      public Slider cutSlider, reverbSlider;

      private Coroutine _playRoutine;
      private void OnEnable()
      {
         _moveBackgrounds = FindObjectOfType<MoveBackgrounds>();
         _moveCave = FindObjectOfType<MoveCave>();
         _audioManager = FindObjectOfType<AudioManager>();

         // cutSlider.onValueChanged.AddListener(_moveBackgrounds.RotateBackgroundParent);
         cutSlider.onValueChanged.AddListener(_moveBackgrounds.ChangeTiling);
         cutSlider.onValueChanged.AddListener(_moveBackgrounds.MoveBackgroundPosition);
         cutSlider.onValueChanged.AddListener(UpdateCutFiltersHigh);
         cutSlider.onValueChanged.AddListener(UpdateCutFiltersLow);
         reverbSlider.onValueChanged.AddListener(UpdateReverbFilter);
         reverbSlider.onValueChanged.AddListener((_moveCave.MoveObjectsPosition));
         // UpdateCutFilters(cutSlider.value);
         // UpdateReverbFilter(reverbSlider.value);
         _moveBackgrounds.MoveBackgroundPosition(cutSlider.value);
      }

      private void Start()
      {
         _lineManager = FindObjectOfType<LineManager>();
         _inputManager = FindObjectOfType<InputManager>();

         _movementManager = FindObjectOfType<MovementManager>();

      }

      private void Update()
      {
         if (_lineManager.pointsList.Count <= 1) return;
         restartButton.interactable=true;

         foreach (var col in _lineManager.pointsList.Select(cellScript => cellScript.GetColumn())
                     .Where(col => col == 15))
         {
            playButton.interactable=true;
            _inputManager.canInput = false;
         }


      }

      public void PlayButton()
      {

         _playRoutine = StartCoroutine(_audioManager.PlaySound());
         _audioManager.musicSource.Stop();
         _audioManager.musicSource.Play();
         _lineManager.lineRenderer.positionCount -= 1;
         _movementManager.GetWaypoints();
         _movementManager.canMove = true;
         _inputManager.canInput = false;
         playButton.GetComponent<Button>().interactable = false;
      }

      public void RestartButton()
      {
         foreach (var movementManagerMovingObject in _movementManager.movingObjects)
         {

            movementManagerMovingObject.transform.position = playerPosition.position;
         }
        
         
         _audioManager.audioSourceDolphin.Stop();
         _audioManager.audioSourceFish.Stop();
         _audioManager.audioSourceWhale.Stop();
         if (_playRoutine != null)
         {
            StopCoroutine(_playRoutine);
         }
        
         foreach (var cellScript in _lineManager.pointsList)
         {
            cellScript.isOccupied = false;
         }

         _lineManager.pointsList.Clear();
         for (int i = 0; i < _lineManager.lineRenderer.positionCount; i++)
         {
            _lineManager.lineRenderer.SetPosition(i, Vector2.zero);
            restartButton.interactable=false;

         }


         _inputManager.isLineStarted = false;
         _movementManager.canMove = false;
         _inputManager.canInput = true;
         _movementManager.selectedMovingObject.SetActive(false);
         
         playButton.interactable=false;
      }


      private void UpdateCutFiltersHigh(float sliderValue)
      {
         if (!(sliderValue <= .5f)) return;
         // Calculate HighPass filter value based on slider position (0-50)
         float highPassValue = Mathf.Lerp(1000f, 10f, sliderValue / .5f);

         // Set HighPass filter value
         _audioManager.highPassFilter.cutoffFrequency = highPassValue;

      }
      
      private void UpdateCutFiltersLow(float sliderValue)
      {
         if (!(sliderValue >= .5f)) return;
         // Calculate LowPass filter value based on slider position (50-100)
         float lowPassValue = Mathf.Lerp(22000f, 2000, (sliderValue - .5f) / .5f);

         // Set LowPass filter value
         _audioManager.lowPassFilter.cutoffFrequency = lowPassValue;

      }
      

      // Method to update the Reverb filter
      private void UpdateReverbFilter(float sliderValue)
      {
         // Calculate reverb filter value based on slider position
         float reverbValue = Mathf.Lerp(-2000f, 200f,
            (sliderValue - reverbSlider.minValue) / (reverbSlider.maxValue - reverbSlider.minValue));

         // Set reverb filter value
         _audioManager.reverbFilter.reverbLevel = reverbValue;
      }

      public void Finish()
      {
         //TODO 
         finishImage1.SetActive(true);
         finishImage2.SetActive(true);
         _lineManager.gameObject.SetActive(false);
         for (int i = 0; i < _movementManager.movingObjects.Count; i++)
         {
            _movementManager.movingObjects[i].gameObject.SetActive(false);
         }

         _audioManager.audioSourceDolphin.enabled = false;
         _audioManager.audioSourceFish.enabled = false;
         _audioManager.audioSourceWhale.enabled = false;
         SceneLoad();
         transform.parent.gameObject.SetActive(false);
      }

      private async void SceneLoad()
      {
         await Task.Delay(4000);
         SceneManager.LoadScene(0);
      }
   }
}
