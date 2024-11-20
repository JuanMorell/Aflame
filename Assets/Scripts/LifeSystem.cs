using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public float secondsToDeath = 2;
    public Transform rainSystem;
    public Image lifeGauge;
    public GameObject gameOverPanel;

    float timer = 2;
    bool underRain;
    LayerMask layerMask;

    void Start()
    {
        layerMask = LayerMask.GetMask("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        //CREAMOS UN RAYCAST PARA DETECTAR LA LLUVIA
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.up);
        Physics.Raycast(ray, out hit);

        //VACIAMOS EL MEDIDOR DE VIDA SI ESTAMOS BAJO LA LLUVIA
        underRain = hit.transform==rainSystem;
        timer = underRain ? timer - Time.deltaTime : secondsToDeath;
        lifeGauge.fillAmount = timer/secondsToDeath;

        //SE ACABA LA PARTIDA SI SE VACÍA EL MEDIDOR DE VIDA
        if(lifeGauge.fillAmount == 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
