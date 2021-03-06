using UnityEngine;
using System.Collections;

public class AP_BulletFollowPreset : AS_ActionPreset
{
	public float ZoomMulti = 1;

	public override void Shoot (GameObject bullet)
	{
		if (!ActionCam) {
			return;	
		}
		
		ActionCam.InAction = false;
		ActionCam.ObjectLookAt = bullet;
		ActionCam.SetPosition (bullet.transform.position, false);
		base.Shoot (bullet);
	}
	
	public override void FirstDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		
		if (!ActionCam.InAction) {
			ActionCam.ObjectLookAt = bullet.gameObject;
			ActionCam.Follow = true;
			ActionCam.ActionBullet (10.0f);
			ActionCam.SlowmotionNow (0.015f, 3.0f);
			ActionCam.LengthMult = 0.1f;
			ActionCam.SetPosition (bullet.transform.position + (bullet.transform.right * ZoomMulti) - (bullet.transform.forward * ZoomMulti), ActionCam.Detected);
			ActionCam.CameraOffset = -Vector3.right;
		}
		
		
		base.FirstDetected (bullet, target, point);
	}
	
	public override void TargetDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		
		if (!ActionCam) {
			return;	
		}
		if (!ActionCam.HitTarget) {
			ActionCam.Follow = true;
			ActionCam.ObjectLookAt = target.gameObject;
			ActionCam.ActionBullet (10.0f);
			ActionCam.Slowmotion (0.015f, 10f);
			ActionCam.LengthMult = 0.1f;
			ActionCam.SetPosition (bullet.transform.position + (bullet.transform.right * ZoomMulti) - (bullet.transform.forward * ZoomMulti), ActionCam.Detected);
			ActionCam.CameraOffset = -Vector3.right;
		}
		base.TargetDetected (bullet, target, point);
	}
	
	public override void TargetHited (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		
			ActionCam.ActionBullet (0.1f);
			ActionCam.ObjectLookAt = null;
			ActionCam.SlowmotionNow (0.15f, 2.5f);
			ActionCam.Follow = true;
			ActionCam.lookAtPosition = point;
			ActionCam.SetPositionDistance (point, true);


		base.TargetHited (bullet, target, point);
		
	}
}
