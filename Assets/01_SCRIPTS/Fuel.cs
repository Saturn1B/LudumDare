using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    public float dispawnTime;
    public Slider fuelValue;
    public float fuelAdd;
    public GameObject AddParticle;

    private void Awake()
    {
        fuelValue = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dispawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Dispawn()
    {
        yield return new WaitForSeconds(dispawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Reservoir")
        {
            GameObject particle = Instantiate(AddParticle, collision.GetContact(0).point, Quaternion.identity);
            fuelValue.value += fuelAdd;
            Destroy(gameObject);
        }
    }
}
