using System;

[Serializable]
public struct StringPair : IEquatable<StringPair>
{
    public string Key;
    public string Value;

    public StringPair(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public bool Equals(StringPair other)
    {
        return this.Key.Equals(other.Key);
    }

    public static implicit operator StringPair((string key, string value) pair)
    {
        return new StringPair(pair.key, pair.value);
    }
}