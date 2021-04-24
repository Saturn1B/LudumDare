using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject Fuel;
    [SerializeField]
    float maxTime;
    [SerializeField]
    float minTime;
    public Transform Bound1;
    public Transform Bound2;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFuel());
        Debug.Log(Camera.main.orthographicSize);
        Debug.Log(Camera.main.rect.width);
        Debug.Log(Camera.main.orthographicSize * Camera.main.rect.width);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnFuel()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        GameObject tank = Instantiate(Fuel, new Vector3(Random.Range(Bound1.position.x, Bound2.position.x), transform.position.y, 0), Quaternion.identity);
        StartCoroutine(SpawnFuel());
    } 
}
