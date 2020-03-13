using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class GridField : MonoBehaviour
{
    public static Rect Bounds;

    public static Vector2 CellSize;

    [SerializeField] private Vector2 firstCellPos;
    [SerializeField] private Vector2 cellSize = new Vector2(1, -1);

    [SerializeField] private int cellCount = 4;

    private Cell[,] cells;
    public Cell[,] Cells => cells;

    private List<CellIndex> emptyCells = new List<CellIndex>();
    public bool HasEmptyCells => emptyCells.Count > 0;

    void Awake()
    {
        CellSize = cellSize;
        Bounds = new Rect(firstCellPos, cellSize * (cellCount - 1));
        Cell.onBecomeEmpty.AddListener(emptyCells.Add);

        cells = new Cell[cellCount, cellCount];
        for (int col = 0; col < cellCount; col++)
        {
            for (int raw = 0; raw < cellCount; raw++)
            {
                cells[col, raw] = new Cell()
                {
                    Index = (col, raw)
                };
                emptyCells.Add((col, raw));
                var center = firstCellPos + new Vector2(cellSize.x * col, cellSize.y * raw);
                // Debug.Log($"center of {col}, {raw} = {center}");
            }
        }
    }

    public void InsertToRandomEmptyCell(Summand summand)
    {
        if (!HasEmptyCells) throw new InvalidOperationException("There's no empty cells");
        var cell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
        cells[cell.col, cell.raw].AttachContent(summand);
        summand.AssignBehaviour(Replace);
        emptyCells.Remove(cell);
    }

    public CellIndex GetIndexFromPos(Vector2 pos)
    {
        var convPos = pos - firstCellPos;
        CellIndex index = (Mathf.RoundToInt(convPos.x / cellSize.x), Mathf.RoundToInt(convPos.y / cellSize.y));
        // Debug.Log("position: " + pos + "; conv pos: " + convPos + "; cell index: " + index);
        return index;
    }

    public void Replace(CellIndex from, Vector2 pos)
    {
        Replace(from, GetIndexFromPos(pos));
    }

    public void Replace(CellIndex from, CellIndex to)
    {
        if (cells[to.col, to.raw].MergeContentWith(cells[from.col, from.raw]))
        {
            emptyCells.Remove(to);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int col = 0; col < cellCount; col++)
        {
            for (int raw = 0; raw < cellCount; raw++)
            {
                Gizmos.DrawWireCube(firstCellPos + new Vector2(cellSize.x * col, cellSize.y * raw), cellSize);
            };
        }
    }
}