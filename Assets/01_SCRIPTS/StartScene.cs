using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScene : MonoBehaviour
{
    public GameObject HUDPanel, StartPanel;
    public AudioSource bip;
    public AudioClip boop;
    public Rigidbody _rb;
    public Booster booster;
    public GameObject fuelSpawner;
    // Start is called before the first frame update
    void Start()
    {
        //Screen.SetResolution(622, 810, FullScreenMode.Windowed);
        _rb = GameObject.Find("PlayerDrill").GetComponent<Rigidbody>();
        booster = GameObject.Find("PlayerDrill").GetComponent<Booster>();
        fuelSpawner = GameObject.Find("FuelSpawner");
        fuelSpawner.SetActive(false);
        booster.enabled = false;
        _rb.useGravity = false;
        StartCoroutine(Counter());
    }

    IEnumerator Counter()
    {
        int timer = 5;
        StartPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timer.ToString();
        timer -= 1;
        while (timer >= -1)
        {
            yield return new WaitForSeconds(1);
            if(timer > 0)
            {
                bip.Play();
            }
            else
            {
                bip.clip = boop;
                bip.Play();
            }
            StartPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timer.ToString();
            timer -= 1;
        }
        StartPanel.SetActive(false);
        HUDPanel.SetActive(true);
        fuelSpawner.SetActive(true);
        fuelSpawner.GetComponent<FuelSpawner>().StartCoroutine(fuelSpawner.GetComponent<FuelSpawner>().SpawnFuel());
        _rb.useGravity = true;
        booster.enabled = true;
    }
}
