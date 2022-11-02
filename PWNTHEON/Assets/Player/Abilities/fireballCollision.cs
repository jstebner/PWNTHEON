using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ability") {
            Debug.Log("Player or ability");
            Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
        } else if (col.gameObject.tag == "Boss") {
            Debug.Log("Boss");
            col.gameObject.GetComponent<BossController>().damageBoss(10);
            Destroy(this.gameObject);
        } else {
            Debug.Log("something elese");
            Destroy(this.gameObject);
        }
    }
}
