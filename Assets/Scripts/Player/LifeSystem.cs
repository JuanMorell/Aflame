using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public float secondsToDeath = 2;
    public Image lifeGauge;
    public GameObject gameOverPanel;
    public RainDetection rainDetection;

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
        timer = rainDetection.underRain ? timer - Time.deltaTime : secondsToDeath;
        lifeGauge.fillAmount = timer/secondsToDeath;

        //SE ACABA LA PARTIDA SI SE VACÍA EL MEDIDOR DE VIDA
        if(lifeGauge.fillAmount == 0)
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Die();
        }

    }

    void Die()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
