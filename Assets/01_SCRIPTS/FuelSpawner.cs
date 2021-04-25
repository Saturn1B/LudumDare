using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject Fuel;
    public GameObject Warning;
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
    }

    IEnumerator SpawnFuel()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        Vector3 spawnPos = new Vector3(Random.Range(Bound1.position.x, Bound2.position.x), transform.position.y, 0);
        GameObject warn = Instantiate(Warning, new Vector3(spawnPos.x, spawnPos.y - 4, spawnPos.z), Quaternion.identity);
        warn.transform.parent = gameObject.transform.parent;
        GameObject tank = Instantiate(Fuel, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(warn);
        StartCoroutine(SpawnFuel());
    } 
}
