using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MoveBackgrounds : MonoBehaviour
{
    public List<Renderer> backgrounds;
    private UIManager _uiManager;
    // public float minRotationAngle = -5f;
    // public float maxRotationAngle = 5f;
    public float minScale = 0.6f;
    public float maxScale = 1.6f;
    public float minYPosition = 0.8f;
    public float maxYPosition = 7.3f;

    
    

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
       
        SetDefaults();
    }

    private void SetDefaults()
    {
        for (int index = 0; index < backgrounds.Count; index++)
        {
            var background = backgrounds[index];
            Vector2 textureOffset = new Vector2(1f, 0f);
            Vector2 textureScale = new Vector2(1f, 1f);

            background.material.SetTextureOffset("_MainTex", textureOffset);
            background.material.SetTextureScale("_MainTex", textureScale);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
        
    }

    private void MoveBackground()
    {
        for (var index = 0; index < backgrounds.Count; index++)
        {
            var background = backgrounds[index];
            var offset = Time.time * 0.01f * (index + 1f);
            var textureOffset = new Vector2(offset, 0);
            background.material.SetTextureOffset("_MainTex", textureOffset);
            
        }
    }

    // public void RotateBackgroundParent(float sliderValue)
    // {
    //     var targetRotation = Mathf.Lerp(minRotationAngle, maxRotationAngle, sliderValue);
    //     transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    //     
    //    
    // }
    
    private float MapSliderValueToYPosition(float sliderValue)
    {
        // Adjust the slider value to fit the desired Y-position range (0.7 to 7.3)
        // Midpoint (slider value 0.5) should be mapped to 2.5
        float minYPosition = 0.7f;
        float maxYPosition = 6.5f;
        float midYPosition = 2f;

        float mappedYPosition;
        if (sliderValue < 0.5f)
        {
            mappedYPosition = Mathf.Lerp(minYPosition, midYPosition, sliderValue * 2);
        }
        else
        {
            mappedYPosition = Mathf.Lerp(midYPosition, maxYPosition, (sliderValue - 0.5f) * 2);
        }

        return mappedYPosition;
    }
    
    public void MoveBackgroundPosition(float sliderValue)
    {
        // Clamp the slider value to [0, 1]
        sliderValue = Mathf.Clamp01(sliderValue);

        // Use the custom mapping function for the position
        var targetYPosition = MapSliderValueToYPosition(sliderValue);
        transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
    }

    public void ChangeTiling(float sliderValue)
    {
        // Calculate the scale based on the slider value
        float scale = Mathf.Lerp(minScale, maxScale, sliderValue);

        // Loop through the backgrounds and update their texture scale based on the slider value
        for (int index = 1; index < backgrounds.Count; index++)
        {
            var background = backgrounds[index];
            Vector2 textureScale = new Vector2(scale, 1); // Use the same scale for both X and Y axis
            background.material.SetTextureScale("_MainTex", textureScale);
        }
    }


}
