using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    string currentSceneName;
    void Start()
    {
        Time.timeScale = 1;
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }
}
