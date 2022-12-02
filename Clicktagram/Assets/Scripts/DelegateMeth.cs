using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateMeth : MonoBehaviour
{
    

    public delegate void DelegateDebug();
    public DelegateDebug delegateDebug;

    public delegate int DelegateInt(int x, int y);
    public DelegateInt delegateInt;



    void Start()
    {
        AddDelegatDebug(Debug1);
        AddDelegatDebug(Debug2);
        delegateDebug -= Debug3 ;

        
        if (delegateDebug != null)
            delegateDebug();


        //Int ekleme cikarma yontemleri
        AddDelegateInt(Toplama);
        delegateInt += Cikarma;
        delegateInt += Carpma;
        delegateInt += (int x, int y) =>
        {
            //Debug.Log("Bölündü");
            return x / y;
        };


        System.Delegate[] funcs = delegateInt.GetInvocationList();

        for (int i = 0; i < funcs.Length; i++)
        {
            int result = ((DelegateInt)funcs[i]).Invoke(20,5);
            //Debug.Log(result);
        }

        int resultt=((DelegateInt)funcs[1]).Invoke(5, 10);
        //Debug.Log(resultt);

    }



    private void Debug1()
    {
        //Debug.Log("Debug 1");
    }

    private void Debug2()
    {
        //Debug.Log("Debug 2");

    }

    private void Debug3()
    {
        //Debug.Log("Debug 3");
    }

    //Delegate debug ekleme methodu bunu baska script uzerinden de ekleyebiliriz.
    public void AddDelegatDebug(DelegateDebug method)
    {
        delegateDebug += method;
    }


    private int Toplama(int x, int y)
    {
        //Debug.Log("Toplama");
        return x + y;
    }

    private int Cikarma(int x,int y)
    {
        //Debug.Log("Cikarma");
        return x - y;
    }

    private int Carpma(int x, int y)
    {
        //Debug.Log("Carpma");
        return x * y;
    }

    //Delegateint ekleme methodu bunu baska script uzerinden de ekleyebiliriz.
    private void AddDelegateInt(DelegateInt method)
    {
        delegateInt += method;
    }
}
