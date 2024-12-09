using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    float fireLitCountdown = 0.5f;
    public float burnCountdown = 1;
    public bool fireActive = false;
    public bool canBurn = true;

    bool fireEnabled = true;
    GameObject fireParent;

    void Start()
    {
        fireParent = transform.parent.gameObject;
    }

    void Update()
    {
        if (fireLitCountdown <= 0 && fireEnabled)
        {
            fireEnabled = false;
            GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<FireBehavior>().fireActive = true;
        }

        if (fireActive && canBurn)
        {
            burnCountdown -= Time.deltaTime;

            if (burnCountdown <= 0)
                Destroy(fireParent);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Condition(other))
        {
            fireLitCountdown -= Time.deltaTime;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (Condition(other))
        {
            fireLitCountdown = 0;
        }
    }

    bool Condition(Collider other)
    {
        return 
            gameObject.CompareTag("Fire") &&
            other.CompareTag("Fire") &&
            !fireActive &&
            other.GetComponent<FireBehavior>().fireActive;
    }
}