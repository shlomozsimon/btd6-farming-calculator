using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorScript : MonoBehaviour
{
    //definding tower type
    [Header("Tower Info")]
    [SerializeField] private string tower = "Banana Farm";

    //definding cross path
    [Header("Paths")]
    [SerializeField] private int topPath = 0;
    [SerializeField] private int middlePath = 0;
    [SerializeField] private int bottomPath = 0;

    //dealing with toatal tower cost
    [Header("Economy")]
    [SerializeField] private float baseCost = 1250f;
    [SerializeField] private float upgradeCost = 0f;

    //banana farm specfifc veriables
    [SerializeField] private int bananas = 4;
    [SerializeField] private float bananaValue = 20f;

    //dealign will sell value
    [Header("Bonuses")]
    [SerializeField] private float monkeyKnowledgeBonus = 0.01f; // 1% value boost
    [SerializeField] private float sellBase = 0.70f;
    [SerializeField] private float sellMKBonus = 0.05f;

    //curent round we are on
    [Header("Simulation")]
    [SerializeField] private float rounds = 1f;
    //farm related factors
    struct FarmBonus{
        //this counts the banas produced in the round over the base so +2 for 1-0-0 or +4 for 2-0-0
        public int extraBananas;
        //the extra value of bananas such as a 0-2-0 wich give $20 + 26% (rounded up)
        public float valueMultiplier;
        //set sell value to 80% for bottom path
        public float sellOverride; // -1 = no override
    }

    void Start(){
        Debug.Log("Tower: " + tower);
        Debug.Log("Crosspath: " + GetCrosspathString());

        Debug.Log("Income per round: " + GetIncomePerRound());
        Debug.Log("Total income: " + GetTotalIncome());
        Debug.Log("Net profit: " + GetNetProfit());

        Debug.Log("Sell multiplier: " + GetSellMultiplier());
        Debug.Log("Sell value: " + GetSellValue());
    }
    //all the top path benfits
    FarmBonus GetTopPathBonus(){
        //definding farmBonus then assining extra bananas produdeced accourding to upgrade path
        FarmBonus bonus = new FarmBonus();

        if (topPath >= 1)
            bonus.extraBananas += 2;

        if (topPath >= 2)
            bonus.extraBananas += 2; // adjust if needed

        return bonus;
    }
    //middle path benfits
    FarmBonus GetMiddlePathBonus(){
        FarmBonus bonus = new FarmBonus();

        if (middlePath >= 2){
            bonus.valueMultiplier += 0.25f;
        }

        return bonus;
    }
    //bottom path benfits
    FarmBonus GetBottomPathBonus(){
        FarmBonus bonus = new FarmBonus();

        if (bottomPath >= 2)
            bonus.sellOverride = 0.80f;

        return bonus;
    }
    //caculating the benfits of each cross path (extra bananas per top path and extra bana value for middle path) then returns the value of benefits of our current upgrade e.g a 2-2-0 will return +4 bananas and +26% for bananas value
    FarmBonus CombineBonuses(){
        //defining the benfit of each crosspath
        FarmBonus total = new FarmBonus();
        total.extraBananas = 0;
        total.valueMultiplier = 1f;
        total.sellOverride = -1f;

        // Enforce BTD6 rule: max 2 paths
        int activePaths = 0;
        if (topPath > 0) activePaths++;
        if (middlePath > 0) activePaths++;
        if (bottomPath > 0) activePaths++;
        if (activePaths > 2){
            Debug.LogError("Invalid crosspath: more than 2 paths used.");
            return total;
        }

        FarmBonus top = GetTopPathBonus();
        FarmBonus mid = GetMiddlePathBonus();
        FarmBonus bot = GetBottomPathBonus();

        // Combine bananas
        total.extraBananas += top.extraBananas;
        total.extraBananas += mid.extraBananas;
        total.extraBananas += bot.extraBananas;

        // Combine multipliers
        total.valueMultiplier += top.valueMultiplier;
        total.valueMultiplier += mid.valueMultiplier;
        total.valueMultiplier += bot.valueMultiplier;

        // Handle sell override (priority rule)
        if (bot.sellOverride > 0){
            total.sellOverride = bot.sellOverride;
        }
        return total;
    }

    //defing cross path
    string GetCrosspathString(){
        return topPath + "-" + middlePath + "-" + bottomPath;
    }

    //caculating sell value of bananas + bananas prduced in a round
    float GetFinalMultiplier()
    {
        //defing the bonus accourding to crosspath so an example 2-2-0 value
        FarmBonus bonus = CombineBonuses();
        return bonus.valueMultiplier + monkeyKnowledgeBonus;
    }

    //caculates the amount of bananas
    int GetBananaCount(){
        FarmBonus bonus = CombineBonuses();
        return bananas + bonus.extraBananas;
    }
    //caclutating the income each round
    float GetIncomePerRound(){
        float value = Mathf.Ceil(bananaValue * GetFinalMultiplier());
        return GetBananaCount() * value;
    }
    
    //calculate the value over a peroid of rounds
    float GetTotalIncome(){
        return Mathf.Ceil(GetIncomePerRound() * rounds);
    }
    //calculates the total tower cost of the tower
    float GetTotalCost(){
        return baseCost + upgradeCost;
    }

    float GetNetProfit(){
        return GetTotalIncome() - GetTotalCost();
    }

    //return sell value
    float GetSellMultiplier(){
        FarmBonus bonus = CombineBonuses();

        float baseSell = (bonus.sellOverride > 0) ? bonus.sellOverride : sellBase;

        float total = baseSell + sellMKBonus;

        return Mathf.Min(total, 0.95f);
    }

    float GetSellValue(){
        return Mathf.Ceil(GetTotalCost() * GetSellMultiplier());
    }
}