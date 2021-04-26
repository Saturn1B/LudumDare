using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    public float maxValue;
    public Image fill;

    public float value;

    // Start is called before the first frame update
    void Start()
    {
        value = maxValue;
        fill.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Add(10.0f);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Deduct(10.0f);
        }
    }

    public void Add(float i)
    {
        value += i;

        if (value > maxValue)
        {
            value = maxValue;
        }
        fill.fillAmount = value / maxValue;
    }

    public void Deduct(float i)
    {
        value -= i;

        if (value < 0)
        {
            value = 0;
        }
        fill.fillAmount = value / maxValue;
    }
}
