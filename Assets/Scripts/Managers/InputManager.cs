using UnityEngine;

namespace Managers
{
     public class InputManager : MonoBehaviour
    {
        public LineManager lineManager;
        public CellScript selectedCell;
        public CellScript startCell;// Store the reference to the clicked cell
        public bool isLineStarted = false, isLineFinished=false;
        public bool canInput = true;
        public bool canAddPoints;
        private GridManager _gridManager;
        private AudioManager _audioManager;
        public Camera cam;
        private void Start()
        {
            _gridManager = FindObjectOfType<GridManager>();
            _audioManager = FindObjectOfType<AudioManager>();
            // startCell = _gridManager.transform.GetChild(0).transform.GetComponent<CellScript>();
            // lineManager.pointsList.Add(startCell);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && canInput)
            {
                HandleMouseClick();
            }
            else if (Input.GetMouseButton(0) && canInput)
            {
                HandleMouseHold();
            }
            
        }
        
        // private void Update()
        // {
        //     if (Input.touchCount > 0 && canInput)
        //     {
        //         if (Input.GetTouch(0).phase == TouchPhase.Began)
        //         {
        //             HandleTouchBegin();
        //             
        //         }
        //         else if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //         {
        //             HandleTouchMove();
        //         }
        //         else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //         {
        //             HandleTouchEnd();
        //         }
        //     }
        // }

        private void HandleTouchBegin()
        {
            HandleMouseClick();
           // _audioManager.audioSource.PlayOneShot(_audioManager.selectedSounds[1]);
        }

        private void HandleTouchMove()
        {
            HandleMouseHold();
        }

        private void HandleTouchEnd()
        {
            // Your code for touch input ending (e.g., touch release)
        }


        private void HandleMouseClick()
        {
             RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
             if (hit.collider != null && hit.collider.TryGetComponent(out CellScript cellScript))
             {
                 if (!isLineStarted)
                 {
                     // selectedCell = startCell; // Store the reference to the clicked cell
                     // lineManager.StartLine(selectedCell.transform.position);
                     // isLineStarted = true;
                     // lineManager.AddOccupiedNode(selectedCell.GetCell());
                    
                     if (cellScript.GetColumn() == 0)
                     {
                         // Debug.Log("Starting new line from the node with CellScript.Vector2Int = (0, 0).");
                         selectedCell = cellScript; // Store the reference to the clicked cell
                         lineManager.StartLine(selectedCell.transform.position);
                         isLineStarted = true;
                        // lineManager.AddOccupiedNode(selectedCell.GetCell());
                         lineManager.pointsList.Add(selectedCell);
                     }
                     else
                     {
                         Debug.Log("Cannot start a new line from this node. Node is occupied.");
                     }
                 }
             }
        }
        
        public int colNumber;
        private void HandleMouseHold()
        {
            if (selectedCell != null && isLineStarted )
            {
                Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                // Check if the mouse is hovering over a node (occupied or unoccupied)
                if (Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.1f))
                {
                    RaycastHit2D snapHit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                    if (snapHit.collider != null && snapHit.collider.TryGetComponent(out CellScript snapCellScript) && snapHit.transform.position.x >= lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                    {

                        // if (lineManager.isSaw)
                        {
                            if (snapHit.transform.position.x != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                                // Snap to the node if it's not occupied and add new points
                                if (!snapCellScript.isOccupied)
                                {
                                    if (colNumber!=snapCellScript.col-1) return;
                                    colNumber = snapCellScript.col;
                                        // Update the last point to the snapped node position and add a new point
                                    lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                                    lineManager.pointsList.Add(snapCellScript);
                                    lineManager.AddPoint(mousePosition);
                                    lineManager.AddOccupiedNode(snapCellScript.GetCell());
                                    snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                                    isLineFinished = true;
                                }
                        }
                        //else if (lineManager.isSquare)
                        // {
                        //     if (snapHit.transform.position.y == lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).y || 
                        //         snapHit.transform.position.x == lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                        //         // Snap to the node if it's not occupied and add new points
                        //         if (!snapCellScript.isOccupied)
                        //         {
                        //             // Update the last point to the snapped node position and add a new point
                        //             lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                        //             lineManager.pointsList.Add(snapCellScript);
                        //             lineManager.AddPoint(mousePosition);
                        //             lineManager.AddOccupiedNode(snapCellScript.GetCell());
                        //             snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                        //         }
                        //  }
                        // else if (lineManager.isSine)
                        // {
                        //     if (snapHit.transform.position.y != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).y &&
                        //         snapHit.transform.position.x != lineManager.lineRenderer.GetPosition(lineManager.lineRenderer.positionCount - 2).x)
                        //         // Snap to the node if it's not occupied and add new points
                        //         if (!snapCellScript.isOccupied)
                        //         {
                        //             // Update the last point to the snapped node position and add a new point
                        //           
                        //             lineManager.UpdateLineEndPoint(snapCellScript.transform.position);
                        //             lineManager.pointsList.Add(snapCellScript);
                        //             
                        //            
                        //             lineManager.AddOccupiedNode(snapCellScript.GetCell());
                        //             snapCellScript.isOccupied = true; // Mark the snapped node as occupied
                        //             lineManager.ConvertSegmentToSine(lineManager.lineRenderer.positionCount - 2,lineManager.lineRenderer.positionCount-1,10);
                        //           
                        //         }
                        // }
                       
                    }

                }
                else
                {
                    foreach (var cell in lineManager.pointsList)
                    {
                        if (cell.col == 15)
                        {
                            return;
                        }
                          
                        else
                        {
                            // If the mouse is not over any node, update the line endpoint to the current mouse position
                            lineManager.UpdateLineEndPoint(mousePosition);
                        }
                    }

                    // // If the mouse is not over any node, update the line endpoint to the current mouse position
                    // lineManager.UpdateLineEndPoint(mousePosition);
                }
            }
        }

      

      
    }
}
