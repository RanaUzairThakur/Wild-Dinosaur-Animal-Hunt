using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollower : AS_ActionPreset
{
    public float ZoomMulti = 1;
    public override void Shoot(GameObject bullet)
    {
        if (!ActionCam)
        {
            return;
        }

        ActionCam.InAction = false;
        ActionCam.ObjectLookAt = bullet;
        ActionCam.SetPosition(bullet.transform.position, false);
        base.Shoot(bullet);
    }

    public override void FirstDetected(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {
        if (!ActionCam)
        {
            return;
        }

        if (!ActionCam.InAction)
        {
            ActionCam.ObjectLookAt = bullet.gameObject;
            ActionCam.Follow = true;
            ActionCam.ActionBullet(2.0f);
            //ActionCam.SlowmotionNow (0.25f, 3.0f);
            ActionCam.Slowmotion(0.015f, 10f);
            ActionCam.LengthMult = 0.2f;
            ActionCam.SetPosition(bullet.transform.position + (bullet.transform.right * ZoomMulti) - (bullet.transform.forward * ZoomMulti), ActionCam.Detected);
            ActionCam.CameraOffset = Vector3.right;
        }


        base.FirstDetected(bullet, target, point);
    }
    public void bulleFollow()
    {

    }
   
    public override void TargetDetected(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {

        if (!ActionCam)
        {
            return;
        }
        if (!ActionCam.HitTarget)
        {
            ActionCam.Follow = true;
            ActionCam.ObjectLookAt = target.gameObject;
            ActionCam.ActionBullet(2.0f);
            ActionCam.Slowmotion(0.015f, 10f);
            ActionCam.LengthMult = 0.2f;
            ActionCam.SetPosition(bullet.transform.position + (bullet.transform.right * ZoomMulti) - (bullet.transform.forward * ZoomMulti), ActionCam.Detected);
            ActionCam.CameraOffset = Vector3.right;
        }
        base.TargetDetected(bullet, target, point);
    }

    public override void TargetHited(AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
    {
        if (!ActionCam)
        {
            return;
        }
        
        ActionCam.ActionBullet(2.0f);
        ActionCam.ObjectLookAt = null;
        ActionCam.SlowmotionNow(0.5f, 1.6f);
        ActionCam.Follow = true;
        ActionCam.lookAtPosition = point;
        ActionCam.SetPositionDistance(point, true);


        base.TargetHited(bullet, target, point);

    }
}
