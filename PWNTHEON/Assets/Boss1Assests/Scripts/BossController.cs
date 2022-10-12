using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


struct FireBallStruct {
    public GameObject fireball;
    public Vector3 direction;
}

public class BossController : MonoBehaviour
{
    public healthbar hp;
    private int health;
    private int maxHealth;
    public bool canTakeDamage;
    public GameObject bossFireball;
    private float bossFireballSpeed = 5f;
    private List<FireBallStruct> activeFireballs = new List<FireBallStruct>();

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
        health = 100;
        hp.setMaxHealth(100);
    }

    void FixedUpdate() {
        moveFireballs();
    }

    public void damageBoss(int lostHealth) {
        if (canTakeDamage) {
            health -= lostHealth;
            if (health <= 0) {
                hp.setHealth(0);
                killBoss();
            } else {
                hp.setHealth(health);
            }
        }
    }

    private void killBoss() {
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void newBossFireball(Vector3 playerPos, Vector3 bossPos) {
        Vector3 direction = (playerPos - bossPos).normalized;
        GameObject fireballObject = Instantiate(bossFireball, bossPos + direction, Quaternion.identity);
        Physics2D.IgnoreCollision(fireballObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        FireBallStruct currentFireball;
        currentFireball.fireball = fireballObject;
        currentFireball.direction = direction;
        activeFireballs.Add(currentFireball);
        fireballObject.transform.position += direction * bossFireballSpeed * Time.deltaTime;
    }

    private void moveFireballs() {
        for (int i = activeFireballs.Count - 1; i >= 0; i--) {
            if (activeFireballs[i].fireball == null) {
                activeFireballs.RemoveAt(i);
            } else {
                activeFireballs[i].fireball.transform.position += activeFireballs[i].direction * bossFireballSpeed * Time.deltaTime;
            }
        }
    }
}
