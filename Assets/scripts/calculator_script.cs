using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorScript : MonoBehaviour
{
    [Header("Tower Info")]
    [SerializeField] private string tower = "Banana Farm";

    [Header("Paths")]
    [SerializeField] private int topPath = 0;
    [SerializeField] private int middlePath = 0;
    [SerializeField] private int bottomPath = 0;

    [Header("Economy")]
    [SerializeField] private float baseCost = 1250f;
    [SerializeField] private float upgradeCost = 0f;

    [SerializeField] private int bananas = 4;
    [SerializeField] private float bananaValue = 20f;

    [Header("Bonuses")]
    [SerializeField] private float monkeyKnowledgeBonus = 0.01f; // 1% value boost
    [SerializeField] private float sellBase = 0.70f;
    [SerializeField] private float sellMKBonus = 0.05f;

    [Header("Simulation")]
    [SerializeField] private float rounds = 1f;

    void Start()
    {
        Debug.Log("Tower: " + tower);
        Debug.Log("Crosspath: " + GetCrosspathString());

        Debug.Log("Income per round: " + GetIncomePerRound());
        Debug.Log("Total income: " + GetTotalIncome());
        Debug.Log("Net profit: " + GetNetProfit());

        Debug.Log("Sell multiplier: " + GetSellMultiplier());
        Debug.Log("Sell value: " + GetSellValue());
    }

    // -------------------------
    // CROSSPATH
    // -------------------------
    string GetCrosspathString()
    {
        return topPath + "-" + middlePath + "-" + bottomPath;
    }

    // -------------------------
    // VALUE MULTIPLIER
    // -------------------------
    float GetValueMultiplier()
    {
        float multiplier = 1f;

        // 0-2-0 gives +25%
        if (middlePath >= 2)
            multiplier += 0.25f;

        // Monkey Knowledge +1%
        multiplier += monkeyKnowledgeBonus;

        return multiplier;
    }

    // -------------------------
    // INCOME
    // -------------------------
    float GetIncomePerRound()
    {
        float value = Mathf.Ceil(bananaValue * GetValueMultiplier());
        return bananas * value;
    }

    float GetTotalIncome()
    {
        return Mathf.Ceil(GetIncomePerRound() * rounds);
    }

    float GetTotalCost()
    {
        return baseCost + upgradeCost;
    }

    float GetNetProfit()
    {
        return GetTotalIncome() - GetTotalCost();
    }

    // -------------------------
    // SELL VALUE
    // -------------------------
    float GetSellMultiplier()
    {
        float multiplier;

        // 0-0-2 sets base to 80%
        if (bottomPath >= 2)
            multiplier = 0.80f;
        else
            multiplier = sellBase;

        // Add MK bonus
        multiplier += sellMKBonus;

        // Cap at 95%
        return Mathf.Min(multiplier, 0.95f);
    }

    float GetSellValue()
    {
        return Mathf.Ceil(GetTotalCost() * GetSellMultiplier());
    }
}