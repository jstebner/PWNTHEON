using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;

    
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
}
