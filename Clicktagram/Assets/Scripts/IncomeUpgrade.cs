using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncomeUpgrade : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TextMeshProUGUI incomeUpgradeFeeText;
    //[SerializeField] TextMeshProUGUI incomeUpgradeNowText;
    //[SerializeField] TextMeshProUGUI incomeUpgradeNextText;
    float incomeUpgradeFee;


    void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    void Start()
    {
        incomeUpgradeFee = PlayerPrefs.GetInt("IncomeUpgradeFee", 50);
        IncomeUpgradeText();
    }

    public void UpgradeIncome()
    {
        if (gameManager.GetTotalMoney >= incomeUpgradeFee)
        {

            gameManager.GetTotalMoney -= incomeUpgradeFee;
            gameManager.GetMoneyIncreaseRate += 0.1f;
            PlayerPrefs.SetFloat("MoneyIncreaseRate", gameManager.GetMoneyIncreaseRate);
            PlayerPrefs.SetFloat("MoneyIncrease", gameManager.GetMoneyIncrease);

            incomeUpgradeFee *= 1.3f;
            PlayerPrefs.SetInt("IncomeUpgradeFee", (int)incomeUpgradeFee);
            IncomeUpgradeText();

        }

    }
    void IncomeUpgradeText()
    {
        //incomeUpgradeNowText.text = gameManager.GetMoneyIncreaseRate.ToString("F2");
        //incomeUpgradeNextText.text = (gameManager.GetMoneyIncreaseRate + 0.1f).ToString("F2");
        incomeUpgradeFeeText.text = "$" + incomeUpgradeFee.ToString("F0");
        incomeUpgradeFeeText.text = "$" + gameManager.SimplifiedValue(incomeUpgradeFee, "Upgrade");
    }
}
