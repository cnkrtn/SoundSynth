using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MovementManager : MonoBehaviour
    {
        public List<GameObject> movingObjects;
        public GameObject selectedMovingObject;
        [SerializeField] private Transform startPosition;
        private LineManager _lineManager;
        private AudioManager _audioManager;

        [Range(0.1f, 10.0f)] public float timeBetweenWaypoints = 1.0f;
        public bool canMove = false;
        private Vector3[] linePositions;
        private int currentLineIndex = 0;
        private float t = 0f;

        void Start()
        {
            _lineManager = FindObjectOfType<LineManager>();
            _audioManager = FindObjectOfType<AudioManager>();
            GetWaypoints();
        }

        public void GetWaypoints()
        {
            linePositions = new Vector3[_lineManager.lineRenderer.positionCount];
            _lineManager.lineRenderer.GetPositions(linePositions);

            if (linePositions.Length > 0)
            {
                foreach (var movingObject in movingObjects)
                {

                    movingObject.SetActive(true);
                    //movingObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
                // selectedMovingObject.transform.position = linePositions[0];
                // selectedMovingObject.SetActive(true);
            }
        }

        void Update()
        {
            if (canMove)
                Move();
        }

        private void Move()
        {
            if (linePositions.Length > 1)
            {
                Vector2 currentTarget = linePositions[currentLineIndex];
                Vector2 nextTarget = linePositions[currentLineIndex + 1];

                int currentRow = _lineManager.pointsList[currentLineIndex].row;
                int currentCol = _lineManager.pointsList[currentLineIndex].col;
                int nextRow = _lineManager.pointsList[currentLineIndex + 1].row;
                int nextCol = _lineManager.pointsList[currentLineIndex + 1].col;

                // Calculate row and column differences
                int rowDifference = Mathf.Abs(nextRow - currentRow);
                int colDifference = Mathf.Abs(nextCol - currentCol);

                // Calculate time based on the square root formula
                float distance = Vector2.Distance(currentTarget, nextTarget);
                float time = timeBetweenWaypoints * Mathf.Max(rowDifference, colDifference);

                float speed = distance / time;

                t += Time.deltaTime * speed / distance;

                foreach (var movingObject in movingObjects)
                {
                    movingObject.transform.position = Vector2.Lerp(currentTarget, nextTarget, t);

                }
                // selectedMovingObject.transform.position = Vector2.Lerp(currentTarget, nextTarget, t);

                if (t >= 1.0f)
                {
                    t = 0f;
                    currentLineIndex++;

                    if (currentLineIndex >= linePositions.Length - 1)
                    {
                        currentLineIndex = 0;
                    }
                }
            }
        }
    }
}
