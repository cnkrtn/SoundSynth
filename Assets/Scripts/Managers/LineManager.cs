using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class LineManager : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        private List<Vector2> occupiedNodes = new List<Vector2>(); // List to keep track of occupied nodes
        public List<CellScript> pointsList;
        public bool isSaw, isSquare, isSine;
        
        
        public void StartLine(Vector3 startPoint)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, startPoint);
        }

        public void UpdateLineEndPoint(Vector3 endPoint)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount-1, endPoint);
        }

        public void AddPoint(Vector3 point)
        {
            int pointIndex = lineRenderer.positionCount;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(pointIndex, point);
            
        }

        public void AddOccupiedNode(Vector2 node)
        {
            occupiedNodes.Add(node);
        }
        
        
        // public void ConvertSegmentToSine(int startIndex, int endIndex, int numberOfPoints)
        // {
        //     if (endIndex >= lineRenderer.positionCount || startIndex < 0 || startIndex >= endIndex)
        //     {
        //         Debug.LogError("Invalid segment indices for converting to sine.");
        //         Debug.Log("startIndex" + startIndex);
        //         Debug.Log("endIndex" + endIndex);
        //         return;
        //     }
        //
        //     // Calculate the amplitude and frequency based on the start and end points
        //     Vector3 startPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 2);//pointsList[pointsList.Count-2].transform.position;
        //     Vector3 endPoint = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        //     float amplitude = Mathf.Abs(startPoint.y - endPoint.y);
        //     float distance = Mathf.Abs(startPoint.x - endPoint.x);
        //     float frequency = Mathf.PI * 2f / (distance * 4f); // Assuming one quarter of a wave (2 * PI / (1/4))
        //
        //     // Calculate the time step between each added point
        //     float timeStep = 1f / (numberOfPoints + 1);
        //
        //     // Insert additional points for the sinusoidal shape between start and end points
        //     for (int i = 1; i <= numberOfPoints; i++)
        //     {
        //         float t = i * timeStep;
        //         float yOffset = amplitude * Mathf.Sin(frequency * t);
        //         Vector3 newPosition = new Vector3(Mathf.Lerp(startPoint.x, endPoint.x, t),
        //             startPoint.y + yOffset,
        //             0f);
        //         Debug.Log("startIndex" + startIndex);
        //         Debug.Log("endIndex" + endIndex);
        //         lineRenderer.positionCount += 1;
        //         lineRenderer.SetPosition(startIndex + i, newPosition);
        //     }
        //     lineRenderer.SetPosition(lineRenderer.positionCount -1,endPoint);
        //     Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     mousePosition.z = 0f;
        //     AddPoint(mousePosition);
        // }

    }
}