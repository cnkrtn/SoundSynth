using UnityEngine;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        public int rows = 6;
        public int columns = 16;
        public float cellWidth = .5f; // Separate width and height variables for non-square cells
        public float cellHeight = .5f;
        public GameObject cellPrefab;
        private Transform gridObject;
   

        private GameObject[,] grid;

        private void Awake()
        {
           
             CreateGrid();
             

        }

        public void CreateGrid()
        {
            grid = new GameObject[rows, columns];

            Vector2 bottomLeft = new Vector2(transform.position.x - columns * cellWidth * 0.5f + cellWidth * 0.5f,
                transform.position.y - rows * cellHeight * 0.5f + cellHeight * 0.5f);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector2 cellPosition = new Vector2(bottomLeft.x + col * cellWidth, bottomLeft.y + row * cellHeight);
                    GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
                    cell.name = $"Cell ({row}, {col})";

                    // Store grid position in the cell's script
                    CellScript cellScript = cell.GetComponent<CellScript>();
                    if (cellScript != null)
                    {
                        cellScript.SetGridPosition(row, col);
                    }

                    grid[row, col] = cell;
                  
                }
            }

            transform.localScale = Vector3.one * .9f;

        }
    }
}
