using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torch_script_1 : MonoBehaviour
{

    private float speed = 60.0f;
    private float amount = 1.0f;
    Vector3 startPos;
    Vector3 newPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos[0] = startPos.x + Mathf.Sin(Time.time * speed) * amount * 0.85f;
        newPos[1] = startPos.y + Mathf.Cos(Time.time * speed * 0.618f) * amount;
        newPos[2] = startPos.z;
        transform.position = newPos;
    }
}
