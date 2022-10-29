using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public healthbar hp;
    private int maxHealth;
    public bool canTakeDamage;
    public bool dodging;
    MenuController menuController;
    public float immunityTimer = 0f;
    // Start is called before the first frame update

    void Awake() {
        menuController = GameObject.Find("Menus").GetComponent<MenuController>();
    }

    void Start()
    {
        canTakeDamage = true;
        hp.setMaxHealth(100);
    }

    void FixedUpdate() {
        
        if (immunityTimer > 0) {
            immunityTimer -= Time.deltaTime;
        } else {
            canTakeDamage = true;
        }
    }

    public void damagePlayer(int lostHealth, bool ability) {
        if (!canTakeDamage) {
           // Debug.Log("Immunity time prevented damage.");
            return;
        }
        if (!dodging || ability) {
            // if (ability) {
            //     Debug.Log("player was hit by ability");
            //     if (dodging) {
            //         Debug.Log("damage was NOT prevented while dodging");
            //     }
            // }
            float updatedHealth = hp.getHeath() - lostHealth;
            canTakeDamage = false;
            immunityTimer = 0.5f;
            if (updatedHealth <= 0) {
                hp.setHealth(0);
                killPlayer();
            } else {
                hp.setHealth(updatedHealth);
            }

            Debug.Log("player lost " + lostHealth + " health.");
        }
        else {
            Debug.Log("Dodged!");
        }
    }

    void killPlayer() {
        menuController.setGameOver();
        Destroy(this.gameObject);
    }
}
