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
        if (itemThrown && Input.GetKey(KeyCode.UpArrow))
        {
            ItemThrown(Vector3.up);
        } else if (itemThrown)
        {
            ItemThrown(Vector3.right);
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
        item.transform.parent = null;
        //item.transform.parent = cylinderGeodesic.worldCenter.transform;
        itemThrown = true;
    }

    void ItemThrown (Vector3 throwDirection)
    {
        itemThrown = false;
        itemRb.AddForce(transform.TransformDirection(throwDirection) * strength, ForceMode.Impulse);
        itemRb.velocity += gameObject.GetComponentInParent<Rigidbody>().velocity;
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
