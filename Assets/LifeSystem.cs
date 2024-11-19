using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public float secondsToDeath = 2;
    public Image lifeGauge;
    float timer = 2;
    bool underRain;
    LayerMask layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("Sky");
    }

    // Update is called once per frame
    void Update()
    {
        //CREAMOS UN RAYCAST PARA DETECTAR LA LLUVIA
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.up);

        underRain = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        timer = underRain ? timer - Time.deltaTime : secondsToDeath;

        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red);

        lifeGauge.fillAmount = timer/secondsToDeath;

        print(timer);
    }
}
