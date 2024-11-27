using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemManipulation : MonoBehaviour
{
    [HideInInspector]
    public bool allowInteraction = false;
    public float strength;

    bool itemGrabbed = false;
    bool itemThrown = false;
    GameObject item;
    Rigidbody itemRb;
    CylinderGeodesic cylinderGeodesic;

    void FixedUpdate()
    {
        if (itemThrown)
        {
            itemThrown = false;
            itemRb.AddForce(transform.TransformDirection(Vector3.right) * strength, ForceMode.Impulse);
            itemRb.velocity += gameObject.GetComponentInParent<Rigidbody>().velocity;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && allowInteraction)
        {
            if (!itemGrabbed)
                GrabItem();
            else
                ThrowItem();
        }
    }

    void GrabItem()
    {
        itemGrabbed = true;

        cylinderGeodesic = item.GetComponent<CylinderGeodesic>();
        cylinderGeodesic.enabled = false;

        if (item.gameObject.GetComponent<Rigidbody>() != null)
        {
            itemRb = item.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.FreezeAll;
        }

        item.transform.parent = transform;
        item.transform.localPosition = item.GetComponent<GetItemRef>().itemPosition;
        item.transform.localRotation = Quaternion.Euler(item.GetComponent<GetItemRef>().itemRotation);
    }

    void ThrowItem()
    {
        itemGrabbed = false;

        cylinderGeodesic.enabled = true;

        if (item.GetComponent<Rigidbody>() != null)
        {
            itemRb = item.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.None;
        }

        item.transform.parent = cylinderGeodesic.worldCenter.transform;
        itemThrown = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            item = other.gameObject;
            allowInteraction = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            allowInteraction = false;
        }
    }
}
