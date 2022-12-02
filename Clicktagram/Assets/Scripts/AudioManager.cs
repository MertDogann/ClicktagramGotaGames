using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<GameObject> audioImages;
    [SerializeField] AudioClip click;
    public bool audioClick;

    void Awake()
    {
        CallComponents();
    }
    void Start()
    {
        audioClick = true;
    }

    void CallComponents()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickAudio()
    {
        audioSource.PlayOneShot(click);
    }





}
