using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    public Image[] HPs;
    int life;
    public Rigidbody _rb;
    public GameObject CollisionSparks;
    public CameraShake shaker;
    public Booster booster;
    public AudioClip[] ImpactClips;
    public AudioClip[] FungusImpactClips;
    public AudioClip[] RockImpacts;
    public AudioSource ObjectImpact;
    public AudioSource Impact;
    public AudioSource Pipe;
    public GameObject HUDPanel, EndPanel;
    public Profondeur profondeur;
    public GameObject Laser, FuelSpawner;
    public MySlider fuelValue;

    private void Awake()
    {
        fuelValue = GameObject.Find("FuelSlider").GetComponent<MySlider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HPs = GameObject.Find("HP").transform.GetComponentsInChildren<Image>();
        life = HPs.Length;
        shaker = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        shaker.enabled = false;
        HUDPanel = GameObject.Find("HUD");
        EndPanel = GameObject.Find("EndGame");
        HUDPanel.SetActive(false);
        EndPanel.SetActive(false);
        FuelSpawner = GameObject.Find("FuelSpawner");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_rb.velocity.magnitude);
        if(fuelValue.value <= 0)
        {
            StartCoroutine(Die(1));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Fuel")
        {
            HPs[life - 1].enabled = false;
            life -= 1;
            Impact.clip = ImpactClips[Random.Range(0, ImpactClips.Length)];
            Impact.Play();
            int r = Random.Range(0, 3);
            if(r == 0)
            {
                Pipe.Play();
            }
            StartCoroutine(Sparks(collision.GetContact(0).point));
            if(collision.transform.tag == "Fungus")
            {
                int r2 = Random.Range(0, 5);
                if(r2 == 0){ ObjectImpact.clip = FungusImpactClips[1]; }
                else { ObjectImpact.clip = FungusImpactClips[0]; }
                ObjectImpact.Play();
                Destroy(collision.transform.gameObject);
            }
            if (collision.transform.tag == "Rock")
            {
                ObjectImpact.clip = RockImpacts[Random.Range(0, RockImpacts.Length)];
                ObjectImpact.Play();
            }
        }

        if (life <= 0)
        {
            StartCoroutine(Die(0));
        }
    }

    IEnumerator Sparks(Vector3 contactPoint)
    {
        GameObject sparks = Instantiate(CollisionSparks, contactPoint, Quaternion.identity);
        shaker.enabled = true;
        yield return new WaitForSeconds(1);
        shaker.enabled = false;
        shaker.shakeDuration = 0.25f;
        yield return new WaitForSeconds(1);
        Destroy(sparks);
    }

    IEnumerator Die(int time)
    {
        yield return new WaitForSeconds(time);
        HUDPanel.SetActive(false);
        EndPanel.SetActive(true);
        booster.canMove = false;
        Laser.SetActive(false);
        FuelSpawner.SetActive(false);
        shaker.enabled = false;
        EndPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "SCORE" + '\n' + profondeur.deepnessValue.ToString("#.#");
        Time.timeScale = 0;
        booster.ResetBooster();
    }
}

