using UnityEngine;

//структура, предоставляющая более удобное отслеживание расположения предмета в заданных рамках, чем чистый Rect

[System.Serializable]
public struct Borders
{
    public float left;
    public float right;
    public float upper;
    public float lower;

    private Rect baseRect;
    public Rect BaseRect => baseRect;

    public Borders(Rect bordersRect, Vector2 threshold)
    {
        baseRect = bordersRect;
        left = Mathf.Min(bordersRect.xMax, bordersRect.xMin) - Mathf.Abs(threshold.x);
        right = Mathf.Max(bordersRect.xMax, bordersRect.xMin) + Mathf.Abs(threshold.x);
        upper = Mathf.Max(bordersRect.yMax, bordersRect.yMin) + Mathf.Abs(threshold.y);
        lower = Mathf.Min(bordersRect.yMax, bordersRect.yMin) - Mathf.Abs(threshold.y);
    }
}