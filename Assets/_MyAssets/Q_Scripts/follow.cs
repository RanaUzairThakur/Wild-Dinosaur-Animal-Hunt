using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : AS_ActionPreset
{
   public static bool followChk,rotationChk;
    // Start is called before the first frame update
   
    public override void Shoot(GameObject bullet)
    {
        base.Shoot(bullet);
    }
    public override void FirstDetected(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {
        followChk = true;
        Debug.Log("First Detected");
      //  bulletDetector.instance.slowMotionNow(0.000002f,500f);
        //AS_ActionCamera.instance.SlowmotionNow(0.002f, 3f);
        //AS_ActionCamera.instance.ActionBullet(10f);
        //Debug.Log("slowotionActive :" + Time.timeScale);
        base.FirstDetected(bullet, target, point);
    }
    public override void TargetDetected(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {
        followChk= true;
        // AS_ActionCamera.instance.ActionBullet(10f);
        //  Gun.instance.offSet2 = new Vector3(0.5f,0.35f,-0.015f);
        // ActionCam.Follow = true;
        //AS_ActionCamera.instance.SlowmotionNow(0.05f, 5f);


        //Debug.Log("slowotionActive :"+Time.timeScale);
        Debug.Log("target Detected");
        base.TargetDetected(bullet, target, point);
    }
    public override void TargetHited(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {
        //AS_ActionCamera.instance.TimeSet(1);
    //    bulletDetector.instance.TimeReset();
        Debug.Log("target Hitted");
        base.TargetHited(bullet, target, point);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
