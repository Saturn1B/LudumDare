using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    public Rigidbody _rb;
    public Transform _transform;
    public KeyCode leftBoost;
    public KeyCode rightBoost;
    public Slider fuelValue;
    public float forceUp;
    public float forceSide;
    public float decreaseRate;
    public GameObject[] BoosterLights;



    // Start is called before the first frame update
    void Start()
    {
        fuelValue = GameObject.Find("Slider").GetComponent<Slider>();
        foreach(GameObject booster in BoosterLights)
        {
            booster.GetComponent<Light>().color = Color.yellow;
            booster.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_rb.velocity.magnitude);
        float param = Mathf.InverseLerp(0, 35, _rb.velocity.magnitude);
        foreach (GameObject booster in BoosterLights)
        {
            booster.GetComponent<Light>().color = Color.Lerp(Color.yellow, Color.red, param);
        }


        if (Input.GetKey(leftBoost) && fuelValue.value > 0)
        {
            _rb.AddForce(transform.right * forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * -0.1f;
            fuelValue.value -= decreaseRate;
            BoosterLights[0].SetActive(true);
        }
        else
        {
            BoosterLights[0].SetActive(false);
        }

        if (Input.GetKey(rightBoost) && fuelValue.value > 0)
        {
            _rb.AddForce(transform.right * -forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * 0.1f;
            fuelValue.value -= decreaseRate;
            BoosterLights[1].SetActive(true);
        }
        else
        {
            BoosterLights[1].SetActive(false);
        }
    }


}
