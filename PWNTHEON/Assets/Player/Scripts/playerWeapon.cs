using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerWeapon : MonoBehaviour
{
    private Camera cam;
    private Transform parent;
    private Vector3 mousePos;
    public SpriteRenderer sword;
    public Transform weapon;
    public Transform attackPoint;
    public float attackRange = 0.75f;
    public LayerMask enemyLayers;
    private int swordDamage = 1;
    private Vector3 endpoint;

    public Slider slider;
    public Image fill;

    [Header("Audio")]
    [SerializeField] private AudioSource swordSwipeSoundEffect;


    void Start() {
        slider.minValue = 0f;
        slider.maxValue = 0.2f;
        slider.value = 0.2f;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        parent = weapon.parent;

        //sword = this.gameObject.transform.GetChild(0);
    }

    Vector3 get_endpoint(Vector3 origin, Vector3 cursor, float r) {
        float d = Mathf.Sqrt(Mathf.Pow(origin[0] - cursor[0], 2) + Mathf.Pow(origin[1] - cursor[1], 2));
        if (d==0) {
            return origin;
        }
        float xofs = (cursor[0]-origin[0])*r/d;
        float yofs = (cursor[1]-origin[1])*r/d;
        
        return new Vector3(origin[0]+xofs, origin[1]+yofs, origin[2]);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        endpoint = get_endpoint(parent.position, mousePos, 10f);
        Vector3 rotation = endpoint - weapon.position;
        float rotZ = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg - 90;
        weapon.rotation = Quaternion.Euler(0,0,rotZ);
        if (slider.value != slider.maxValue) {
            weapon.position = Vector3.MoveTowards(parent.position, endpoint, Mathf.Sin(Mathf.PI*slider.value/slider.maxValue) / 2.5f);
        }
        attackPoint.position = sword.bounds.center;
        
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
