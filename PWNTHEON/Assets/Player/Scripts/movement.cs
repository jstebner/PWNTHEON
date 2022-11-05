using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour
{
    private Camera cam;
    private Vector2 mousePos;
    private float moveSpeed = 25f;
    public float moveX = 0f;
    public float moveY = 0f;

    private float dodgeTimer = 0f;  // timer
    private const float TimeDodge = 0.5f;  // How long dodge lasts
    private const float TimeFatRoll = 1f;  // How long Fat Roll lasts
    private const float maxDodgeSpeed = 40f;  // Speed of dodge; distance traveled
    private const float maxFatRollSpeed = 20f;  // Speed of Fat Roll; distance traveled
    private const float maxRollAmount = 360f;  // Degree of dodge roll; num of spins
    private const float maxFatRollAmount = 720f;  // Degree of Fat Roll roll; num of spins
    private float dodgeSpeed = maxDodgeSpeed;
    private float rollAmount = maxRollAmount;
    private float maxDodgeTime = TimeDodge;
    public bool FatRolling = false;
    
    public Rigidbody2D playerRB;
    public Vector2 dodgeDir;
    private playerHealth playerHealth;
    private SpriteRenderer playerSprite;
    public SpriteRenderer weapon;
    public Transform attackPoint;
    private float immunityTime;
    private float maxImmunityTime = TimeDodge;

    public Slider slider;
    public Image fill;

    public State state;
    public enum State {
        Normal,
        Dodge,
    }

    [Header("Audio")]
    [SerializeField] private AudioSource rollSoundEffect;
    [SerializeField] private AudioSource runningSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Normal;
        playerRB = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<playerHealth>();
        playerSprite = GetComponent<SpriteRenderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        slider.minValue = 0f;
        slider.maxValue = 0.5f; //REVIEW: maye change this
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == State.Normal) {
            handleMovement();
            playerHealth.canTakeDamage = true;
            slider.value += Time.deltaTime;
        } 
        else if (state == State.Dodge) {
            handleDodge();
        }
        if (immunityTime > 0) {
            playerHealth.dodging = true;
            immunityTime -= Time.deltaTime;
        } else {
            playerHealth.dodging = false;
            playerHealth.canTakeDamage = true;
        }
        //transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0f);
    }

    void handleMovement() {
        moveX = 0f;
        moveY = 0f;
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
            startDodge();
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        //Debug.Log(lookDirection.x);
        if (lookDirection.x > 0f) {
            playerSprite.flipX = true;
            weapon.sortingOrder = 1;
        } else if (lookDirection.x < 0f) {
            playerSprite.flipX = false;
            weapon.sortingOrder = 0;
        }
        if (moveX == 0 && moveY == 0) {
            //Idle
            runningSoundEffect.Stop();
            return;
        } else {
            if (!runningSoundEffect.isPlaying) {
                runningSoundEffect.Play();
            }
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

    public void startDodge(){
        if (FatRolling) {
            dodgeDir = new Vector2(moveX, moveY).normalized;
            dodgeTimer = TimeFatRoll;
            maxDodgeTime = TimeFatRoll;
            dodgeSpeed = maxFatRollSpeed;
            rollAmount = maxFatRollAmount;
            playerHealth.canTakeDamage = false;
            playerHealth.immunityTimer = TimeFatRoll;
            FatRolling = false;
            maxImmunityTime = TimeFatRoll;
            state = State.Dodge;
            rollSoundEffect.Play();
        } else if (slider.value == slider.maxValue) {
            dodgeDir = new Vector2(moveX, moveY).normalized;
            dodgeTimer = TimeDodge;
            maxDodgeTime = TimeDodge;
            dodgeSpeed = maxDodgeSpeed;
            rollAmount = maxRollAmount;
            maxImmunityTime = TimeDodge;
            resetDodgeCooldown();
            state = State.Dodge;
            rollSoundEffect.Play();
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
                direction = playerSprite.flipX ? -1f : 1f;
            }
            else {
                direction = -1f * dodgeDir.x / Mathf.Abs(dodgeDir.x);
            }
            transform.Rotate(new Vector3(0, 0, rollAmount * (Time.deltaTime/maxDodgeTime) * direction));
            playerRB.AddForce(dodgeDir * dodgeSpeed);
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
