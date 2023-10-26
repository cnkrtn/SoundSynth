
using UnityEngine;
using System.Collections;

public class Notescale : MonoBehaviour {
	
    float scale = Mathf.Pow (2f,1.0f/12f);
    AudioSource sound;
	

    void Start () {

        sound = GetComponent<AudioSource> ();
        StartCoroutine(PlayScale ());
    }

    IEnumerator PlayScale () {
	
        for (int i=0; i<72; i++) {

            sound.pitch = Mathf.Pow (scale,i);
            sound.Play ();
            yield return new WaitForSeconds(0.5f);
        }
    }

}
