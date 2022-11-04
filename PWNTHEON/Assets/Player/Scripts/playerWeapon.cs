using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerWeapon : MonoBehaviour
{
    private Camera cam;
    private Vector3 mousePos;
    public SpriteRenderer sword;
    public Transform weapon;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private int swordDamage = 10;

    public Slider slider;
    public Image fill;

    [Header("Audio")]
    [SerializeField] private AudioSource swordSwipeSoundEffect;


    void Start() {
        slider.minValue = 0f;
        slider.maxValue = 3f;
        slider.value = 3f;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //sword = this.gameObject.transform.GetChild(0);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - weapon.position;
        float rotZ = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg - 90;
        weapon.rotation = Quaternion.Euler(0,0,rotZ);
        attackPoint.position = Vector3.MoveTowards(attackPoint.position, sword.bounds.center, 1);
        
        if (Input.GetKey(KeyCode.Mouse0)) {
            attack();
        }
        manageCooldowns();
    }

    private void attack() {
        if (slider.value == slider.maxValue) {
            swordSwipeSoundEffect.Play();
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hits) {
                if (enemy.tag == "Boss") {
                    Debug.Log(enemy.name + " hit.");
                    enemy.GetComponent<BossController>().damageBoss(swordDamage);
                }
            }
            resetAttackMeter();
        }
        return;
    }
    
    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void manageCooldowns() {
        slider.value += Time.deltaTime;
    }

    private void resetAttackMeter() {
        slider.value = slider.minValue;
    }
}
