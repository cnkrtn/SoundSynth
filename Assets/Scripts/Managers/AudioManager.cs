using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource1;

        public void PlaySound(AudioClip clip)
        {
            audioSource1.PlayOneShot(clip);
        }
    
        public void StopSound()
        {
            audioSource1.Stop();
        }
        
    }
}