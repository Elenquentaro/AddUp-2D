using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell
{
    public static GenericEvent<CellIndex> onBecomeEmpty = new GenericEvent<CellIndex>();

    public CellIndex Index { get; set; }

    private Summand content = null;

    public bool IsEmpty => content == null;

    public int? ContentValue => content?.Number;

    public void AttachContent(Summand content)
    {
        this.content = content;
        this.content.AssignValue(Index);
    }

    public Summand ExtractContent()
    {
        Summand summand = content;
        content = null;
        onBecomeEmpty?.Invoke(Index);
        return summand;
    }

    public bool MergeContentWith(Cell other)
    {
        if (this == other) return false;
        if (IsEmpty)
        {
            AttachContent(other.ExtractContent());
            return true;
        }
        if (this.ContentValue == other.ContentValue)
        {
            content.IncreaseValue();
            UnityEngine.Object.Destroy(other.ExtractContent().gameObject);
            return true;
        }
        return false;
    }
}