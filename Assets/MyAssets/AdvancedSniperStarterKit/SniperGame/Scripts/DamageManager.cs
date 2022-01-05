using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{

	public GameObject[] deadbody;
	public AudioClip[] hitsound;
	public int hp = 100;
	public int Score = 10;
	private float distancedamage;
	public static int KilledAnimal;
    public bool isDead;

	void Start(){
        isDead = false;
	//	DeadAnim = this.gameObject.GetComponenet<Animation>();
	}
	
	void Update(){
		if (hp <= 0) {
		Dead (Random.Range (0, deadbody.Length));
            
		}
       // Debug.Log("Test damage");
    }
	
	public void ApplyDamage (int damage, Vector3 velosity, float distance)
	{
		if (hp <= 0) {
			return;
		}
		distancedamage = distance;
		hp -= damage;
	}
	
	public void ApplyDamage (int damage, Vector3 velosity, float distance, int suffix)
	{
		if (hp <= 0) {
			return;
		}
		distancedamage = distance;
		hp -= damage;
		if (hp <= 0) {
			Dead (suffix);
		}
		
	}
	
	public void AfterDead (int suffix)
	{

		int scoreplus = Score;
		
		if(suffix == 2){
			scoreplus = Score * 5;	
		}
			
		ScoreManager score = (ScoreManager)GameObject.FindObjectOfType (typeof(ScoreManager));	
		if(score){
			score.AddScore (scoreplus, distancedamage);
		}
	}
	
	
	public void Dead (int suffix)
	{
        if (!isDead)
        {
            if (Gun.instance.crossHairOwnUse.enabled == true)
            {
                Gun.instance.mainSlider.value = 0;
                Gun.instance.mainSlider.gameObject.SetActive(false);
            }
            // Dead Animal Count Here
            KilledAnimal++;

			wasii_GamePlay_UI_Handler.instance.Complete_level();
           // print("Dead Counter " + KilledAnimal);
            isDead = true;
            //Debug.Log("Check dead value"  +KilledAnimal);
            //DeadAnim.Play("death");
        }
        

        if (deadbody.Length > 0 && suffix >= 0 && suffix < deadbody.Length) {
			// this Object has removed by Dead and replaced with Ragdoll. the ObjectLookAt will null and ActionCamera will stop following and looking.
			// so we have to update ObjectLookAt to this Ragdoll replacement. then ActionCamera to continue fucusing on it.
			GameObject deadReplace = (GameObject)Instantiate (deadbody [suffix], this.transform.position, this.transform.rotation);
			// copy all of transforms to dead object replaced
			CopyTransformsRecurse (this.transform, deadReplace);
			// destroy dead object replaced after 5 sec
			Destroy (deadReplace, 5);
			// destry this game object.
			Destroy (this.gameObject,1);
			this.gameObject.SetActive(false);
		// print("Dead After counter "+KilledAnimal);
		}
		AfterDead (suffix);
	}
	
	// Copy all transforms to Ragdoll object
	public void CopyTransformsRecurse (Transform src, GameObject dst)
	{
		
	
		dst.transform.position = src.position;
		dst.transform.rotation = src.rotation;

		
		foreach (Transform child in dst.transform) {
			var curSrc = src.Find (child.name);
			if (curSrc) {
				CopyTransformsRecurse (curSrc, child.gameObject);
			}
		}
	}

}
