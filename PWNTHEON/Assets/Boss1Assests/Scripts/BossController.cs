using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


struct FireBallStruct {
    public GameObject fireball;
    public Vector3 direction;
}

struct SlamStruct {
    public GameObject slam;
    public float increaseSizeRate;
}

public class BossController : MonoBehaviour
{
    public healthbar hp;
    private int health;
    private int maxHealth;
    public bool canTakeDamage;
    public GameObject bossFireballPrefab;
    public GameObject slamPrefab;
    public GameObject soundBlastPrefab;
    private float bossFireballSpeed = 5f;
    private float physicalSlamSizeIncreaseRate = 12f;
    private float abilitySlamSizeIncreaseRate = 7f;
    private List<FireBallStruct> activeFireballs = new List<FireBallStruct>();
    private List<SlamStruct> activeSlams = new List<SlamStruct>();
    MenuController menuController;
    private GameObject player;
    private GameObject boss;
    private bool switchedPhase = false;

    public float maxMagicBulletCooldownTime = 0.25f;
    public float maxMeleeCooldownTime = 2f;
    public float maxMagicBulletVolleyCooldownTime = 2f;
    public float maxTooCloseToBossCooldownTime = 0.20f;
    public int maxBulletsInVolley = 3;
    public int maxMagicBulletVolleys = 2;

    void Awake() {
        menuController = GameObject.Find("Menus").GetComponent<MenuController>();
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.Find("Greuhl");
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
        if (hp.getHeath() <= 30 && !switchedPhase) {
            switchPhase();
            switchedPhase = true;
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
        float incSizeRate = 0f;
        for (int i = activeSlams.Count - 1; i >= 0; i--) {
            if (activeSlams[i].slam.transform.localScale.x >= 37) {
                Destroy(activeSlams[i].slam);
                activeSlams.RemoveAt(i);
            } else {
                incSizeRate = activeSlams[i].increaseSizeRate;
                activeSlams[i].slam.transform.localScale += new Vector3(incSizeRate * Time.deltaTime, 0.389375f * incSizeRate * Time.deltaTime, incSizeRate * Time.deltaTime);
                SlamStruct slam = activeSlams[i];
                slam.increaseSizeRate += 0.1f;
                activeSlams[i] = slam;
            }
        }
    }

    public void newPhysicalSlam() {
        GameObject slamObject = Instantiate(slamPrefab, new Vector2(0.14f, 0.62f), Quaternion.identity);
        SlamStruct currentSlam;
        currentSlam.slam = slamObject;
        currentSlam.increaseSizeRate = physicalSlamSizeIncreaseRate;
        activeSlams.Add(currentSlam);
    }

    public void newSoundBlast() {
        GameObject slamObject = Instantiate(soundBlastPrefab, new Vector2(0.14f, 0.62f), Quaternion.identity);
        SlamStruct currentSlam;
        currentSlam.slam = slamObject;
        currentSlam.increaseSizeRate = abilitySlamSizeIncreaseRate;
        activeSlams.Add(currentSlam);
    }

    public void meleeAttack() {
        if (Vector3.Distance(player.transform.position, transform.position) <= 2.5f) {
            player.GetComponent<playerHealth>().damagePlayer(30, false);
        }
    }

    public void switchPhase() {
        maxMagicBulletCooldownTime = 0.01f;
        maxMeleeCooldownTime = 1.5f;
        maxMagicBulletVolleyCooldownTime = 0.5f;
        maxTooCloseToBossCooldownTime = 0.15f;
        maxBulletsInVolley = 5;
        maxMagicBulletVolleys = 1;
        boss.GetComponent<Animator>().SetTrigger("Switch Phase");
    }
}
