using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CylinderGeodesic : MonoBehaviour
{
    public Transform centerPt;
    public float distance = 206;

    void Update()
    {
        Vector3 vector = new Vector3(centerPt.position.x, transform.position.y, centerPt.position.z);
        transform.position = (transform.position - vector).normalized * distance + vector;

        transform.LookAt(vector);
    }
}