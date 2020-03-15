using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : Localizator
{
    public static IntEvent onScoreChange = new IntEvent();

    [SerializeField] private Text scoreDisplay = null;

    [SerializeField] private MergeRewardTable rewardTable = null;


    private int score = 0;

    protected override void Awake()
    {
        base.Awake();

        Summand.onIncrease.AddListener(IncreaseScore);
        ItemBuy.onPurchase.AddListener(PayForItem);
    }

    void Start()
    {
        score = DataManager.CurrentProgress.Score;
        DisplayText();
    }


    public void IncreaseScore(int rewardRank)
    {
        if (rewardTable == null) rewardTable = Resources.Load("Reward Table") as MergeRewardTable;
        rewardRank = Mathf.Clamp(rewardRank - 1, 0, rewardTable.Rewards.Length - 1);
        ChangeScore(rewardTable.Rewards[rewardRank]);
    }

    public void PayForItem(ShopItem item)
    {
        ChangeScore(-item.GetCurrentPrice(DataManager.CurrentProgress.BuyedItems[item.number - 1]));
    }

    private void ChangeScore(int count)
    {
        score += count;
        onScoreChange?.Invoke(score);
        DisplayText();
    }

    public override void DisplayText()
    {
        scoreDisplay.text = localizableText + ": " + score.ToString();
    }
}