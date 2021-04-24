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



    // Start is called before the first frame update
    void Start()
    {
        fuelValue = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftBoost) && fuelValue.value > 0)
        {
            _rb.AddForce(transform.right * forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * -0.1f;
            fuelValue.value -= 0.001f;
        }

        if (Input.GetKey(rightBoost) && fuelValue.value > 0)
        {
            _rb.AddForce(transform.right * -forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * 0.1f;
            fuelValue.value -= 0.001f;
        }

    }


}
