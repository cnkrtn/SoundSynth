using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public LineManager lineManager;
        public CellScript selectedCell; // Store the reference to the clicked cell
        public bool isMouseOnNode = false; // Flag to check if the mouse is on a node
        private bool isLineStarted = false; // Flag to check if a line is currently being drawn

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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out CellScript cellScript))
            {
                if (!isLineStarted)
                {
                    if (cellScript.GetCell() == Vector2.zero)
                    {
                        Debug.Log("Starting new line from the node with CellScript.Vector2Int = (0, 0).");
                        selectedCell = cellScript; // Store the reference to the clicked cell
                        lineManager.StartLine(selectedCell.transform.position);
                        isLineStarted = true;
                    }
                    else if (!cellScript.isOccupied)
                    {
                        Debug.Log("Starting new line from the node with CellScript.Vector2Int = " + cellScript.GetCell());
                        selectedCell = cellScript; // Store the reference to the clicked cell
                        lineManager.StartLine(selectedCell.transform.position);
                        isLineStarted = true;
                    }
                    else
                    {
                        Debug.Log("Cannot start a new line from this node. Node is occupied.");
                    }
                }
            }
        }

        private void HandleMouseHold()
        {
            if (selectedCell != null && isLineStarted)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                if (isMouseOnNode && !selectedCell.isOccupied)
                {

                    // Snapped to a node, update the last position of the line to the snapped node's position
                    lineManager.UpdateLineEndPoint(selectedCell.transform.position);
                    selectedCell.isOccupied = true;
                    lineManager.AddOccupiedNode(selectedCell.GetCell());

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
                else if (!isMouseOnNode)
                {
                    // Add a new position for the current mouse position
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

        private void OnDrawGizmos()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            isMouseOnNode = hit.collider != null && hit.collider.TryGetComponent(out CellScript cellScript);
        }
    }
}
