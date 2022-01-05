using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider))]

public class Hit_Body : AS_BulletHiter
{
	public int Suffix = 0;
	public float DamageMult = 1;
	public DamageManager damageManage;
    //public GameObject hitText,ScoreText;

    void Start(){


        if (damageManage == null){
			damageManage = this.RootObject.GetComponent<DamageManager> ();
		}
	}
	
	public override void OnHit (RaycastHit hit, AS_Bullet bullet)
	{
        //hitText.SetActive(true);
        //ScoreText.SetActive(true);
        //hitText.GetComponent<Text>().text = "BODY SHOT";
        //ScoreText.GetComponent<Text>().text = "$50+";
        //GamePlay_UI_Handler.instance.BodyShotScore();
       // print("Body Shot");


        float distance = Vector3.Distance (bullet.pointShoot, hit.point);
		if (damageManage) {
			int damage = (int)((float)bullet.Damage * DamageMult);
			damageManage.ApplyDamage (damage, bullet.transform.forward * bullet.HitForce, distance, Suffix);
		}
		AddAudio (hit.point);
		base.OnHit (hit, bullet);
	}

   
}
