using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CylinderGeodesic : MonoBehaviour
{
    public Transform worldCenter;
    public float distance = 206;

    void Update()
    {
        Vector3 vector = new Vector3(worldCenter.position.x, transform.position.y, worldCenter.position.z);
        transform.position = (transform.position - vector).normalized * distance + vector;

        transform.LookAt(vector);
    }
}