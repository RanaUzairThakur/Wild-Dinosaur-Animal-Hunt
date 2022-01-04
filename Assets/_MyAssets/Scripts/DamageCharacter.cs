using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageCharacter : MonoBehaviour {
	public float TotalHealth = 100,currentHealth;
	public GameObject DeathObject;
	public Image HealthBar;
	public AudioClip hitsound,deadSound;
	public AudioSource audioSource;

	public static DamageCharacter Instance;
	public bool PlayerDied;
	// Use this for initialization
	void Start () {
        PlayerDied = false;

		if (this.gameObject.tag == "Enemy") {
			//HealthBar = GameObject.FindGameObjectWithTag ("GameManger").GetComponent<LevelManger> ().Animal_health;

//			audioSource = GetComponent<AudioSource>();
		}
		currentHealth = TotalHealth;
		if (HealthBar) 
		{
			HealthBar.fillAmount = currentHealth / TotalHealth;	
		}
	}
	
	// Update is called once per frame
//	void Update () {
//		if (currentHealth <= 0) {
//			Death ();
//		}
//	}
	void OnEnable(){

		if (this.gameObject.name == "Jeep") {
			//currentHealth = GameObject.FindGameObjectWithTag ("GameManger").GetComponent<LevelManger> ().PlayerModel.GetComponent<DamageCharacter> ().currentHealth;
		}

		if (this.gameObject.name == "Player") {
			//currentHealth = GameObject.FindGameObjectWithTag ("GameManger").GetComponent<LevelManger> ().Jeep.GetComponent<DamageCharacter> ().currentHealth;

		}

	}

	public void Damage(float damageReceived)
	{
		if (hitsound!=null) {
			audioSource.PlayOneShot (hitsound);
		}
		HealthBar.transform.parent.gameObject.SetActive (true);
		//Debug.Log ("DamageCalled");
//		Debug.Log (this.gameObject.name);
		if (this.gameObject.name.Contains ("Lion")) {
			//Debug.Log ("its Lion man");
			this.gameObject.GetComponent<Animator>().SetTrigger("Hit");

		}
		if (this.gameObject.name.Contains ("Tiger")) {
			//Debug.Log ("its tiger man");

		}
		currentHealth -= damageReceived;

		if (HealthBar) 
		{
			float current = (float)currentHealth;
			float total = (float)TotalHealth;
			HealthBar.fillAmount = (current / total);	
		}


		if (currentHealth <= 0 && !PlayerDied) {
						Death ();
					}
		
	}
	public void ApplyDamage(float damageReceived)
	{
		if (this.gameObject.tag == "Player") {
			audioSource.PlayOneShot (hitsound);
		}
		HealthBar.transform.parent.gameObject.SetActive (true);
		currentHealth -= damageReceived;
		if (HealthBar) 
		{
			float current = (float)currentHealth;
			float total = (float)TotalHealth;
			HealthBar.fillAmount = (current / total);	
		}
		if (currentHealth <= 0 && !PlayerDied) {
			Death ();
		}
	}
	public void Death(){
		HealthBar.transform.parent.gameObject.SetActive (false);
		if (DeathObject) {
			Instantiate (DeathObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
			Destroy (this.gameObject);
		}

//		
		if (this.gameObject.CompareTag ("Player")) {
			audioSource.PlayOneShot (deadSound);

			wasii_GamePlay_UI_Handler.instance.Level_Failed();
            Debug.Log("fail1");
            PlayerDied = true;

            Debug.Log("PlayerDied");

//			Destroy (this.gameObject);
		}


		if (this.gameObject.CompareTag ("Enemy")) {
			ObjectiveHandler.instance.EnemyKilled ();
			if(deadSound!=null)
			 	audioSource.PlayOneShot (deadSound);
		}
	}


}
