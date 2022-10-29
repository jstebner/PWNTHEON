using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttackCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<playerHealth>().damagePlayer(10, true);
            Destroy(this.gameObject);
        } else if (col.gameObject.tag == "Ability") {
            Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
        } else {
            Destroy(this.gameObject);
        }
        //Debug.Log("hit " + col.gameObject.name);
    }
}
