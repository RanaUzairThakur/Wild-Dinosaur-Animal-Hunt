using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletRayHit2 : MonoBehaviour
{
    public  bool hitTarget;

    void FixedUpdate()
    {
        if(follow.followChk==true && ! hitTarget)
        {
            HittingDetect();
        }

    }
    public void HittingDetect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 10f))
        {

            if (hit.collider.GetComponent<AS_BulletHiter>())
            {

              //  Debug.Log("HIt in my bulletRayScript:" + hit.collider.gameObject);
                bulletDetector.instance.slowMotionNow(0.05f, 400f);
                //Destroy(gameObject);
                hitTarget = true;

            }
            else
            {
                follow.followChk = false;
            }
        }
    }
   
}
