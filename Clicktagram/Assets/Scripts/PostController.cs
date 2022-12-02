using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PostController : MonoBehaviour
{
    public int postFollowersIncrease;
    public int postLevel;
    [SerializeField] TextMeshProUGUI lvlText;
    void Start()
    {
        lvlText.text ="LVL "+ postLevel.ToString();
    }

}
