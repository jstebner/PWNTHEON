using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    
    void Update()
    {
        if (MenuController.paused || MenuController.gameOver || MenuController.gameWon) {
            music.volume = 0.15f;
        } else {
            music.volume = 0.5f;
        }
    }

}
