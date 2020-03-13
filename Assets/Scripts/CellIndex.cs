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
        // Debug.Log("index: " + this + "; corner: " + upperLeftCorner + "; size: " + size + "\npos: " + pos);
        return pos;
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