using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MakeItButton : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    GameObject button;
    
    void Start()
    {
        button = this.gameObject;
    }

    void Update()
    {
        ButtonMake();
        
    }

    void ButtonMake()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                unityEvent.Invoke();
            }
        }
    }


}
