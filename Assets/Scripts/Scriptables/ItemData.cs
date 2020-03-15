using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Resources/Create item data file")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ShopItem[] items = { new ShopItem() };
    public ShopItem[] Items => items;
}
