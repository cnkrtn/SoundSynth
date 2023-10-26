using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
       // public List<AudioClip> selectedSounds;
       // public List<AudioSource> selectedSources;
        public AudioSource musicSource,audioSource;
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

            float[] rowOffsets = { -4, -2f, 0f, 2f, 4f, 5f }; // Offset values for each row

            audioSource.Play(); // Start playing the audio source before the loop

            while (true)
            {
                for (int i = 0; i < cellList.Count; i++)
                {
                    int currentRow = cellList[i].row;
                    int currentCol = cellList[i].col;

                    var nextIndex = i + 1;

                    if (nextIndex >= cellList.Count)
                    {
                        nextIndex = 0;
                        i = -1;
                    }
                    else
                    {
                        var nextRow = cellList[nextIndex].row;

                        // Calculate the pitch based on the offset of the current row
                        float startPitch = Mathf.Pow(2f, rowOffsets[currentRow] / 12f);

                        var differenceCol = Mathf.Abs(cellList[nextIndex].col - currentCol);
                        var differenceRow = Mathf.Abs(nextRow - currentRow);

                        // Calculate the target pitch based on the offset of the next row
                        float targetPitch = Mathf.Pow(2f, rowOffsets[nextRow] / 12f);

                        float duration = timeBetweenWaypoints * Mathf.Sqrt(differenceCol * differenceCol + differenceRow * differenceRow);
                        float startTime = Time.time;

                        while (Time.time < startTime + duration)
                        {
                            float t = (Time.time - startTime) / duration;
                            audioSource.pitch = Mathf.Lerp(startPitch, targetPitch, t);
                            yield return null;
                        }

                        audioSource.pitch = targetPitch;
                    }
                }
            }
        }



    }
}
