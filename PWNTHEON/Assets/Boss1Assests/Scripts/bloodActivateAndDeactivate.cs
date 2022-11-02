using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodActivateAndDeactivate : MonoBehaviour
{
    public bool bloodIsActive = false;
    public GameObject blood;

    // Update is called once per frame
    void Update()
    {
        blood.SetActive(bloodIsActive);
    }
}
