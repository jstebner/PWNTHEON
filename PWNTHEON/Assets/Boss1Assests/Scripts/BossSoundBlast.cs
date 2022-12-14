using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundBlast : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        bool behindPillar = false;
        if (col.gameObject.tag == "Player") {
            RaycastHit2D[] hits = Physics2D.RaycastAll(col.gameObject.transform.position, (GameObject.Find("Greuhl").transform.position - col.gameObject.transform.position).normalized);
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].collider.name == "PillarBase1" || hits[i].collider.name == "PillarBase2" || hits[i].collider.name == "PillarBase3" || hits[i].collider.name == "PillarBase4") {
                    behindPillar = true;
                }
            }
            if (!behindPillar) {
                col.gameObject.GetComponent<playerHealth>().damagePlayer(25, true);
                Physics2D.IgnoreCollision(col, this.GetComponent<Collider2D>());
                behindPillar = false;
            } else {
                Debug.Log("sound blast dodged by being behind a pillar");
            }
        } 
    }
}
