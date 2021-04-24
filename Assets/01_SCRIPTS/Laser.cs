using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserTransform;
    public Vector3 contactPos;
    public GameObject hitObject;

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

            hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<DestructibleObject>())
            {
                hit.transform.GetComponent<DestructibleObject>().HP -= 0.1f;
            }
        }
        else
        {
            hitObject = null;
            laserTransform.localScale = new Vector3(laserTransform.localScale.x, 15, laserTransform.localScale.z);
        }

    }
}
