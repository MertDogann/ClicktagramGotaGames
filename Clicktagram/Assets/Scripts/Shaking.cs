using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public bool start;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration = 1.0f;
    [SerializeField] float shakeValue = 10f;

    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shakings());
        }
        
    }

    IEnumerator Shakings()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position =(( startPos + Random.insideUnitSphere)) /shakeValue;
            yield return null;
        }

        transform.position = startPos;
    }
}
