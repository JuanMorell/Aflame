using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject victoryPanel;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
        }
    }
}
