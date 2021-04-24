﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton ()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    /*public void SettingsButton()
    {
        SceneManager.LoadScene(3);
    }*/

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
