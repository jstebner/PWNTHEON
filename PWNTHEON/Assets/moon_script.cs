using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moon_script : MonoBehaviour
{

    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 10000) {speed *= 1.0001f;} // we do a lil trolling    
        transform.Rotate (0,0,speed*Time.deltaTime);
    }
}
