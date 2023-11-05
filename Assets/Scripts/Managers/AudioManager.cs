using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
       // public List<AudioClip> selectedSounds;
       // public List<AudioSource> selectedSources;
       public AudioSource musicSource;
       public AudioSource audioSourceFish,audioSourceWhale,audioSourceDolphin;
        public AudioReverbFilter reverbFilter;
        public AudioHighPassFilter highPassFilter;
        public AudioLowPassFilter lowPassFilter;
        private LineManager _lineManager;
        private MovementManager _movementManager;
        public AudioMixer audioMixer;
        [Range(0.1f, 10.0f)] public float timeBetweenWaypoints = 1.0f;
        
     [SerializeField] public float lowPassMin = 0f;
        [SerializeField] public float lowPassMax = 50f;
        [SerializeField] public float highPassMin = 50f;
        [SerializeField] public float highPassMax = 100f;
        [SerializeField] public float reverbMin = 0f;
        [SerializeField] public float reverbMax = 1f;

        private float pitchLerpDuration = 0.5f; // Adjust this value as needed for the pitch transition speed

        void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            _movementManager = FindObjectOfType<MovementManager>();
        }

        public IEnumerator PlaySound()
        {
            
            var cellList = _lineManager.pointsList;
            float[] rowOffsets = { 0, 1f, 2f, 4f, 5f, 7f }; // Offset values for each row

            while (true) // Outer loop to control playback cycles
            {
                int currentIndex = 0; // Track the current node index
                audioSourceFish.Play();
                audioSourceDolphin.Play();
                audioSourceWhale.Play();// Start playing the audio source before the loop

                while (currentIndex < cellList.Count - 1) // Check if currentIndex is not at the last node
                {
                    int currentRow = cellList[currentIndex].row;
                    int currentCol = cellList[currentIndex].col;

                    int nextIndex = (currentIndex + 1) % cellList.Count; // Use modulo to wrap around to the beginning of the list

                    int nextRow = cellList[nextIndex].row;
                    int nextCol = cellList[nextIndex].col;

                    // Calculate the pitch based on the offset of the current row
                    float startPitch = Mathf.Pow(2f, rowOffsets[currentRow] / 12f);

                    // Calculate the target pitch based on the offset of the next row
                    float targetPitch = Mathf.Pow(2f, rowOffsets[nextRow] / 12f);

                    // Calculate the differences in rows and columns
                    int rowDifference = Mathf.Abs(nextRow - currentRow);
                    int colDifference = Mathf.Abs(nextCol - currentCol);

                    // Use the larger difference between rows and columns to determine the duration
                    float duration = timeBetweenWaypoints * Mathf.Max(rowDifference, colDifference);

                    audioMixer.SetFloat("pitchShifter",startPitch);
                    yield return new WaitForSeconds(duration); // Wait for the calculated duration

                    audioMixer.SetFloat("pitchShifter",targetPitch); // Change the pitch to the target value

                    currentIndex++; // Move to the next node
                }
            }
        }




    }
}
