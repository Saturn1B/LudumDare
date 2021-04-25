using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profondeur : MonoBehaviour
{
    public Text Deepness;
    public float deepnessValue;

    // Start is called before the first frame update
    void Start()
    {
        deepnessValue = transform.position.y;
        Deepness = GameObject.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deepnessValue)
        {
            deepnessValue = transform.position.y;
            Deepness.text = deepnessValue.ToString("#.#");
        }
    }
}
