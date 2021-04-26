using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool OptionsOn = false;
    public static bool InGame = false;
    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject MenuUI;
    public Booster booster;
    public GameObject startMenu, endMenu;

    private void Awake()
    {
        booster = GameObject.Find("PlayerDrill").GetComponent<Booster>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !startMenu.activeSelf && !endMenu.activeSelf)
        {
            if (!OptionsOn)
            {
                if (GameIsPaused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        booster.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        booster.enabled = false;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        if (InGame == true)
        {
            Time.timeScale = 0f;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);   
    }
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InGame = true;
    }

    public void LoadOptions()
    {
        MenuUI.SetActive(false);
        if (InGame != true)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        OptionsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        OptionsOn = true;
    }

    public void Back()
    {
        Time.timeScale = 1f;
        OptionsMenuUI.SetActive(false);
        OptionsOn = false;
            MenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Waiting For Quit ...");
        Application.Quit();
    }
}
