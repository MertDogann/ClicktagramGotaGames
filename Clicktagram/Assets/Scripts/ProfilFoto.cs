using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfilFoto : MonoBehaviour
{
    [SerializeField] List<GameObject> image;
    [SerializeField] TextMeshPro profilFotoLvlText;
    [SerializeField] RectTransform partic;
    
    GameManager gameManager;
    AddNewPost addNewPost;
    int profilFotoIndex;
    
    [SerializeField]Image nextImageFill;


    public int GetProfilFotoIndex { get { return profilFotoIndex; } }

    void Awake()
    {
        CallComponents();
    }
    void CallComponents()
    {
        gameManager = GetComponentInParent<GameManager>();
        addNewPost = GetComponentInParent<GameManager>().GetComponentInChildren<AddNewPost>();
    }
    void Start()
    {
        LoadToStartFoto();
        
    }

    void Update()
    {
         LevelFill(nextImageFill.fillAmount);
        switch (gameManager.GetTotalFollowers)
        {
            case < 1000:
                float lerpValue = gameManager.GetTotalFollowersBar / 1000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 1030:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 5000:

                lerpValue = gameManager.GetTotalFollowersBar / 3970;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 5100:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 25000:

                lerpValue = gameManager.GetTotalFollowersBar / 19900;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 25300:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 50000:

                lerpValue = gameManager.GetTotalFollowersBar / 24700;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 50400:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 200000:

                lerpValue = gameManager.GetTotalFollowersBar / 149600;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 200800:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 1000000:
                lerpValue = gameManager.GetTotalFollowersBar / 850400;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 1001000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 2000000:
                lerpValue = gameManager.GetTotalFollowersBar / 999000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 2002000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 10000000:
                lerpValue = gameManager.GetTotalFollowersBar / 7998000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 10010000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 30000000:
                lerpValue = gameManager.GetTotalFollowersBar / 19920000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 30030000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;

            case < 50000000:
                lerpValue = gameManager.GetTotalFollowersBar / 19970000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 50100000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;
            case < 100000000:
                lerpValue = gameManager.GetTotalFollowersBar / 49990000;
                nextImageFill.fillAmount = Mathf.Lerp(0, 1, lerpValue);
                break;
            case < 1000050000:
                gameManager.GetTotalFollowersBar = 0;
                nextImageFill.fillAmount = 0;
                break;




        }
    }

    void LevelFill(float amount)
    {
        float particAngle = amount * 360;
        partic.localEulerAngles = new Vector3(0, 0, -particAngle);
    }
    void LoadToStartFoto()
    {
        profilFotoIndex = PlayerPrefs.GetInt("ProfilFotoIndex", 0);
        profilFotoLvlText.text = (profilFotoIndex + 1).ToString();
        image[0].SetActive(false);
        image[profilFotoIndex].SetActive(true);
        image[profilFotoIndex].transform.GetChild(0).gameObject.SetActive(true);
        image[profilFotoIndex].transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ImageChange()
    {
        switch (gameManager.GetTotalFollowers)
        {
            case < 1000:
                
                break;
            case < 5000:
                if (!image[1].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(0, 1);
                }
                
                IndexControl(0, 1);
                break;
            case < 25000:
                if (!image[2].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(2, 3);
                }
                IndexControl(1, 2);
                break;
            case < 50000:
                if (!image[3].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(4, 5);
                }
                IndexControl(2, 3);
                break;
            case < 200000:
                if (!image[4].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(6, 7);
                }
                IndexControl(3, 4);
                break;
            case < 1000000:
                if (!image[5].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(8, 9);
                }
                IndexControl(4, 5);
                break;
            case < 2000000:
                if (!image[6].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(10, 11);
                    addNewPost.PostFee(12,0);
                }
                IndexControl(5, 6);
                break;
            case < 10000000:
                if (!image[7].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(13, 14);
                }
                IndexControl(6, 7);
                break;
            case < 30000000:
                if (!image[8].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(15, 16);
                }
                IndexControl(7, 8);
                break;
            case < 50000000:
                if (!image[9].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(17, 18);
                }
                IndexControl(8, 9);
                break;
            case < 100000000:
                if (!image[10].gameObject.activeInHierarchy)
                {
                    addNewPost.PostFee(19, 20);
                }
                IndexControl(9, 10);
                break;
        }
    }

    void IndexControl(int a,int b)
    {
        StartCoroutine(Delay(a,b));


        profilFotoIndex = b;
        PlayerPrefs.SetInt("ProfilFotoIndex", profilFotoIndex);
        profilFotoLvlText.text = (profilFotoIndex +1 ).ToString() ;
    }

    IEnumerator Delay(int a,int b)
    {
        image[a].SetActive(false);
        image[b].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        
        image[b].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        
    }


    
}
