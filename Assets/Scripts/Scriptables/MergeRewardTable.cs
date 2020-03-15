using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward Table", menuName = "Resources/Create merge reward table")]
public class MergeRewardTable : ScriptableObject
{
    [SerializeField] private int[] rewards = { 50, 75, 100, 150, 200, 300 };
    public int[] Rewards => rewards;

    public void ResizeRewardTable(int newSize)
    {
        int difference = newSize - rewards.Length;
        int lastValue = rewards[rewards.Length - 1];
        Array.Resize<int>(ref rewards, newSize);
        if (difference > 0)
        {
            for (int i = rewards.Length - difference; i < rewards.Length; i++)
            {
                rewards[i] = lastValue;
            }
        }
    }
}
