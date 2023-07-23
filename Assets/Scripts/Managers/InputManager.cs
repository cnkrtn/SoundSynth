using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public LineManager lineManager;
        public CellScript selectedCell; // Store the reference to the clicked cell
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
                    if (cellScript.GetCell() == Vector2.zero && !lineManager.IsNodeOccupied(cellScript.GetCell()))
                    {
                        Debug.Log("Starting new line from the node with CellScript.Vector2Int = (0, 0).");
                        selectedCell = cellScript; // Store the reference to the clicked cell
                        lineManager.StartLine(selectedCell.transform.position);
                        isLineStarted = true;
                        lineManager.AddOccupiedNode(selectedCell.GetCell());
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

                // Check if the mouse is hovering over a node (occupied or unoccupied)
                if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f))
                {
                    RaycastHit2D snapHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (snapHit.collider != null && snapHit.collider.TryGetComponent(out CellScript snapCellScript))
                    {
                        // Snap to the node if it's not occupied and add new points
                        if (!snapCellScript.isOccupied)
                        {
                            // Update the last point to the snapped node position and add a new point
                            lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                            lineManager.AddPoint(mousePosition);
                            lineManager.AddOccupiedNode(snapCellScript.GetCell());
                            snapCellScript.isOccupied = true; // Mark the snapped node as occupied
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
            // Reset the line started flag when the mouse is released
            isLineStarted = false;
        }
    }
}
