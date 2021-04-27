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
    public AudioClip BlockImpactClip;
    public AudioClip[] FungusImpactClips;
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
                    hit.transform.GetComponent<DestructibleObject>().HP -= 0.1f * Time.deltaTime * 100;
                    if (hit.transform.GetComponent<DestructibleObject>().param1 >= -0.8)
                    {
                        canPlay = false;
                        hit.transform.GetComponent<Collider>().enabled = false;
                        if(hit.transform.tag == "Fungus")
                        {
                            int r2 = Random.Range(0, 5);
                            if (r2 == 0) { BreakBlock.clip = FungusImpactClips[1]; }
                            else { BreakBlock.clip = FungusImpactClips[0]; }
                        }
                        else { BreakBlock.clip = BlockImpactClip; }
                        BreakBlock.Play();
                        LaserBeam.Stop();
                        BeamEndSound.Stop();
                    }
                    param += 0.004f;
                    param = Mathf.Clamp(param, 0, 1);
                    if(hit.transform.tag == "Fungus")
                    {
                        hit.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(hit.transform.GetComponent<DestructibleObject>().startColor, redColor, param));
                        hit.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(hit.transform.GetComponent<DestructibleObject>().startColor, redColor, param));
                    }
                    else
                    {
                        hit.transform.GetComponent<Renderer>().material.SetColor("Color_31be6d288cfa48f3808d0fed40841607", Color.Lerp(hit.transform.GetComponent<DestructibleObject>().startColor, redColor, param));
                    }
                }
            }
            else
            {
                if (hitObject != null)
                {
                    if (hitObject.GetComponent<DestructibleObject>())
                    {
                        hitObject.GetComponent<DestructibleObject>().regenProps = true;
                        if(hitObject.transform.tag == "Fungus")
                        {
                            hitObject.GetComponent<DestructibleObject>().destroyColor = hitObject.transform.GetChild(1).GetComponent<Renderer>().material.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
                            hitObject.GetComponent<DestructibleObject>().destroyColor = hitObject.transform.GetChild(2).GetComponent<Renderer>().material.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
                        }
                        else
                        {
                            hitObject.GetComponent<DestructibleObject>().destroyColor = hitObject.GetComponent<Renderer>().material.GetColor("Color_31be6d288cfa48f3808d0fed40841607");
                        }
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
