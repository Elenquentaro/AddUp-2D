using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class GridField : MonoBehaviour
{
    public static Borders Borders;

    public static Vector2 CellSize;

    [SerializeField] private Vector2 firstCellPos = new Vector2(-1.5f, 1.5f);
    [SerializeField] private Vector2 cellSize = new Vector2(1, -1);

    [SerializeField] private int gridSize = 4;

    private Cell[,] cells;
    public Cell[,] Cells => cells;

    private List<CellIndex> emptyCells = new List<CellIndex>();
    public bool HasEmptyCells => emptyCells.Count > 0;
    public bool HalfFilled => emptyCells.Count <= (gridSize * gridSize) / 2;

    private int maxSummandAtField = 1;
    public int MaxSummandAtField => maxSummandAtField;


    void Awake()
    {
        CellSize = cellSize;
        Borders = new Borders(new Rect(firstCellPos, cellSize * (gridSize - 1)), cellSize / 2);

        Cell.onBecomeEmpty.AddListener(emptyCells.Add);
        Summand.onIncrease.AddListener((num) =>
        {
            if (maxSummandAtField <= num)
                maxSummandAtField = num + 1;
        });

        cells = new Cell[gridSize, gridSize];
        for (int col = 0; col < gridSize; col++)
        {
            for (int raw = 0; raw < gridSize; raw++)
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

    public void InsertToRandomEmptyCell(Summand summand, int summandValue = 1)
    {
        if (!HasEmptyCells) throw new InvalidOperationException("There's no empty cells");
        var cell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
        cells[cell.col, cell.raw].AttachContent(summand);
        summand.AssignBehaviour(Replace);
        emptyCells.Remove(cell);
        summand.AssignValue((cell.col, cell.raw), summandValue);
        if (summandValue > maxSummandAtField) maxSummandAtField = summandValue;
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
        if (pos.y < Borders.lower || pos.y > Borders.upper)
        {
            Debug.Log("Out of bounds!");
            if (Trash.IsSelected)
            {
                Destroy(cells[from.col, from.raw].ExtractContent().gameObject);
            }
        }
        else
        {
            Replace(from, GetIndexFromPos(pos));
        }
    }

    public void Replace(CellIndex from, CellIndex to)
    {
        to.BoundToGrid(cells.GetLength(0), cells.GetLength(1));
        if (cells[to.col, to.raw].MergeContentWith(cells[from.col, from.raw]))
        {
            emptyCells.Remove(to);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int col = 0; col < gridSize; col++)
        {
            for (int raw = 0; raw < gridSize; raw++)
            {
                // Handles.Label(firstCellPos + new Vector2(cellSize.x * col, cellSize.y * raw), $"col {col}, raw {raw}");
                Gizmos.DrawWireCube(firstCellPos + new Vector2(cellSize.x * col, cellSize.y * raw), cellSize);
            };
        }
    }
}