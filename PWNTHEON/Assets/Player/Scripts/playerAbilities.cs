using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerAbilities : MonoBehaviour
{
    public KeyCode abilityKey;
    public KeyCode abilityNextKey;
    private Color selectColor = new Color(255,215,0,50);
    private Color notSelectColor = new Color(0,0,0,0);
    private int selected = 0;
    private float keyDelayMax = 0.2f;
    private float keyDelay;
    private movement movement;
    public Camera cam;
    private Vector3 mousePos;


    struct FireBallStruct {
        public GameObject fireball;
        public Vector3 direction;
    }
    [Header("Fireball")]
    public Image fireballImg;
    [SerializeField] TextMeshProUGUI countdown1;
    public float cooldown1 = 5f;
    public float fireballSpeed = 5f;
    bool isCooldown1 = false;
    public GameObject FireBallPrefab;
    private FireBallStruct activeFireball;
    //private castFireball castFireball;

    [Header("Fat Roll")]
    public Image rollImg;
    [SerializeField] TextMeshProUGUI countdown2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;

    [Header("Heal")]
    public Image healImg;
    [SerializeField] TextMeshProUGUI countdown3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public healthbar hp;

    [Header("Audio")]
    [SerializeField] private AudioSource fireballSoundEffect;

    public Image[] selections;

    // Start is called before the first frame update
    void Start()
    {
        fireballImg.fillAmount = 0;
        selections[0].color = selectColor;
        countdown1.text = "";
        rollImg.fillAmount = 0;
        countdown2.text = "";
        healImg.fillAmount = 0;
        countdown3.text = "";
        movement = GetComponent<movement>();
        //castFireball = GetComponent<castFireball>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if  (Input.GetKey(abilityKey) && movement.state == movement.State.Normal){
            if (selected == 0)
                Fireball();
            else if (selected == 1)
                FatRoll();
            else if (selected == 2)
                Heal();
        }
        if (keyDelay > 0f){
            keyDelay -= Time.deltaTime;
        }
        else if  ((Input.GetKey(abilityNextKey) || scroll > 0f || scroll < 0f ) && keyDelay <= 0f ){
            keyDelay = keyDelayMax;
            selections[selected].color = notSelectColor;
            if (scroll > 0f)
            {
                selected -= 1;
                if (selected < 0)
                    selected += 3;
            }
            else if ((scroll < 0f) || Input.GetKey(abilityNextKey))
                selected = (selected + 1) % 3;

            selections[selected].color = selectColor;
        }
        Cooldown();
        moveFireballs();
    }

    void Cooldown() {
        if (isCooldown1 == true) {
            fireballImg.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            countdown1.text = ((int)Mathf.Round((fireballImg.fillAmount / 1) * cooldown1)).ToString();
            if (fireballImg.fillAmount <= 0) {
                fireballImg.fillAmount = 0;
                countdown1.text = "";
                isCooldown1 = false;
            }
        }
        if (isCooldown2 == true) {
            rollImg.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            countdown2.text = ((int)Mathf.Round((rollImg.fillAmount / 1) * cooldown2)).ToString();
            if (rollImg.fillAmount <= 0) {
                rollImg.fillAmount = 0;
                countdown2.text = "";
                isCooldown2 = false;
            }
        }
        if (isCooldown3 == true) {
            healImg.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            countdown3.text = ((int)Mathf.Round((healImg.fillAmount / 1) * cooldown3)).ToString();
            if (healImg.fillAmount <= 0) {
                healImg.fillAmount = 0;
                countdown3.text = "";
                isCooldown3 = false;
            }
        }
    }

    private void moveFireballs() {
        if (activeFireball.fireball == null || activeFireball.direction == null) {
            return;
        }
        activeFireball.fireball.transform.position += activeFireball.direction * fireballSpeed * Time.deltaTime;
    
    }

    void Fireball() {
        if (isCooldown1 == false){
            isCooldown1 = true;
            fireballImg.fillAmount = 1;
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPos = transform.position;
            newFireball(mousePos, playerPos);
            fireballSoundEffect.time = 0.5f;
            fireballSoundEffect.Play();
        }
    }

    void FatRoll() {
        if (isCooldown2 == false && !(movement.moveX == 0 && movement.moveY == 0)){
            isCooldown2 = true;
            rollImg.fillAmount = 1;
            movement.FatRolling = true;
            movement.startDodge();
        }
    }

    void Heal() {
        if (isCooldown3 == false){
            isCooldown3 = true;
            healImg.fillAmount = 1;

            var healthMax = hp.getMaxHeath();
            var healAmount = hp.getHeath() + 10;
            if (healAmount > healthMax)
                healAmount = healthMax;
            hp.setHealth((int)(healAmount));
        }
    }

    private void newFireball(Vector3 mousePos, Vector3 playerPos) {
        Vector3 direction = (mousePos - playerPos).normalized;
        GameObject fireballObject = Instantiate(FireBallPrefab, playerPos + direction, Quaternion.identity);
        Physics2D.IgnoreCollision(fireballObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        activeFireball.fireball = fireballObject;
        activeFireball.direction = direction;
    }
}
