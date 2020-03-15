using System;

[Serializable]
public class ShopItem : IEquatable<ShopItem>
{
    public int number = 1;

    public bool Equals(ShopItem other)
    {
        return this.number.Equals(other.number);
    }

    public int GetCurrentPrice(int purchaseNumber)
    {
        return number * 100 * (int)Math.Pow(purchaseNumber + 1, 2);
    }
}
