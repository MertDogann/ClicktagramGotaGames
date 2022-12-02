using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelecetionScreen : MonoBehaviour
{
    int currentScene = 0;

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {

        currentScene = PlayerPrefs.GetInt("CurrentScene");
        if (SceneManager.GetActiveScene().name != "Level " + currentScene)
        {
            SceneManager.LoadScene("Level " + currentScene);
        }
    }

    public void Male()
    {
        currentScene = 2;
        PlayerPrefs.SetInt("CurrentScene", currentScene);
        SceneManager.LoadScene(currentScene);
    }

    public void Female()
    {
        currentScene = 1;
        PlayerPrefs.SetInt("CurrentScene", currentScene);
        SceneManager.LoadScene(currentScene);
    }
}
