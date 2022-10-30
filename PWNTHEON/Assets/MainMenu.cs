using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera mainCamera;
    private float fov;
    private float timeZoom = 10f;
    
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        fov = mainCamera.fieldOfView;
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        Debug.Log("Play Game!");
        while (timeZoom > 0) {
            mainCamera.fieldOfView -= 100 * Time.deltaTime;
            timeZoom -= Time.deltaTime;
            Debug.Log(timeZoom);
        }

        SceneManager.LoadScene("Boss1");
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
