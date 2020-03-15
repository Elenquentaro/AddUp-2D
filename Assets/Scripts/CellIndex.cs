using System;
using UnityEngine;

[Serializable]
public struct CellIndex
{
    public int col;
    public int raw;

    public CellIndex(int _col, int _raw)
    {
        col = _col;
        raw = _raw;
    }

    public Vector2 GetPosition(Vector2 upperLeftCorner, Vector2 size)
    {
        var pos = upperLeftCorner + new Vector2(size.x * col, size.y * raw);
        return pos;
    }

    public void BoundToGrid(int columns, int raws)
    {
        col = Mathf.Clamp(col, 0, columns - 1);
        raw = Mathf.Clamp(raw, 0, raws - 1);
    }

    public static implicit operator (int, int)(CellIndex ci)
    {
        return (ci.col, ci.raw);
    }

    public static implicit operator CellIndex((int col, int raw) index)
    {
        return new CellIndex() { col = index.col, raw = index.raw };
    }

    public override string ToString()
    {
        return $"column = {col}, raw = {raw}";
    }
}