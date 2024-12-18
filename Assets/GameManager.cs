using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevel;
    public GameObject pausePanel;

    string currentSceneName;
    bool gamePaused;

    void Start()
    {
        Time.timeScale = 1;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            gamePaused = !gamePaused;
            PauseContinue(gamePaused);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void PauseContinue(bool pausing)
    {
        Time.timeScale = pausing ? 0 : 1;
        pausePanel.SetActive(pausing);
    }
}
