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

    // Use this for initialization
    void Start()
    {

        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            PlayerPrefs.SetInt("pause", 1);
            PauseUI.SetActive(true);
            Time.timeScale = 0;           
        }
        if(!paused)
        {
            PauseUI.SetActive(false);
            PlayerPrefs.SetInt("pause", 0);
            
        }
    }

    public void Resume()
    {
        paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
