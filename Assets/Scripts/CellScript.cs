using UnityEngine;

public class CellScript : MonoBehaviour
{
    public int row;
    public int col;
    public Vector2 cellPosition;
    public bool isOccupied = false;

    public void SetGridPosition(int row, int col)
    {
        this.row = row;
        this.col = col;

        // Update the cellPosition whenever SetGridPosition is called
        cellPosition = new Vector2(row, col);
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return col;
    }

    public Vector2 GetCell()
    {
        return cellPosition;
    }
}