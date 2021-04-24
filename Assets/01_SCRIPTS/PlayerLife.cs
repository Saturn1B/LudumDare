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
        }

        if (life <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
