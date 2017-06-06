using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;

    //CURSOR
    public CursorMode cursorMode = CursorMode.Auto;
    private bool paused = false;
    public bool pausedEstado = false;

    //ACTUAL SCENE
    Scene ActualScene;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        ActualScene = SceneManager.GetActiveScene();
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                PlayerPrefs.SetInt("pause", 1);
                pausedEstado = true;
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }
            if (!paused)
            {
                PauseUI.SetActive(false);
                PlayerPrefs.SetInt("pause", 0);
                pausedEstado = false;
                Time.timeScale = 1.0f;
                /*Time.fixedDeltaTime = Time.timeScale * 0.02f;*/

            }
        }   
    }

    public void Resume()
    {
        paused = false;
    }
    public void Quit()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(ActualScene.name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
