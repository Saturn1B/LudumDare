using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public AudioSource ObjectImpact;
    public AudioSource Impact;
    public AudioSource Pipe;

    // Start is called before the first frame update
    void Start()
    {
        HPs = GameObject.Find("HP").transform.GetComponentsInChildren<Image>();
        life = HPs.Length;
        shaker = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        shaker.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Fuel")
        {
            HPs[life - 1].enabled = false;
            life -= 1;
            Impact.clip = ImpactClips[Random.Range(0, 2)];
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
        }

        if (life <= 0)
        {
            booster.ResetBooster();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator Sparks(Vector3 contactPoint)
    {
        GameObject sparks = Instantiate(CollisionSparks, contactPoint, Quaternion.identity);
        shaker.enabled = true;
        yield return new WaitForSeconds(1);
        shaker.enabled = false;
        shaker.shakeDuration = 0.5f;
        yield return new WaitForSeconds(1);
        Destroy(sparks);
    }

}

