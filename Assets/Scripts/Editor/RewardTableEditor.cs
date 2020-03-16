using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//расширение редактора для более удобной настройки таблицы наград
//поддерживается автоподсчёт стоимости по вписанной формуле

[CustomEditor(typeof(MergeRewardTable))]
public class RewardTableEditor : Editor
{
    private int firstRewardValue = 50;
    private int tableSize = 10;

    public void OnEnable()
    {
        MergeRewardTable mrt = target as MergeRewardTable;
        firstRewardValue = mrt.Rewards[0];
        tableSize = mrt.Rewards.Length;
    }

    public override void OnInspectorGUI()
    {
        MergeRewardTable mrt = target as MergeRewardTable;
        firstRewardValue = EditorGUILayout.DelayedIntField("Start reward: ", firstRewardValue);
        if (firstRewardValue < 10)
        {
            firstRewardValue = 10;
        }
        tableSize = EditorGUILayout.DelayedIntField("Target table size:", tableSize);
        if (tableSize < 1) tableSize = 1;

        // GUILayout.Label("Current price progression:\n");
        if (GUILayout.Button("Calculate rewards"))
        {
            mrt.ResizeRewardTable(tableSize);
            for (int i = 1; i < tableSize; i++)
            {
                int prevReward = mrt.Rewards[i - 1];
                mrt.Rewards[i] = prevReward % 3 == 0 ? 4 * prevReward / 3 : 3 * prevReward / 2;
            }
        }

        DrawDefaultInspector();
    }
}
