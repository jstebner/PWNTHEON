using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private Camera cam;
    private Vector2 mousePos;
    private float moveSpeed = 25f;
    private float maxDodgeSpeed = 30f;
    private float dodgeTimer = 0f;
    private const float TimeDodge = 0.8f;
    public Rigidbody2D playerRB;
    private Vector2 dodgeDir;
    private playerHealth playerHealth;
    private SpriteRenderer playerSprite;
    public SpriteRenderer weapon;
    public Transform attackPoint;
    private float immunityTime;
    private float maxImmunityTime = TimeDodge;

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
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
                dodgeTimer = TimeDodge;
                resetDodgeCooldown();
                state = State.Dodge;
            }
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        Debug.Log(lookDirection.x);
        if (lookDirection.x > 0f) {
            playerSprite.flipX = true;
            weapon.sortingOrder = 2;
        } else if (lookDirection.x < 0f) {
            playerSprite.flipX = false;
            weapon.sortingOrder = 0;
        }
        if (moveX == 0 && moveY == 0) {
            //Idle
            return;
        } else {
            playerRB.AddForce(new Vector2(moveX, moveY).normalized * moveSpeed);
            // if (moveX > 0) {
            //     playerSprite.flipX = true;
            //     weapon.flipX = true;
            //     weapon.sortingOrder = 1;
            //     attackPoint.position = new Vector3(playerRB.position.x + .42f, playerRB.position.y + 0.75f, 0f);
            // } else if (moveX < 0) {
            //     playerSprite.flipX = false;
            //     weapon.flipX = false;
            //     weapon.sortingOrder = 0;
            //     attackPoint.position = new Vector3(playerRB.position.x - 0.39f, playerRB.position.y + 0.75f, 0f);
            // }
        }

    }

    void handleDodge() {
        if (dodgeDir.x == 0 && dodgeDir.y == 0) {
            //no direction
            state = State.Normal;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
            immunityTime = 0f;
            return;
        }
        if (dodgeTimer >= 0){
            float direction = 1f;
            if (dodgeDir.x == 0) {
                direction = playerSprite.flipX ? 1f : -1f;
            }
            else {
                direction = dodgeDir.x / Mathf.Abs(dodgeDir.x);
            }
            transform.Rotate(new Vector3(0, 0, -360 * (Time.deltaTime/TimeDodge) * direction));
            playerRB.AddForce(dodgeDir * maxDodgeSpeed);
            dodgeTimer -= Time.deltaTime;
            // Add rotation here
        } else {
            state = State.Normal;
            dodgeTimer = 0f;
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
        }
        immunityTime = maxImmunityTime;
    }

    void resetDodgeCooldown() {
        slider.value = slider.minValue;
    }
}
