﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemBuy : Localizator
{
    public static GenericEvent<ShopItem> onPurchase = new GenericEvent<ShopItem>();

    public static EmptyEvent onCannotPurchase = new EmptyEvent();

    [SerializeField] private Text itemNameGUI = null;

    private ShopItem item;

    private int purchaseNumber = 0;

    private Button button = null;

    public void Init(ShopItem item)
    {
        this.item = item;
        button = GetComponent<Button>();
        CheckPurchaseAbility(DataManager.CurrentProgress.Score);
        ScoreCounter.onScoreChange.AddListener(CheckPurchaseAbility);

        LocalizeText(LocalizationManager.CurrentLocalization ?? new Localization());
        purchaseNumber = DataManager.CurrentProgress.BuyedItems[item.number - 1];
        DisplayText();
    }

    public override void DisplayText()
    {
        textGUI.text = localizableText + ": " + CalculatePrice();
        itemNameGUI.text = item.number.ToString();
    }

    public int CalculatePrice()
    {
        return item.GetCurrentPrice(purchaseNumber);
    }

    //вызывается нажатием на кнопку
    public void Buy()
    {
        if (!SummandSpawner.HasEmptyCells)
        {
            onCannotPurchase?.Invoke();
            return;
        }
        purchaseNumber++;
        onPurchase?.Invoke(item);
        DisplayText();
    }

    public void CheckPurchaseAbility(int currentScore)
    {
        button.interactable = currentScore >= CalculatePrice();
    }
}