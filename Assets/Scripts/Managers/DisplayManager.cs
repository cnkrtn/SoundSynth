using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    private void Start()
    {
        for (var i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

}
