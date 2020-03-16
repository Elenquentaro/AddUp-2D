[System.Serializable]
public struct SummandInfo
{
    public int value;
    public CellIndex position;

    public SummandInfo(int val, CellIndex pos)
    {
        value = val;
        position = pos;
    }

    public static implicit operator SummandInfo((int val, CellIndex pos) info)
    {
        return new SummandInfo(info.val, info.pos);
    }

    public static implicit operator SummandInfo(Summand summand)
    {
        return new SummandInfo(summand.Number, summand.ParentCellIndex);
    }
}