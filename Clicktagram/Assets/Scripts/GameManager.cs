using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
//using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    AudioManager audioManager;
    ProfilFoto profilFoto;
    Tutorial tutorial;


    [SerializeField] List<Animator> clickAnims;
    [SerializeField] List<Animator> shaderAnims;
    bool starting;
    int startClickAnimCount;
    float clickForce;
    public float GetClickForce { get { return clickForce; } set { clickForce = value; } }



    [SerializeField] GameObject settingPanel;
    //GameObject vibrationImage;
    public bool vibration;
    public bool settings = false;


    [SerializeField] TextMeshPro totalFollowersText;
    float totalFollowers;
    public float GetTotalFollowers { get { return totalFollowers; } set { totalFollowers = value; } }
    float totalFollowersBar;
    public float GetTotalFollowersBar { get { return totalFollowersBar; } set { totalFollowersBar = value; } }

    [SerializeField] TextMeshProUGUI moneyText;
    float totalMoney;
    public float GetTotalMoney { get { return totalMoney; } set { totalMoney = value; } }

    float moneyIncrease;
    public float GetMoneyIncrease { get { return moneyIncrease; } set { moneyIncrease = value; } }

    float moneyIncreaseRate;
    public float GetMoneyIncreaseRate { get { return moneyIncreaseRate; } set { moneyIncreaseRate = value; } }
    float moneyClickForce;

    float nextUpdate = 3;
    [SerializeField] float nextClick = 1;


    void Awake()
    {
        CallComponents();
    }



    void Start()
    {
        PlayerPrefsStart();
        startClickAnimCount = clickAnims.Count;
        vibration = true;
        
    }


    void Update()
    {
        FollowersAndMoneyText();
        FollowersAndMoneySave();
        EverySecondClick();

    }

    void CallComponents()
    {
        tutorial = GetComponentInChildren<Tutorial>();
        audioManager = GetComponentInChildren<AudioManager>();
        profilFoto = GetComponentInChildren<ProfilFoto>();
        

        //vibrationImage = settingPanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }
    void PlayerPrefsStart()
    {
        totalFollowers = PlayerPrefs.GetInt("TotalFollowers", 0);
        totalFollowersBar = PlayerPrefs.GetInt("TotalFollowersBar", 0);
        clickForce = PlayerPrefs.GetInt("ClickForce", 1); ;
        totalMoney = PlayerPrefs.GetInt("TotalMoney", 0);
        moneyIncrease = PlayerPrefs.GetFloat("MoneyIncrease", 1);
        moneyIncreaseRate = PlayerPrefs.GetFloat("MoneyIncreaseRate", 0);
    }


    void FollowersAndMoneyText()
    {
        totalFollowersText.text = SimplifiedValue(totalFollowers, "Followers");
        moneyText.text = SimplifiedValue(totalMoney, "Money");
    }

    void FollowersAndMoneySave()
    {
        PlayerPrefs.SetInt("TotalFollowers", (int)totalFollowers);
        PlayerPrefs.SetInt("TotalFollowersBar", (int)totalFollowersBar);
        PlayerPrefs.SetInt("TotalMoney", (int)totalMoney);
    }
    void EverySecondClick()
    {
        if (Time.time >= nextUpdate && tutorial.GetTutorialIndex == 3)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + nextClick;
            Click(true);
            
        }
    }



    public void ClickTouch()
    {
        Click(!settings);
    }

    void Click(bool auto)
    {
        
        if (auto)
        {
            totalFollowers += clickForce;
            totalFollowersBar += clickForce;
            moneyClickForce = clickForce * moneyIncreaseRate;
            totalMoney += (moneyIncrease + moneyClickForce);
            ClickAnim();
            profilFoto.ImageChange();
            if (vibration)
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }

            if (tutorial.gameObject.activeInHierarchy)
            {
                tutorial.GetComponent<Animator>().SetBool("Click", false);
            }
            if (audioManager.audioClick)
            {
                audioManager.ClickAudio();
            }
        }
        
        

    }

    void ClickAnim()
    {
        for (int i = 0; i < shaderAnims.Count; i++)
        {
            if (!IsAnimationStatePlaying(shaderAnims[i], 0, "Shader"))
            {
                Debug.Log(IsAnimationStatePlaying(shaderAnims[i], 0, "Shader"));
                clickAnims.Add(shaderAnims[i]);
                shaderAnims[i].gameObject.GetComponentInParent<Shaking>().start = true;
                break;
            }
        }

        foreach (var anim in clickAnims)
        {
            anim.SetTrigger("click");
        }
        if (clickAnims.Count > startClickAnimCount)
        {
            clickAnims.RemoveAt((clickAnims.Count - 1));
        }
    }



    bool IsAnimationStatePlaying(Animator anim, int animLayer, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void SettingPanel()
    {
        settings = true;
        settingPanel.SetActive(true);
    }
    public void Resume()
    {
        settingPanel.SetActive(false);
        settings = false;
    }
    //public void VibrationOnOff()
    //{
    //    if (vibration)
    //    {
    //        vibrationImage.SetActive(true);
    //        vibration = false;
    //    }
    //    else
    //    {
    //        vibrationImage.SetActive(false);
    //        vibration = true;
    //    }
    //}


    public string SimplifiedValue(float value, string texts)
    {
        if (value < 10000)
        {

            return value.ToString("F0");
        }
        else if (value < 50000)
        {
            if (texts == "Money" || texts == "Upgrade")
            {
                return (value / 1000).ToString("F1") + "K";
            }
            else
            {
                return (value / 1000).ToString("F2") + "K";
            }
            
        }
        else if (value < 100000)
        {
            return (value / 1000).ToString("F1") + "K";
        }
        else if (value < 1000000)
        {
            return (value / 1000).ToString("F0") + "K";
        }
        else if (value < 10000000)
        {
            return (value / 1000000).ToString("F2") + "M";
        }
        else if (value < 1000000000)
        {
            return (value / 1000000).ToString("F1") + "M";
        }
        else
        {
            return (value / 1000000000).ToString("f0") + "G";
        }
    }

}
