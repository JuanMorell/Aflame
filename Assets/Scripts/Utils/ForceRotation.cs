using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRotation : MonoBehaviour
{
    public float strength;

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddTorque(new Vector3(0, strength, 0));
    }
}
