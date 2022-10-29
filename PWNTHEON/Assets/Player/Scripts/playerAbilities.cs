using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerAbilities : MonoBehaviour
{
    public KeyCode abilityKey;
    public KeyCode abilityNextKey;
    private Color selectColor = new Color(255,215,0,100);
    private Color notSelectColor = new Color(0,0,0,100);
    private int selected = 0;
    private float keyDelayMax = 0.25f;
    private float keyDelay;
    private movement movement;

    [Header("Fireball")]
    public Image fireballImg;
    public Image fireballSelect;
    [SerializeField] TextMeshProUGUI countdown1;
    public float cooldown1 = 5;
    bool isCooldown1 = false;

    [Header("Fat Roll")]
    public Image rollImg;
    public Image rollSelect;
    [SerializeField] TextMeshProUGUI countdown2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;

    [Header("Heal")]
    public Image healImg;
    public Image healSelect;
    [SerializeField] TextMeshProUGUI countdown3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public healthbar hp;

    // Start is called before the first frame update
    void Start()
    {
        fireballImg.fillAmount = 0;
        fireballSelect.color = selectColor;
        countdown1.text = "";
        rollImg.fillAmount = 0;
        countdown2.text = "";
        healImg.fillAmount = 0;
        countdown3.text = "";
        movement = GetComponent<movement>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if  ((Input.GetKey(abilityNextKey) || Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetAxis("Mouse ScrollWheel") < 0f )&& keyDelay <= 0){
            keyDelay = keyDelayMax;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                selected = (selected + 2) % 3;
            else
                selected = (selected + 1) % 3;
            if (selected == 0) {
                fireballSelect.color = selectColor;
                rollSelect.color = notSelectColor;
                healSelect.color = notSelectColor;
            }
            else if (selected == 1) {
                fireballSelect.color = notSelectColor;
                rollSelect.color = selectColor;
                healSelect.color = notSelectColor;
            }
            else if (selected == 2) {
                fireballSelect.color = notSelectColor;
                rollSelect.color = notSelectColor;
                healSelect.color = selectColor;
            }
        }
        Cooldown();
    }

    void Cooldown() {
        if (isCooldown1 == true) {
            fireballImg.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            countdown1.text = ((int)Mathf.Round((fireballImg.fillAmount / 1) * 5)).ToString();
            if (fireballImg.fillAmount <= 0) {
                fireballImg.fillAmount = 0;
                countdown1.text = "";
                isCooldown1 = false;
            }
        }
        if (isCooldown2 == true) {
            rollImg.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            countdown2.text = ((int)Mathf.Round((rollImg.fillAmount / 1) * 5)).ToString();
            if (rollImg.fillAmount <= 0) {
                rollImg.fillAmount = 0;
                countdown2.text = "";
                isCooldown2 = false;
            }
        }
        if (isCooldown3 == true) {
            healImg.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            countdown3.text = ((int)Mathf.Round((healImg.fillAmount / 1) * 5)).ToString();
            if (healImg.fillAmount <= 0) {
                healImg.fillAmount = 0;
                countdown3.text = "";
                isCooldown3 = false;
            }
        }
    }

    void Fireball() {
        if (isCooldown1 == false){
            isCooldown1 = true;
            fireballImg.fillAmount = 1;
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
}
