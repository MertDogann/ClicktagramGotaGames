using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddNewPost : MonoBehaviour
{
    [SerializeField] List<GameObject> postList;
    GameManager gameManager;
    float postFollowersRate;
    float x;
    float followersPerSec;

    public float GetX { get { return x; } }

    void Awake()
    {
        CallComponents();
    }

    void CallComponents()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    void Start()
    {
        postFollowersRate = PlayerPrefs.GetInt("PostFollowersRate", 0);
        x = PlayerPrefs.GetInt("X", 0);
        PostSave();
    }

    void FixedUpdate()
    {
        followersPerSec = x * Time.deltaTime;
        gameManager.GetTotalFollowers = gameManager.GetTotalFollowers + followersPerSec;
        gameManager.GetTotalFollowersBar = gameManager.GetTotalFollowersBar + followersPerSec;
    }

    public void PostFee(int i, int ii )
    {
        PostAdd(i);
        PostAdd(ii);
    }

    void PostAdd(int i)
    {
        StartCoroutine(DelayPostActive(i));
        if (postList.Count != i + 1)
        {
            postFollowersRate += 1;
            PlayerPrefs.SetInt("PostFollowersRate", (int)postFollowersRate);
            x += postList[i].GetComponent<PostController>().postFollowersIncrease;
            PlayerPrefs.SetInt("X", (int)x);
        }
        PlayerPrefs.SetInt("PostActiveNum ", i + 1);
    }


    IEnumerator DelayPostActive(int i)
    {
        postList[i].transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        
        postList[i].transform.GetChild(0).gameObject.SetActive(false);
        postList[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);



    }



    void PostSave()
    {
        for (int i = 0; i < postList.Count; i++)
        {
            PlayerPrefs.GetInt("PostActiveNum ", -1);
            if (PlayerPrefs.GetInt("PostActiveNum ") == i + 1)
            {
                for (int ii = 0; ii < PlayerPrefs.GetInt("PostActiveNum "); ii++)
                {
                    postList[ii].transform.GetChild(0).gameObject.SetActive(false);
                    postList[ii].transform.GetChild(1).gameObject.SetActive(true);
                    postList[ii].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            }

        }
    }
}
