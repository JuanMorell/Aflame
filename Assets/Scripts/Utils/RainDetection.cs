using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDetection : MonoBehaviour
{
    public Transform rainSystem;
    [HideInInspector]
    public bool underRain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CREAMOS UN RAYCAST PARA DETECTAR LA LLUVIA
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.up);
        Physics.Raycast(ray, out hit);

        //VACIAMOS EL MEDIDOR DE VIDA SI ESTAMOS BAJO LA LLUVIA
        underRain = hit.transform == rainSystem;
    }
}
