using System;
using Managers;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public int row;
    public int col;
    public Vector2 cellPosition;
    public bool isOccupied = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        OpenNota(row);
    }

    private void OpenNota(int i)
    { 
        MovementManager.Instance.notes[MovementManager.Instance.noteGroup].noteList[i].gameObject.SetActive(false);
        MovementManager.Instance.notes[MovementManager.Instance.noteGroup].noteList[i].gameObject.SetActive(true);
    }
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