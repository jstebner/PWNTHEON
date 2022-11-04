using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {  
        Debug.Log("Play Game!");

        SceneManager.LoadScene("Boss1");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OptionsBtn() {
        if (options.activeInHierarchy == true)
            options.SetActive(false);
        else
            options.SetActive(true);
    }
}
