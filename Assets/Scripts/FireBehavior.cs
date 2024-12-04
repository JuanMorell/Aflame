using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public bool fireActive = false;
    bool fireEnabled = true;
    public float fireLitCountdown = 1;
    public float burnCountdown = 2;
    GameObject fireTriggerParent;
    void Start()
    {
        fireTriggerParent = transform.parent.gameObject;
    }
    void Update()
    {
        if (fireLitCountdown <= 0 && fireEnabled)
        {
            fireEnabled = false;
            GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<FireBehavior>().fireActive = true;
        }
        if (fireActive && !fireTriggerParent.CompareTag("Player"))
        {
            burnCountdown -= Time.deltaTime;
            if (burnCountdown <= 0)
            {
                Destroy(fireTriggerParent);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (
            gameObject.CompareTag("Fire") &&
            other.CompareTag("Fire") &&
            !fireActive &&
            other.GetComponent<FireBehavior>().fireActive
        )
        {
            fireLitCountdown -= Time.deltaTime;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (
            gameObject.CompareTag("Fire") &&
            other.CompareTag("Fire") &&
            !fireActive &&
            other.GetComponent<FireBehavior>().fireActive
        )
        {
            fireLitCountdown = 0;
        }
    }
}