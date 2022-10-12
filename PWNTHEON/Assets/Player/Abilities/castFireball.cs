using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castFireball : MonoBehaviour
{
    private float fireBallSpeed;
    public GameObject FireBall;
    private float fireballCooldown;
    private Camera mainCam;

    void Start() 
    {
        mainCam = Camera.main;
        fireBallSpeed = 150f;
        fireballCooldown = 0f;
    }
    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if(Input.GetKey(KeyCode.Alpha1) && fireballCooldown <= 0) {
    //         Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
    //         mousePosition.z = 0;
    //         newFireBall(mousePosition, transform.position, fireBallSpeed);
    //         fireballCooldown = 3f;
    //     }
    //     if (fireballCooldown > 0) {
    //         fireballCooldown -= Time.deltaTime;
    //     }
    // }

    void newFireBall(Vector3 mousePos, Vector3 playerPos, float speed) {
        Vector3 direction = (mousePos - playerPos).normalized;
        GameObject fireballObject = Instantiate(FireBall, playerPos + direction, Quaternion.identity);
        Physics2D.IgnoreCollision(fireballObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        Rigidbody2D fireballRB = fireballObject.GetComponent<Rigidbody2D>();
        fireballRB.AddForce(direction * speed);
    }
}
