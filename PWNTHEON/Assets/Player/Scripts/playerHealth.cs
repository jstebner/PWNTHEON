using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public healthbar hp;
    private int health;
    private int maxHealth;
    public bool canTakeDamage;
    public GameObject gameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas = GameObject.Find("Menus");
        gameOverCanvas.GetComponent<Canvas>().enabled = false;
        canTakeDamage = true;
        health = 100;
        hp.setMaxHealth(100);
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            damagePlayer(10);
        }
    }
    public void damagePlayer(int lostHealth) {
        if (canTakeDamage) {
            health -= lostHealth;
            if (health <= 0) {
                hp.setHealth(0);
                killPlayer();
            } else {
                hp.setHealth(health);
            }
        } else {
            Debug.Log("Dodged!");
        }
    }

    void killPlayer() {
        //SceneManager.loadScene("Main menu");
        gameOverCanvas.GetComponent<Canvas>().enabled = true;
        Destroy(this.gameObject);

    }
}
