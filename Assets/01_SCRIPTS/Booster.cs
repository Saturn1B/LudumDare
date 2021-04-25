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
    public ParticleSystem[] BoosterFlame;
    public AudioSource[] Engage;
    public AudioSource[] Loop;
    public AudioSource[] End;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        fuelValue = GameObject.Find("Slider").GetComponent<Slider>();
        canMove = true;
        ResetBooster();
    }

    // Update is called once per frame
    void Update()
    {
        float param = Mathf.InverseLerp(0, 15, _rb.velocity.magnitude);
        foreach (GameObject booster in BoosterLights)
        {
            booster.GetComponent<Light>().color = Color.Lerp(Color.yellow, Color.blue, param);
        }
        foreach (ParticleSystem booster in BoosterFlame)
        {
            //booster.GetComponent<Light>().color = Color.yellow;
            booster.startColor = Color.Lerp(Color.yellow, Color.blue, param);
        }

        if(Input.GetKeyDown(leftBoost) && fuelValue.value > 0 && canMove)
        {
            StartCoroutine(StartFlameSound(0));
        }
        if (Input.GetKey(leftBoost) && fuelValue.value > 0 && canMove)
        {
            _rb.AddForce(transform.right * forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * -0.1f;
            fuelValue.value -= decreaseRate;
            BoosterLights[0].SetActive(true);
            if (!BoosterFlame[0].isPlaying)
            {
                BoosterFlame[0].Play();
            }
        }
        else if((Input.GetKeyUp(leftBoost) || fuelValue.value <= 0) && canMove)
        {
            BoosterLights[0].SetActive(false);
            BoosterFlame[0].Stop();
            Loop[0].Stop();
            End[0].Play();
        }

        if (Input.GetKeyDown(rightBoost) && fuelValue.value > 0 && canMove)
        {
            StartCoroutine(StartFlameSound(1));
        }
        if (Input.GetKey(rightBoost) && fuelValue.value > 0 && canMove)
        {
            _rb.AddForce(transform.right * -forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
            _transform.localEulerAngles += Vector3.forward * 0.1f;
            fuelValue.value -= decreaseRate;
            BoosterLights[1].SetActive(true);
            if (!BoosterFlame[1].isPlaying)
            {
                BoosterFlame[1].Play();
            }
        }
        else if ((Input.GetKeyUp(rightBoost) || fuelValue.value <= 0) && canMove)
        {
            BoosterLights[1].SetActive(false);
            BoosterFlame[1].Stop();
            Loop[1].Stop();
            End[1].Play();
        }
    }

    IEnumerator StartFlameSound(int index)
    {
        Engage[index].Play();
        yield return new WaitForSeconds(0.1f);
        Loop[index].Play();
    }

    public void ResetBooster()
    {
        foreach (GameObject booster in BoosterLights)
        {
            booster.GetComponent<Light>().color = Color.yellow;
            booster.SetActive(false);
        }
        foreach (ParticleSystem booster in BoosterFlame)
        {
            booster.Stop();
        }
        foreach (AudioSource booster in Engage)
        {
            booster.Stop();
        }
        foreach (AudioSource booster in Loop)
        {
            booster.Stop();
        }
        foreach (AudioSource booster in End)
        {
            booster.Stop();
        }
    }
}
