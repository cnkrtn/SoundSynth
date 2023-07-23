using UnityEngine;

namespace Managers
{
     public class InputManager : MonoBehaviour
    {
        public LineManager lineManager;
        public CellScript selectedCell;
        public CellScript startCell;// Store the reference to the clicked cell
        private bool isLineStarted = false; // Flag to check if a line is currently being drawn
        private GridManager _gridManager;
        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            startCell = _gridManager.transform.GetChild(0).transform.GetComponent<CellScript>();
            lineManager.pointsList.Add(startCell);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleMouseClick();
            }
            else if (Input.GetMouseButton(0))
            {
                HandleMouseHold();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                HandleMouseRelease();
            }
        }

        private void HandleMouseClick()
        {
            
                if (!isLineStarted)
                {
                    selectedCell = startCell; // Store the reference to the clicked cell
                    lineManager.StartLine(selectedCell.transform.position);
                    isLineStarted = true;
            
                }
        }

        private void HandleMouseHold()
        {
            if (selectedCell != null && isLineStarted)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                // Check if the mouse is hovering over a node (occupied or unoccupied)
                if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f))
                {
                    RaycastHit2D snapHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (snapHit.collider != null && snapHit.collider.TryGetComponent(out CellScript snapCellScript) && snapHit.transform.position.x >= lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                    {

                        if (lineManager.isSaw)
                        {
                            if (snapHit.transform.position.y != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).y &&
                                snapHit.transform.position.x != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                                // Snap to the node if it's not occupied and add new points
                                if (!snapCellScript.isOccupied)
                                {
                                    // Update the last point to the snapped node position and add a new point
                                    lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                                    lineManager.pointsList.Add(snapCellScript);
                                    lineManager.AddPoint(mousePosition);
                                    lineManager.AddOccupiedNode(snapCellScript.GetCell());
                                    snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                                }
                        }else if (lineManager.isSquare)
                        {
                            if (snapHit.transform.position.y == lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).y || 
                                snapHit.transform.position.x == lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                                // Snap to the node if it's not occupied and add new points
                                if (!snapCellScript.isOccupied)
                                {
                                    // Update the last point to the snapped node position and add a new point
                                    lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                                    lineManager.pointsList.Add(snapCellScript);
                                    lineManager.AddPoint(mousePosition);
                                    lineManager.AddOccupiedNode(snapCellScript.GetCell());
                                    snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                                }
                        }else if (lineManager.isSine)
                        {
                            if (snapHit.transform.position.y != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).y &&
                                snapHit.transform.position.x != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                                // Snap to the node if it's not occupied and add new points
                                if (!snapCellScript.isOccupied)
                                {
                                    // Update the last point to the snapped node position and add a new point
                                  
                                    lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                                    lineManager.pointsList.Add(snapCellScript);
                                    
                                   
                                    lineManager.AddOccupiedNode(snapCellScript.GetCell());
                                    snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                                    lineManager.ConvertSegmentToSine(lineManager.lineRenderer.positionCount - 2,lineManager.lineRenderer.positionCount-1,10);
                                  //  lineManager.AddPoint(mousePosition);ineManager.lineRend
                                }
                        }
                       
                    }

                }
                else
                {
                    // If the mouse is not over any node, update the line endpoint to the current mouse position
                    lineManager.UpdateLineEndPoint(mousePosition);
                }
            }
        }

        private void HandleMouseRelease()
        {
            if (isLineStarted)
            {
                // Reset the line started flag when the mouse is released
                isLineStarted = false;
            }
        }

      
    }
}
