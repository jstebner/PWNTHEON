using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private float moveSpeed = 25f;
    private float maxDodgeSpeed = 100f;
    private float dodgeTimer = 0f;
    public Rigidbody2D playerRB;
    private Vector2 dodgeDir;
    private playerHealth playerHealth;
    private SpriteRenderer playerSprite;
    public SpriteRenderer weapon;
    public Transform attackPoint;
    private float immunityTime;
    private float maxImmunityTime = 0.8f;

    public Slider slider;
    public Image fill;

    private State state;
    private enum State {
        Normal,
        Dodge,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
        playerRB = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<playerHealth>();
        playerSprite = GetComponent<SpriteRenderer>();
        slider.minValue = 0f;
        slider.maxValue = 3f;
        slider.value = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == State.Normal) {
            handleMovement();
            slider.value += Time.deltaTime;
        } 
        else if (state == State.Dodge) {
            handleDodge();
        }
        if (immunityTime > 0) {
            playerHealth.canTakeDamage = false;
            immunityTime -= Time.deltaTime;
        } else {
            playerHealth.canTakeDamage = true;
        }
        //transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0f);
    }

    void handleMovement() {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.D)) {
            moveX = 1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.W)) {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.S)) {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (slider.value == slider.maxValue) {
                dodgeDir = new Vector2(moveX, moveY).normalized;
                dodgeTimer = 0.08f;
                resetDodgeCooldown();
                state = State.Dodge;
            }
        }

        if (moveX == 0 && moveY == 0) {
            //Idle
            return;
        } else {
            playerRB.AddForce(new Vector2(moveX, moveY).normalized * moveSpeed);
            if (moveX > 0) {
                playerSprite.flipX = true;
                weapon.flipX = true;
                attackPoint.position = new Vector3(playerRB.position.x + .42f, playerRB.position.y + 0.75f, 0f);
            } else if (moveX < 0) {
                playerSprite.flipX = false;
                weapon.flipX = false;
                attackPoint.position = new Vector3(playerRB.position.x - 0.39f, playerRB.position.y + 0.75f, 0f);
            }
        }

    }

    void handleDodge() {
        if (dodgeDir.x == 0 && dodgeDir.y == 0) {
            //no direction
            state = State.Normal;
            immunityTime = 0f;
            return;
        }
        if (dodgeTimer >= 0){
            playerRB.AddForce(dodgeDir * maxDodgeSpeed);
            dodgeTimer -= Time.deltaTime;
        } else {
            state = State.Normal;
            dodgeTimer = 0f;
        }
        immunityTime = maxImmunityTime;
    }

    void resetDodgeCooldown() {
        slider.value = slider.minValue;
    }
}
