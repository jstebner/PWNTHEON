using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            Physics2D.IgnoreCollision(col.collider, col.otherCollider);
        } else {
            Destroy(this.gameObject);
        }
    }
}
