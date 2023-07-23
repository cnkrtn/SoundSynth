using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class LineManager : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        private List<Vector2> occupiedNodes = new List<Vector2>(); // List to keep track of occupied nodes

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

        public bool IsNodeOccupied(Vector2 node)
        {
            return occupiedNodes.Contains(node);
        }
    }
}