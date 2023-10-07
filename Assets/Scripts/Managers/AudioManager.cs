using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> selectedSounds; 
        public List<AudioSource> selectedSources; 
        private LineManager _lineManager;
        private MovementManager _movementManager;

        private void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            _movementManager = FindObjectOfType<MovementManager>();

        }

        public IEnumerator PlaySound()
        {
            var cellList = _lineManager.pointsList;
            List<CellScript> consecutiveCells = new List<CellScript>();
            while (true)
            {


                for (var i = 0; i < cellList.Count; i++)
                {
                    var cell = cellList[i].GetComponent<CellScript>();
                    var column = cell.col;
                    var row = cell.row;

                    consecutiveCells.Clear();
                    consecutiveCells.Add(cell);

                    GetConsecutiveColumns(i, cellList, column, consecutiveCells);

                    if (consecutiveCells.Count > 1)
                    {
                        foreach (var cellScript in consecutiveCells)
                        {
                            selectedSources[cellScript.row].PlayOneShot(selectedSounds[cellScript.row]);
                        }

                        i += consecutiveCells.Count - 1;
                    }
                    else
                    {
                        selectedSources[cell.row].PlayOneShot(selectedSounds[cell.row]);
                    }

                    yield return new WaitForSeconds(_movementManager.audioTime);
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
    }
}
