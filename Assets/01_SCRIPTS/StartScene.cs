using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScene : MonoBehaviour
{
    public GameObject HUDPanel, StartPanel;
    public Rigidbody _rb;
    public Booster booster;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GameObject.Find("PlayerDrill").GetComponent<Rigidbody>();
        booster = GameObject.Find("PlayerDrill").GetComponent<Booster>();
        booster.enabled = false;
        _rb.useGravity = false;
        StartCoroutine(Counter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Counter()
    {
        int timer = 5;
        StartPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timer.ToString("#");
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1);
            StartPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timer.ToString("#");
            timer -= 1;
        }
        StartPanel.SetActive(false);
        HUDPanel.SetActive(true);
        _rb.useGravity = true;
        booster.enabled = true;
    }
}
