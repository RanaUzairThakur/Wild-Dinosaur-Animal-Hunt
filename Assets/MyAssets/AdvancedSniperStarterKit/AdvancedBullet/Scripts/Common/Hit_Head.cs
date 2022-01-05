using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CapsuleCollider))]

public class Hit_Head : AS_BulletHiter
{
    public int Suffix = 2;
    public float DamageMult = 3;
    public DamageManager damageManage;
    //public GameObject hitText,ScoreText;

    void Start()
    {
        if (damageManage == null)
        {
            damageManage = this.RootObject.GetComponent<DamageManager>();
        }
    }

    public override void OnHit(RaycastHit hit, AS_Bullet bullet)
    {
        //hitText.SetActive(true);
        //ScoreText.SetActive(true);
        //hitText.GetComponent<Text>().text = "HEAD SHOT";
        //ScoreText.GetComponent<Text>().text = "$100+";
        //GamePlay_UI_Handler.instance.HeadShotScore();
        // print("HEAD Shot");

        float distance = Vector3.Distance(bullet.pointShoot, hit.point);
        if (damageManage)
        {
            int damage = (int)((float)bullet.Damage * DamageMult);
            damageManage.ApplyDamage(damage, bullet.transform.forward * bullet.HitForce, distance, Suffix);
        }
        AddAudio(hit.point);
        base.OnHit(hit, bullet);
    }
}
