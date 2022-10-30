using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool paused = false;
    public static bool gameOver = false;
    public static bool gameWon = false;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject gameWonMenu;

    void Awake() {
        paused = false;
        gameOver = false;
        gameWon = false;
    }

    void Start() {
        resume();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (!gameOver & !gameWon) {
                if (paused) {
                    resume();
                } else {
                    pause();
                }
            }
        }     
    }

    public void pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void backToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame() {
        Application.Quit();
    }
    
    public void setGameOver() {
        gameOver = true;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void setGameWon() {
        gameWon = true;
        gameWonMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
