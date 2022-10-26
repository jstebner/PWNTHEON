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
    public GameObject bossFireballPrefab;
    public GameObject physicalSlamPrefab;
    private float bossFireballSpeed = 5f;
    private float slamSizeIncreaseRate = 8f;
    private List<FireBallStruct> activeFireballs = new List<FireBallStruct>();
    private List<GameObject> activeSlams = new List<GameObject>();
    MenuController menuController;

    void Awake() {
        menuController = GameObject.Find("Menus").GetComponent<MenuController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
        health = 100;
        hp.setMaxHealth(100);
    }

    void FixedUpdate() {
        moveFireballs();
        updateSlams();
        if (Input.GetKeyDown(KeyCode.J)) {
            if (activeSlams.Count == 0) {
                newPhysicalSlam();
            }
        }
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
        menuController.setGameWon();
        Destroy(this.gameObject);
    }

    public void newBossFireball(Vector3 playerPos, Vector3 bossPos) {
        Vector3 direction = (playerPos - bossPos).normalized;
        GameObject fireballObject = Instantiate(bossFireballPrefab, bossPos + direction, Quaternion.identity);
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

    private void updateSlams() {
        for (int i = activeSlams.Count - 1; i >= 0; i--) {
            if (activeSlams[i].transform.localScale.x >= 37) {
                Destroy(activeSlams[i]);
                activeSlams.RemoveAt(i);
            } else {
                activeSlams[i].transform.localScale += new Vector3(slamSizeIncreaseRate * Time.deltaTime, 0.389375f * slamSizeIncreaseRate * Time.deltaTime, slamSizeIncreaseRate * Time.deltaTime);
            }
        }
    }

    private void newPhysicalSlam() {
        GameObject slamObject = Instantiate(physicalSlamPrefab, new Vector2(0.14f, 0.62f), Quaternion.identity);
        activeSlams.Add(slamObject);
    }
}
