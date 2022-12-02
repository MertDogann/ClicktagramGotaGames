using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] BoxCollider clickBox;
    GameManager gameManager;
    Animator gameTutorial;
    int tutorialIndex;

    public int GetTutorialIndex {  get { return tutorialIndex; } set { tutorialIndex = value; } }


    void Awake()
    {
        CallComponents();
    }
    void CallComponents()
    {
        gameManager = GetComponentInParent<GameManager>();
        gameTutorial = GetComponent<Animator>();
    }

    void Start()
    {
        tutorialIndex = PlayerPrefs.GetInt("TutorialIndex", 0);
        if (PlayerPrefs.GetInt("TotalFollowers") == 0)
        {
            gameTutorial.SetBool("TapPlay", true);
            clickBox.enabled = false;
            
        }
        else
        {
            
            gameTutorial.SetBool("Click", true);

            gameTutorial.SetBool("TapPlay", false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }



    
    void LateUpdate()
    {
        if (tutorialIndex == 3)
        {
            gameObject.SetActive(false);
            

        }
        if (PlayerPrefs.GetInt("ClickButtonFee") <= gameManager.GetTotalMoney && tutorialIndex < 2)
        {
           
            gameTutorial.SetBool("AddButton", true);
        }
        else
        {
            gameTutorial.SetBool("AddButton", false);
        }
        if (PlayerPrefs.GetInt("MergeFee") <= gameManager.GetTotalMoney && tutorialIndex == 2)
        {
            gameTutorial.SetBool("Merge", true);
        }
        
        
    }

    public void TapPlay()
    {
        
        gameTutorial.SetBool("TapPlay", false);
        transform.GetChild(2).gameObject.SetActive(false);
        gameTutorial.SetBool("Click", true);
        StartCoroutine(WaitClickBox());

    }

    IEnumerator WaitClickBox()
    {
        yield return new WaitForSeconds(0.2f);
        clickBox.enabled = true;
    }
}
