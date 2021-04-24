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

    private void Start()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
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

}
