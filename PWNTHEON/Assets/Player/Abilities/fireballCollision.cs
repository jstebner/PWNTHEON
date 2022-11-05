using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Ability") {
            Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
        } else if (col.gameObject.tag == "Boss") {
            Debug.Log("Boss");
            col.gameObject.GetComponent<BossController>().damageBoss(3);
            Destroy(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}
