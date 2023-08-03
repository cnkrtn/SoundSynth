using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCave : MonoBehaviour
{
    public GameObject caveTop, caveBottom;
    public float minObject1YPosition, maxObject1YPosition;
    public float minObject2YPosition, maxObject2YPosition;


    public void MoveObjectsPosition(float sliderValue)
    {


        // Calculate the target position for object1
        var targetYPosition1 = Mathf.Lerp(minObject1YPosition, maxObject1YPosition, sliderValue);
        caveTop.transform.position = new Vector2(caveTop.transform.position.x, targetYPosition1);

        // Calculate the target position for object2
        var targetYPosition2 = Mathf.Lerp(minObject2YPosition, maxObject2YPosition, sliderValue);
        caveBottom.transform.position = new Vector2(caveBottom.transform.position.x, targetYPosition2);
    }
}
