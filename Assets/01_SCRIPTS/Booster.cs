using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public Rigidbody _rb;
    public Transform _transform;
    public KeyCode leftBoost;
    public KeyCode rightBoost;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftBoost))
        {
            _rb.AddForce(transform.right * 0.15f, ForceMode.Force);
            _rb.AddForce(transform.up * 1.5f, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * -0.1f;
        }

        if (Input.GetKey(rightBoost))
        {
            _rb.AddForce(transform.right * -0.15f, ForceMode.Force);
            _rb.AddForce(transform.up * 1.5f, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * 0.1f;
        }
    }


}
