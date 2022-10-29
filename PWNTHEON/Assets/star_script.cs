using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_script : MonoBehaviour
{

    private float speed = 15.0f;
    private float amount = 0.4f;
    Vector3 startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(startPos.x + Mathf.Sin(Time.time * speed) * amount, startPos.y + Mathf.Cos(Time.time * speed * 1.618f) * amount, startPos.z);
        transform.position = newPos;
    }
}
