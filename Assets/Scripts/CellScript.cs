using UnityEngine;

public class CellScript : MonoBehaviour
{
    public int row;
    public int col;

    public void SetGridPosition(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return col;
    }
}