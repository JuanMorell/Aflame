using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemManipulation : MonoBehaviour
{
    public Vector3 itemPosition;
    public Vector3 itemRotation;
    public float strength;

    bool itemGrabbed = false;
    bool itemThrown = false;
    Rigidbody itemRb;
    CylinderGeodesic cylinderGeodesic;

    void FixedUpdate()
    {
        if (itemThrown)
        {
            itemThrown = false;
            itemRb.AddForce(Vector3.right * strength, ForceMode.Impulse);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.M) && other.gameObject.CompareTag("Item") && !itemGrabbed)
        {
            GrabItem(other.gameObject);
            itemGrabbed = true;
        } else if (Input.GetKeyDown(KeyCode.M) && other.gameObject.CompareTag("Item"))
        {
            ThrowItem(other.gameObject);
        }
    }

    void GrabItem(GameObject item)
    {
        CylinderGeodesic cylinderGeodesic = item.GetComponent<CylinderGeodesic>();
        cylinderGeodesic.enabled = false;
        item.transform.parent = transform;
        item.transform.localPosition = itemPosition;
        item.transform.localRotation = Quaternion.Euler(itemRotation);
    }

    void ThrowItem(GameObject item)
    {
        CylinderGeodesic cylinderGeodesic = item.GetComponent<CylinderGeodesic>();
        cylinderGeodesic.enabled = true;
        itemGrabbed = false;
        item.transform.parent = cylinderGeodesic.worldCenter.transform;
        itemRb = item.GetComponent<Rigidbody>();
        itemRb.constraints = RigidbodyConstraints.None;
        itemThrown = true;
    }
}
