//using System.Collections;
//using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
//using UnityEngine;

//public class ItemManipulation : MonoBehaviour
//{
//    public Vector3 itemPosition;
//    public Vector3 itemRotation;
//    public float strength;

//    bool itemGrabbed = false;
//    bool itemThrown = false;
//    bool allowInteraction = false;
//    Rigidbody itemRb;
//    CylinderGeodesic cylinderGeodesic;

//    void FixedUpdate()
//    {
//        if (itemThrown)
//        {
//            itemThrown = false;
//            itemRb.AddForce(Vector3.right * strength, ForceMode.Impulse);
//        }
//    }

//    void Update() 
//    {
//        if (Input.GetKeyDown(KeyCode.M)) { 
//            allowInteraction = true;
//        }
//    }

//    void OnTriggerStay(Collider other)
//    {
//        if (allowInteraction && other.gameObject.CompareTag("Item") && !itemGrabbed)
//        {
//            GrabItem(other.gameObject);
//            itemGrabbed = true;
//            allowInteraction = false;
//        } else if (Input.GetKeyDown(KeyCode.M) && other.gameObject.CompareTag("Item"))
//        {
//            ThrowItem(other.gameObject);
//        }
//    }

//    void GrabItem(GameObject item)
//    {
//        CylinderGeodesic cylinderGeodesic = item.GetComponent<CylinderGeodesic>();
//        cylinderGeodesic.enabled = false;
//        itemRb = item.GetComponent<Rigidbody>();
//        itemRb.constraints = RigidbodyConstraints.FreezeAll;
//        item.transform.parent = transform;
//        item.transform.localPosition = itemPosition;
//        item.transform.localRotation = Quaternion.Euler(itemRotation);
//    }

//    void ThrowItem(GameObject item)
//    {
//        CylinderGeodesic cylinderGeodesic = item.GetComponent<CylinderGeodesic>();
//        cylinderGeodesic.enabled = true;
//        itemGrabbed = false;
//        itemRb = item.GetComponent<Rigidbody>();
//        itemRb.constraints = RigidbodyConstraints.None;
//        item.transform.parent = cylinderGeodesic.worldCenter.transform;
//        itemThrown = true;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemManipulation : MonoBehaviour
{
    public Vector3 itemPosition;
    public Vector3 itemRotation;
    public float strength;
    public GameObject hand;

    bool itemGrabbed = false;
    [HideInInspector]
    public bool allowInteraction = false;
    bool itemThrown = false;
    Rigidbody itemRb;
    CylinderGeodesic cylinderGeodesic;

    void FixedUpdate()
    {
        if (itemThrown)
        {
            itemThrown = false;
            itemRb.AddForce(hand.transform.TransformDirection(Vector3.right) * strength, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && allowInteraction)
        {
            print("M");
            if (!itemGrabbed)
                GrabItem();
            else
                ThrowItem();
        }
    }

    void GrabItem()
    {
        itemGrabbed = true;

        if (gameObject.GetComponent<CylinderGeodesic>()!=null)
            gameObject.GetComponent<CylinderGeodesic>().enabled = false;

        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            itemRb = gameObject.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.FreezeAll;
        }

        gameObject.transform.parent = hand.transform;
        gameObject.transform.localPosition = itemPosition;
        gameObject.transform.localRotation = Quaternion.Euler(itemRotation);
    }

    void ThrowItem()
    {
        itemGrabbed = false;

        if (gameObject.GetComponent<CylinderGeodesic>() != null)
            gameObject.GetComponent<CylinderGeodesic>().enabled = true;

        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            itemRb = gameObject.GetComponent<Rigidbody>();
            itemRb.constraints = RigidbodyConstraints.None;
        }

        gameObject.transform.parent = cylinderGeodesic.worldCenter.transform;
        itemThrown = true;
    }
}
