using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip whaleSound; // The original whale sound clip
        public float pitchMultiplier = 1.0f; // Initial pitch multiplier

        public List<Vector2Int> noteTimings; // List of note timings (bar, note)
        private List<AudioSource> activeSources = new List<AudioSource>();

        // Array to hold the pitches for the E minor scale
        private float[] eMinorScalePitches = { 82.41f, 92.50f, 98.00f, 110.00f, 123.47f, 130.81f};

    
        private LineManager _lineManager;

        private void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            // PlayWhaleSound(noteTimings);
        }
        // Call this method to start playing the whale sound
        // public void PlayWhaleSound(List<Vector2Int> noteTimings)
        // {
        //     this.noteTimings = noteTimings;
        //     StartCoroutine(PlayNotes());
        // }

        // Coroutine to play the notes at the specified timings
        private IEnumerator PlayNotes()
        {
            float startTime = Time.time;

            // Clear previous active audio sources
            foreach (AudioSource source in activeSources)
            {
                Destroy(source.gameObject);
            }
            activeSources.Clear();

            // Play each note at its specified timing
            foreach (Vector2Int noteTiming in noteTimings)
            {
                int bar = noteTiming.x;
                int note = noteTiming.y;

                float targetPitch = CalculatePitchForNoteIndex(note);
                float pitch = targetPitch * pitchMultiplier;

                float duration = 1.0f; // Each note will play for 1 second (you can adjust this duration as needed)

                // Create a new GameObject and AudioSource for the current note
                GameObject noteObject = new GameObject("Note");
                noteObject.transform.SetParent(transform);
                AudioSource noteSource = noteObject.AddComponent<AudioSource>();
                activeSources.Add(noteSource);

                noteSource.clip = whaleSound;
                noteSource.pitch = pitch;
                noteSource.PlayScheduled(AudioSettings.dspTime + (bar * 1.0f)); // Schedule the playback at the specified time

                // Destroy the AudioSource after the duration to stop the note
                Destroy(noteObject, duration);
            }

            // Wait for all notes to finish playing
            yield return new WaitForSeconds(noteTimings[noteTimings.Count - 1].x + 1.0f - (Time.time - startTime));
        }

        // Calculate the target pitch based on the current note index
        private float CalculatePitchForNoteIndex(int noteIndex)
        {
            // Make sure the noteIndex is within the valid range
            noteIndex = Mathf.Clamp(noteIndex, 0, eMinorScalePitches.Length - 1);

            // Return the corresponding pitch for the given note index
            return eMinorScalePitches[noteIndex];
        }
    
        public void NoteTimingsCalculator()
        {
            for (var i = 0; i < _lineManager.pointsList.Count; i++)
            {
                Vector2Int time;
                time= Vector2Int.RoundToInt(_lineManager.pointsList[i].GetCell());
                
                noteTimings.Add(time);
            }
        }
    }
}
