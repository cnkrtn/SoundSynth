using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MovementManager : MonoBehaviour
    {
        public List<GameObject> movingObjects;
        public GameObject selectedMovingObject;
        private LineManager _lineManager;
        public float speed = 2.0f;
        public bool canMove=false;
        private Vector3[] linePositions;
        private int currentLineIndex = 0;
        private float t = 0f;

        void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            // Get the positions of the points from the LineRenderer
           // GetWaypoints();
        }

        public void GetWaypoints()
        {
            linePositions = new Vector3[_lineManager.lineRenderer.positionCount];
            _lineManager.lineRenderer.GetPositions(linePositions);

            // Make sure there are points in the line
            if (linePositions.Length > 0)
            {
                selectedMovingObject.transform.position =
                    linePositions[0]; // Set the GameObject to the start position of the line
                selectedMovingObject.SetActive(true);
            }
        }

        void Update()
        {
            if(canMove)
                Move();
        }

        private void Move()
        {
            // Make the GameObject move along the line
            if (linePositions.Length > 1)
            {
                Vector2 currentTarget = linePositions[currentLineIndex];
                Vector2 nextTarget = linePositions[currentLineIndex + 1];

                t += Time.deltaTime * speed / Vector2.Distance(currentTarget, nextTarget);

                selectedMovingObject.transform.position = Vector2.Lerp(currentTarget, nextTarget, t);

                // If the GameObject reaches the next point, move to the next segment of the line
                if (t >= 1.0f)
                {
                    t = 0f;
                    currentLineIndex++;

                    // Loop back to the start of the line when reaching the end
                    if (currentLineIndex >= linePositions.Length - 1)
                    {
                        currentLineIndex = 0;
                    }
                }
            }
        }
    }
}