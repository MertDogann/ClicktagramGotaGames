using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class AddNewButton : MonoBehaviour
{
    GameManager gameManager;
    Tutorial tutorial;
    [SerializeField] List<GameObject> fields;
    [SerializeField] TextMeshProUGUI clickButtonFeeText;
    [SerializeField] TextMeshProUGUI mergeFeeText;
    [SerializeField] ParticleSystem mergeParticle;
    List<int> firstListing = new List<int>();
    List<int> secListing = new List<int>();
    int mergeFee;
    
    int clickButtonFee;
    int mintagFirst;
    int mintagSec = -1;
    int smallIndex = -1;
    int largeIndex;
    int aNext;
    bool merging;
    bool finishMerge;
    
    void Awake()
    {
        CallComponents();
        
        
    }

    void CallComponents()
    {
        gameManager = GetComponentInParent<GameManager>();
        tutorial = GetComponentInParent<GameManager>().GetComponentInChildren<Tutorial>();
    }

    void Start()
    {
        clickButtonFee = PlayerPrefs.GetInt("ClickButtonFee", 10);
        mergeFee = PlayerPrefs.GetInt("MergeFee", 20);
        ClickForceSave();
        ClickButtonFeeText();
        MergeButtonFeeText();
    }

    void Update()
    {
        if (tutorial.gameObject.activeInHierarchy)
        {
            PlayerPrefs.SetInt("ClickButtonFee", clickButtonFee);
            PlayerPrefs.SetInt("MergeFee", mergeFee);
        }
        
    }

    public void ButtonMerge()
    {
        if (gameManager.GetTotalMoney >= mergeFee)
        {
            
            for (int i = 1; i <= fields.Count; i++)
            {
                FirstListAddIndex(i);
                if (i == fields.Count)
                {
                    for (int iii = 0; iii < fields.Count; iii++)
                    {
                        FirstListSmallIndex();
                        if (mintagFirst == 20)
                        {
                            secListing.Clear();
                            break;
                        }
                        if (smallIndex != -1)
                        {
                            for (int ii = (smallIndex); ii < fields.Count; ii++)
                            {
                                SecListAddIndex(ii);
                                if ((ii + 1) == fields.Count && secListing.Min() == 20)
                                {
                                    secListing.Clear();
                                    break;
                                }

                                if ((fields.Count - smallIndex) == secListing.Count)
                                {
                                    for (int iiii = 0; iiii < secListing.Count; iiii++)
                                    {
                                        if (mintagFirst == secListing[iiii])
                                        {
                                            SecListSmallIndex(iiii);
                                            secListing.Clear();
                                            merging = true;
                                            break;

                                        }

                                        if (mintagFirst != secListing[iiii])
                                        {
                                            if (iiii + 1 == secListing.Count)
                                            {
                                                DontMerge();
                                                break;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        if (merging)
                        {
                            Merging();
                            MergeIndexRes();
                            MergeSaveAndFee();
                            MergeButtonFeeText();

                            break;
                        }
                        else
                        {
                            firstListing.RemoveAt(firstListing.IndexOf(mintagFirst));
                        }

                        if (finishMerge)
                        {
                            firstListing.Clear();
                            finishMerge = false;
                            break;
                        }
                    }
                }
                if (mintagFirst == 20)
                {
                    firstListing.Clear();
                    MergeIndexRes();

                    break;

                }
            }
        }

    }

    void MergeButtonFeeText()
    {
        mergeFeeText.text = "$" + gameManager.SimplifiedValue(mergeFee, "Upgrade");
    }


    private void MergeSaveAndFee()
    {
        gameManager.GetTotalMoney -= mergeFee;
        mergeFee += 10;
        PlayerPrefs.SetInt("MergeFee", mergeFee);
    }

    void FirstListAddIndex(int i)
    {
        int firstListTags = System.Convert.ToInt32(fields[i - 1].gameObject.tag);
        firstListing.Add(firstListTags);
    }

    void FirstListSmallIndex()
    {
        mintagFirst = firstListing.Min();
        smallIndex = firstListing.IndexOf(mintagFirst) + 1;
    }
    void SecListAddIndex(int ii)
    {
        int secListTags = System.Convert.ToInt32(fields[ii].gameObject.tag);
        secListing.Add(secListTags);
    }
    void SecListSmallIndex(int iiii)
    {
        mintagSec = secListing[iiii];
        largeIndex = (secListing.IndexOf(mintagSec) + 1);
        largeIndex += smallIndex;
    }

    void DontMerge()
    {
        secListing.Clear();
        mintagSec = -1;
        if (smallIndex == 1)
        {
            finishMerge = true;
        }
    }

    void Merging()
    {
        firstListing.Clear();
        int.TryParse((fields[smallIndex - 1].GetComponent<ActiveControll>().nexTag), out aNext);
        PlayerPrefs.SetInt("FieldsActiveNum " + (smallIndex).ToString(), aNext);
        fields[smallIndex - 1].transform.GetChild(aNext).gameObject.SetActive(true);
        fields[smallIndex - 1].transform.GetChild((aNext - 1)).gameObject.SetActive(false);
        fields[largeIndex - 1].transform.GetChild(PlayerPrefs.GetInt("FieldsActiveNum " + (largeIndex).ToString())).gameObject.SetActive(false);
        MergeParticlePosition();

        PlayerPrefs.SetInt("FieldsActiveNum " + (largeIndex).ToString(), 0);
        PlayerPrefs.SetInt("IsEmpty " + (largeIndex).ToString(), 2);
        merging = false;
        MergeTutorial();
        
    }

    void MergeParticlePosition()
    {
        mergeParticle.transform.parent = fields[smallIndex - 1].transform;
        mergeParticle.transform.localPosition = new Vector3(0, 0.412f, 0.108f);
        mergeParticle.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        mergeParticle.Play();
    }

    void MergeTutorial()
    {
        if (tutorial.gameObject.activeInHierarchy && tutorial.GetTutorialIndex == 2)
        {
            tutorial.GetComponent<Animator>().SetBool("Merge", false);
            tutorial.GetTutorialIndex++;
            PlayerPrefs.SetInt("TutorialIndex", tutorial.GetTutorialIndex);
        }
    }

    void MergeIndexRes()
    {
        smallIndex = -1;
        largeIndex = -1;
        mintagFirst = -1;
        
        mintagSec = -1;
    }




    public void ButtonAdd()
    {
        if (gameManager.GetTotalMoney >= clickButtonFee)
        {
            bool butonCon = false;
            foreach (var field in fields)
            {
                if (field.gameObject.CompareTag("20"))
                { butonCon = true; }
                else
                { butonCon = false; }
                if (field.GetComponent<ActiveControll>().isEmpty == 2 && butonCon)
                {
                    PlayerPrefs.SetInt("IsEmpty " + field.gameObject.name, 1);
                    PlayerPrefs.SetInt("FieldsActiveNum " + field.gameObject.name, 0);
                    field.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                }
            }
            if (butonCon)
            {
                SaveAndFee();
                ClickButtonFeeText();
                AddButtonTutorial();
            }
        }
    }

    void AddButtonTutorial()
    {
        if (tutorial.gameObject.activeInHierarchy && tutorial.GetTutorialIndex <2)
        {
            tutorial.GetTutorialIndex++;
            PlayerPrefs.SetInt("TutorialIndex" , tutorial.GetTutorialIndex);
            tutorial.GetComponent<Animator>().SetBool("AddButton", false);
            tutorial.GetComponent<Animator>().SetBool("Click", true);

        }
    }

    void SaveAndFee()
    {
        gameManager.GetTotalMoney -= clickButtonFee;
        gameManager.GetClickForce += 1;
        
        gameManager.GetMoneyIncrease += 1;
        clickButtonFee += 20;
        PlayerPrefs.SetInt("ClickForce", (int)gameManager.GetClickForce);
        PlayerPrefs.SetFloat("MoneyIncrease", gameManager.GetMoneyIncrease);
        PlayerPrefs.SetInt("ClickButtonFee", clickButtonFee);
    }
    void ClickButtonFeeText()
    {
        clickButtonFeeText.text = "$" + gameManager.SimplifiedValue(clickButtonFee, "Upgrade");
    }



    void ClickForceSave()
    {
        for (int i = 1; i <= fields.Count; i++)
        {
            PlayerPrefs.GetInt("FieldsActiveNum " + i.ToString(), -1);
            PlayerPrefs.GetInt("IsEmpty " + i.ToString(), 0);
            if (PlayerPrefs.GetInt("IsEmpty " + i.ToString()) == 1)
            {
                fields[(i - 1)].transform.GetChild(PlayerPrefs.GetInt("FieldsActiveNum " + i.ToString())).gameObject.SetActive(true);
            }
        }
    }
















}
