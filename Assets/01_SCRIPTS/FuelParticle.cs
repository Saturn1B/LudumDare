using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelParticle : MonoBehaviour
{
    public ParticleSystem particle;

    // Update is called once per frame
    void Update()
    {
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
