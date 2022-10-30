using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Camera mainCamera;
    
    void Start()
    {
        mainCamera = GetComponent<Camera> ();
    }

    public void PlayGame()
    {
        Debug.Log("Play Game!");
        for (int i=0; i < 100; i++) {
            mainCamera.orthographicSize += 1;

        }
        SceneManager.LoadScene("Boss1");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
