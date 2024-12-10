using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGravity : MonoBehaviour
{
    public float gravity;

    void FixedUpdate()
    { 
        if (GetComponent<Rigidbody>().velocity.y <= 0)
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -gravity), ForceMode.Acceleration);
    }
}
