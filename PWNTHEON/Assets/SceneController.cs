using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private bool paused = false;
    private AudioSource music;
    // Update is called once per frame
    void Start() {
        music = FindObjectOfType<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            switchTimeScale();
        }       
    }

    void switchTimeScale() {
        paused = !paused;
        if (paused) {
            Time.timeScale = 0f;
            music.volume = 0.15f;
        } else {
            Time.timeScale = 1f;
            music.volume = 0.3f;
        }
    }
}
