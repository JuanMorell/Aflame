using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GetItemRef : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            print(other.gameObject.name);
            other.gameObject.GetComponent<ItemManipulation>().allowInteraction = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            print(other.gameObject.name);
            other.gameObject.GetComponent<ItemManipulation>().allowInteraction = false;
        }
    }
}
