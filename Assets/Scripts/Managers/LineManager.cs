using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class LineManager : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        private int lineRendererPointIndex = 0;
        private List<Vector2> occupiedNodes = new List<Vector2>(); // List to keep track of occupied nodes

        public void StartLine(Vector3 startPoint)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRendererPointIndex = 1;
        }

        public void UpdateLineEndPoint(Vector3 endPoint)
        {
            if (lineRendererPointIndex == 1)
            {
                lineRenderer.SetPosition(1, endPoint);
            }
            else
            {
                Vector3[] positions = new Vector3[lineRendererPointIndex + 2];
                for (int i = 0; i <= lineRendererPointIndex; i++)
                {
                    positions[i] = lineRenderer.GetPosition(i);
                }

                positions[lineRendererPointIndex + 1] = endPoint;

                lineRenderer.positionCount = lineRendererPointIndex + 2;
                lineRenderer.SetPositions(positions);
            }
        }

        public void ClearLine()
        {
            lineRenderer.positionCount = 0;
            lineRendererPointIndex = 0;
            occupiedNodes.Clear(); // Reset the list of occupied nodes
        }

        public bool HasLines()
        {
            return lineRenderer.positionCount > 1;
        }

        public void AddOccupiedNode(Vector2 node)
        {
            occupiedNodes.Add(node);
        }
    }
}