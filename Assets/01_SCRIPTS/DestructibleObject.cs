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
        if (GetComponent<Renderer>().sharedMaterial.HasProperty("Color_31be6d288cfa48f3808d0fed40841607"))
        {
            Debug.Log("enfiguralimificulé");
            startColor = GetComponent<Renderer>().sharedMaterial.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
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
            GetComponent<Renderer>().material.color = Color.Lerp(destroyColor, startColor, param);
            param += 0.004f;
            param = Mathf.Clamp(param, 0, 1);
            if(param >= 1)
            {
                regenProps = false;
                param = 0;
                GetComponent<Renderer>().material.color = startColor;
            }
        }
    }

    IEnumerator DestroyObject()
    {
        while(gameObject.GetComponent<Renderer>().sharedMaterial.GetFloat("Vector1_664C595E") < 1)
        {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("Vector1_664C595E", param1);
            param1 += 0.002f;
        }
        laser.canPlay = true;
        Destroy(gameObject);
    }

}
