using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Grid
{
    [SerializeField] private Cell[,] cells = new Cell[4, 4];
    public Cell[,] Cells => cells;
}
