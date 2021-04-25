using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserTransform;
    public Vector3 contactPos;
    public GameObject hitObject;
    float param = 0;
    public Color redColor;
    public ParticleSystem LaserBeam;
    public AudioSource BeamEndSound;
    public AudioSource BreakBlock;
    public bool canPlay;

    private void Start()
    {
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(laserTransform.position, laserTransform.up * -15, out RaycastHit hit))
        {
            //hit.point
            Debug.DrawRay(laserTransform.position, laserTransform.up * -15, Color.red, 0.5f);
            contactPos = hit.point;
            float size = Mathf.Clamp(Vector3.Distance(laserTransform.position, contactPos), 0, 15);
            laserTransform.localScale = new Vector3(laserTransform.localScale.x, size, laserTransform.localScale.z);

            if(Vector3.Distance(laserTransform.position, contactPos) <= 15)
            {
                if (canPlay)
                {
                    LaserBeam.Play();
                    BeamEndSound.Play();
                }

                hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<DestructibleObject>())
                {
                    if(hitObject.GetComponent<DestructibleObject>().laser == null)
                    {
                        hitObject.GetComponent<DestructibleObject>().laser = this;
                    }
                    hit.transform.GetComponent<DestructibleObject>().HP -= 0.1f;
                    if (hit.transform.GetComponent<DestructibleObject>().param1 >= -0.8)
                    {
                        canPlay = false;
                        hit.transform.GetComponent<Collider>().enabled = false;
                        BreakBlock.Play();
                        LaserBeam.Stop();
                        BeamEndSound.Stop();
                    }
                    param += 0.004f;
                    param = Mathf.Clamp(param, 0, 1);
                    hit.transform.GetComponent<Renderer>().material.color = Color.Lerp(hit.transform.GetComponent<DestructibleObject>().startColor, redColor, param);
                }
            }
            else
            {
                if (hitObject != null)
                {
                    if (hitObject.GetComponent<DestructibleObject>())
                    {
                        hitObject.GetComponent<DestructibleObject>().regenProps = true;
                        hitObject.GetComponent<DestructibleObject>().destroyColor = hitObject.GetComponent<Renderer>().material.color;
                        param = 0;
                    }
                    hitObject = null;
                    BeamEndSound.Stop();
                    LaserBeam.Stop();
                    LaserBeam.Clear();
                }
            }
        }
        else
        {
            laserTransform.localScale = new Vector3(laserTransform.localScale.x, 15, laserTransform.localScale.z);
        }

    }
}
