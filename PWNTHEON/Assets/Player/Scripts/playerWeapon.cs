using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerWeapon : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private int swordDamage = 10;

    public Slider slider;
    public Image fill;

    void Start() {
        slider.minValue = 0f;
        slider.maxValue = 3f;
        slider.value = 3f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            attack();
        }
        manageCooldowns();
    }

    private void attack() {
        if (slider.value == slider.maxValue) {
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
