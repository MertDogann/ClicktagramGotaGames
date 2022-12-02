using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveControll : MonoBehaviour
{
    public int isEmpty;
    [SerializeField] List<GameObject> childs;
    public string activeTag;
    public string nexTag;


    void Update()
    {

        ActiveDeactiveControl();
        
    }

    void ActiveDeactiveControl()
    {
        for (int i = 0; i < childs.Count; i++)
        {
            if (childs[i].activeInHierarchy)
            {
                ActiveChild(i);
                break;
            }
            if (!childs[i].activeInHierarchy)
            {
                DeactiveChild();
            }
        }
    }
    void ActiveChild(int i)
    {
        activeTag = childs[i].gameObject.tag;
        gameObject.tag = activeTag;
        if (i+1 != childs.Count)
        {
            nexTag = childs[i + 1].gameObject.tag;
        }
        
        isEmpty = 1;
    }
    private void DeactiveChild()
    {
        activeTag = null;
        gameObject.tag = "20";
        nexTag = null;
        isEmpty = 2;
    }


}
