using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarValue : MonoBehaviour
{
    Scrollbar scroll;
    void Start()
    {
        scroll= GetComponent<Scrollbar>();
        scroll.value = 1;
    }


}
