using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhysicalSlam : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<playerHealth>().damagePlayer(20, false);
            Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
        } 
    }
}
