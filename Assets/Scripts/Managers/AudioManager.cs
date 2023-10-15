using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> selectedSounds;
        public List<AudioClip> fishSounds, whaleSounds, dolphinSounds;
        public List<AudioSource> selectedSources;
        public AudioSource musicSource;
        private LineManager _lineManager;
        private MovementManager _movementManager;
        public AudioMixer audioMixer;
        
        public AudioReverbFilter reverbFilter;
        public AudioHighPassFilter highPassFilter;
        public AudioLowPassFilter lowPassFilter;
        
        
        [SerializeField]public float lowPassMin = 0f; // Minimum value for the LowPass filter
        [SerializeField]public float lowPassMax = 50f; // Maximum value for the LowPass filter
        [SerializeField]public float highPassMin = 50f; // Minimum value for the HighPass filter
        [SerializeField]public float highPassMax = 100f; // Maximum value for the HighPass filter
        [SerializeField]public float reverbMin = 0f; // Minimum value for the Reverb filter
        [SerializeField]public float reverbMax = 1f; // Maximum value for the Reverb filter

        
        // public string echoDelay = "EchoDelay"; 
        // public string lowCutoffFreq = "LowCutoffFreq"; 
        // public string highCutoffFreq = "HighCutoffFreq"; 
        private void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            _movementManager = FindObjectOfType<MovementManager>();

        }

        public IEnumerator PlaySound()
        {
            var cellList = _lineManager.pointsList;

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
                        var nextCol = cellList[nextIndex].col;
                        var nextRow = cellList[nextIndex].row;
                        var differenceCol = Mathf.Abs(nextCol - currentCol);
                        var differenceRow = Mathf.Abs(nextRow - currentRow);

                        if (differenceCol > differenceRow)
                        {
                            for (int j = 0; j < differenceCol; j++)
                            {
                                selectedSources[currentRow].PlayOneShot(selectedSounds[currentRow]);
                                yield return new WaitForSeconds(1.5f);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < differenceRow; j++)
                            {
                                selectedSources[currentRow].PlayOneShot(selectedSounds[currentRow]);
                                yield return new WaitForSeconds(1.5f);
                            }
                        }
                    }

                 

                  
                }
            }
        }







        private static void GetConsecutiveColumns(int i, List<CellScript> cellList, int column, List<CellScript> consecutiveCells)
        {
            // Check the cells after the current cell
            for (var j = i + 1; j < cellList.Count; j++)
            {
                var nextCell = cellList[j].GetComponent<CellScript>();

                // If the column of the next cell matches the current cell's column, add it to the consecutive list
                if (nextCell.col == column)
                {
                    consecutiveCells.Add(nextCell);
                }
                else
                {
                    break; // Stop checking consecutive cells as soon as we find a different column
                }
            }
        }
        
        // public void SetEchoLevel(float level)
        // {
        //     audioMixer.SetFloat(echoDelay, level);
        // }
        //
        // public void SetLowFilterLevel(float level)
        // {
        //     audioMixer.SetFloat(lowCutoffFreq, level);
        // } 
        // public void SetHighFilterLevel(float level)
        // {
        // //       private float MapSliderValueToYPosition(float sliderValue)
        // {
        //     // Adjust the slider value to fit the desired Y-position range (0.7 to 7.3)
        //     // Midpoint (slider value 0.5) should be mapped to 2.5
        //     float minYPosition = 0.7f;
        //     float maxYPosition = 6.5f;
        //     float midYPosition = 2f;
        //
        //     float mappedYPosition;
        //     if (sliderValue < 0.5f)
        //     {
        //         mappedYPosition = Mathf.Lerp(minYPosition, midYPosition, sliderValue * 2);
        //     }
        //     else
        //     {
        //         mappedYPosition = Mathf.Lerp(midYPosition, maxYPosition, (sliderValue - 0.5f) * 2);
        //     }
        //
        //     return mappedYPosition;
        // }
        // // }
    }
}
