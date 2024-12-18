using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Victory : MonoBehaviour
{
    public GameObject savingSystem;
    public GameObject victoryPanel;
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);

            SaveCurrentLevel currentLevel = new SaveCurrentLevel(gameManager.currentLevel+1);

            JObject levelDataJson = JObject.FromObject(currentLevel);

            savingSystem.GetComponent<SaveGame>().SaveFileAsJSon("CurrentLevel",
                levelDataJson);
        }
    }
}
