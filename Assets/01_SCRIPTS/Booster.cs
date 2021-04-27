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
    public MySlider fuelValue;
    public float forceUp;
    public float forceSide;
    public float decreaseRate;
    public GameObject[] BoosterLights;
    public ParticleSystem[] BoosterFlame;
    public AudioSource[] Engage;
    public AudioSource[] Loop;
    public AudioSource[] End;
    public AudioSource Warning;
    public bool canMove;
    float warningTimer;
    bool belowLevel;
    public Animator animator;
    public GameObject orangeCircle, redCircle, whiteBarrel;
    bool goRight;
    bool goLeft;

    private void Awake()
    {
        fuelValue = GameObject.Find("FuelSlider").GetComponent<MySlider>();
        orangeCircle = GameObject.Find("Circle2");
        redCircle = GameObject.Find("Circle3");
        whiteBarrel = GameObject.Find("Fuel2");
        orangeCircle.SetActive(false);
        redCircle.SetActive(false);
        whiteBarrel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        ResetBooster();
    }

    // Update is called once per frame
    void Update()
    {
        if(fuelValue.value < 40 && fuelValue.value >= 30)
        {
            if(!belowLevel)
            {
                redCircle.SetActive(true);
                whiteBarrel.SetActive(true);
                StartCoroutine(FuelWarning());
                belowLevel = true;
            }
            warningTimer = 1.5f;
        }
        else if(fuelValue.value >= 20)
        {
            warningTimer = 1.2f;
        }
        else if(fuelValue.value >= 10)
        {
            warningTimer = 0.9f;
        }
        else { warningTimer = 0.5f; }

        if(fuelValue.value >= 40)
        {
            if (belowLevel)
            {
                orangeCircle.SetActive(false);
                redCircle.SetActive(false);
                whiteBarrel.SetActive(false);
                StopAllCoroutines();
                belowLevel = false;
            }
        }

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
            goLeft = true;
            animator.SetBool("Left", true);
            StartCoroutine(StartFlameSound(0));
        }
        if (Input.GetKey(leftBoost) && fuelValue.value > 0 && canMove)
        {

            _transform.localEulerAngles += Vector3.forward * -0.1f;
            fuelValue.Deduct(decreaseRate * Time.deltaTime * 100);
            BoosterLights[0].SetActive(true);
            if (!BoosterFlame[0].isPlaying)
            {
                BoosterFlame[0].Play();
            }
        }
        else if((Input.GetKeyUp(leftBoost) || fuelValue.value <= 0) && canMove)
        {
            goLeft = false;
            animator.SetBool("Left", false);
            BoosterLights[0].SetActive(false);
            BoosterFlame[0].Stop();
            Loop[0].Stop();
            End[0].Play();
        }

        if (Input.GetKeyDown(rightBoost) && fuelValue.value > 0 && canMove)
        {
            goRight = true;
            animator.SetBool("Right", true);
            StartCoroutine(StartFlameSound(1));
        }
        if (Input.GetKey(rightBoost) && fuelValue.value > 0 && canMove)
        {

            _transform.localEulerAngles += Vector3.forward * 0.1f;
            fuelValue.Deduct(decreaseRate * Time.deltaTime * 100);
            BoosterLights[1].SetActive(true);
            if (!BoosterFlame[1].isPlaying)
            {
                BoosterFlame[1].Play();
            }
        }
        else if ((Input.GetKeyUp(rightBoost) || fuelValue.value <= 0) && canMove)
        {
            goRight = false;
            animator.SetBool("Right", false);
            BoosterLights[1].SetActive(false);
            BoosterFlame[1].Stop();
            Loop[1].Stop();
            End[1].Play();
        }
    }

    private void FixedUpdate()
    {
        if (goLeft)
        {
            _rb.AddForce(transform.right * forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
        }
        if (goRight)
        {
            _rb.AddForce(transform.right * -forceSide, ForceMode.Force);
            _rb.AddForce(transform.up * forceUp, ForceMode.Force);
        }
    }

    IEnumerator StartFlameSound(int index)
    {
        Engage[index].Play();
        yield return new WaitForSeconds(0.1f);
        Loop[index].Play();
    }

    IEnumerator FuelWarning()
    {
        yield return new WaitForSeconds(warningTimer / 2);
        if (redCircle.activeSelf)
        {
            redCircle.SetActive(false);
            orangeCircle.SetActive(true);
        }
        else
        {
            redCircle.SetActive(true);
            orangeCircle.SetActive(false);
        }
        yield return new WaitForSeconds(warningTimer/2);
        if (redCircle.activeSelf)
        {
            redCircle.SetActive(false);
            orangeCircle.SetActive(true);
        }
        else
        {
            redCircle.SetActive(true);
            orangeCircle.SetActive(false);
        }
        Warning.Play();
        StartCoroutine(FuelWarning());
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
