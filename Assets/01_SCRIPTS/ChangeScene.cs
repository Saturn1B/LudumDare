using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public GameObject Instruction;

    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(622, 810, FullScreenMode.Windowed);
    }

    public void PlayButton ()
    {
        StartCoroutine(StartGame());
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

    IEnumerator StartGame()
    {
        Instruction.SetActive(true);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
