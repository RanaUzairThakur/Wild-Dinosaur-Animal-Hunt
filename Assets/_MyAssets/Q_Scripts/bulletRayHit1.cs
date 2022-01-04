using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletRayHit1 : MonoBehaviour
{
    public  bool runningDetect;
    public static bulletRayHit1 instance;
    private void Start()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        if(follow.followChk==true)
        {
            RunningDetected();
        }
        

    }
    
    public  void RunningDetected()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 40f))
        {

            if (hit.collider.GetComponent<AS_BulletHiter>())
            {

               // Debug.Log("Running my bulletRayScript:" + hit.collider.gameObject);
                
                bulletDetector.instance.slowMotionNow(0.01f, 400f);
                //Destroy(gameObject);
                runningDetect = true;

            }
            else
            {
                follow.followChk = false;
            }
        }
    }
   
}
