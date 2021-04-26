using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public float HP;
    public Color startColor;
    public bool regenProps;
    public Color destroyColor;
    public float param = 0;
    public float param1 = -1;
    public Laser laser;

    private void Start()
    {
        if(transform.tag == "Fungus")
        {
            startColor = gameObject.transform.GetChild(1).GetComponent<Renderer>().material.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
        }
        else
        {
            startColor = GetComponent<Renderer>().material.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
        }

        if (transform.tag == "Fungus")
        {
            gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
            gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            StartCoroutine(DestroyObject());
        }

        if (regenProps)
        {
            if (transform.tag == "Fungus")
            {
                gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(destroyColor, startColor, param));
                gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(destroyColor, startColor, param));
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(destroyColor, startColor, param));
            }
            param += 0.004f;
            param = Mathf.Clamp(param, 0, 1);
            if(param >= 1)
            {
                regenProps = false;
                param = 0;
                if (transform.tag == "Fungus")
                {
                    gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", startColor);
                    gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", startColor);
                }
                else
                {
                    GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", startColor);
                }
            }
        }
    }

    IEnumerator DestroyObject()
    {
        while(param1 < 1)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            if (transform.tag == "Fungus")
            {
                gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
                gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_664C595E", param1);
            }
            param1 += 0.002f;
        }
        laser.canPlay = true;
        Destroy(gameObject);
    }

}
