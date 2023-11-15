using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public CellScript cellScript;
    private void OnTriggerEnter2D(Collider2D col)
    {
       cellScript.OpenNota(cellScript.row);
    }
}
