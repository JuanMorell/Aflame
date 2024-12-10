using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distanceFromPlayer;
    //public float maxDistanceDelta;
    public Vector2 cameraFocusOffset;
    public GameObject player;
    public Transform centerPt;

    // Update is called once per frame
    void LateUpdate()
    {
        //LA CÁMARA SIGUE AL JUGADOR A CIERTA DISTANCIA ENTORNO AL CENTRO DEL MUNDO
        Vector3 direction = (player.transform.position - centerPt.position).normalized;
        Vector3 newPos = player.transform.position + (direction * distanceFromPlayer);
        transform.position = new Vector3(newPos.x, newPos.y + cameraFocusOffset.y, newPos.z);
            //Vector3.MoveTowards(transform.position, new Vector3(newPos.x, newPos.y + cameraFocusOffset.y, newPos.z), maxDistanceDelta);

        //LA CÁMARA ENFOCA AL JUGADOR
        Quaternion lookAt = Quaternion.LookRotation(player.transform.position - transform.position);
        Quaternion correction = Quaternion.Euler(cameraFocusOffset.x, 0, 0);
        transform.rotation = lookAt * correction;
    }
}
