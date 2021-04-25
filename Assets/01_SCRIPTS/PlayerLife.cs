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

    // Start is called before the first frame update
    void Start()
    {
        HPs = GameObject.Find("HP").transform.GetComponentsInChildren<Image>();
        life = HPs.Length;
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
            StartCoroutine(Sparks(collision.GetContact(0).point));
        }

        if (life <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator Sparks(Vector3 contactPoint)
    {
        GameObject sparks = Instantiate(CollisionSparks, contactPoint, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(sparks);
    }

}

