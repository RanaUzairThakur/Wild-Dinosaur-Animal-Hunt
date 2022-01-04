using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Change_Health_Bar : MonoBehaviour {
	public Image Healthbar;
	public float currentHelth, TotalHealth;
	// Use this for initialization
	void Start () {
		//Healthbar = GameObject.FindGameObjectWithTag ("GameManger").GetComponent<LevelManger> ().Animal_health;
		currentHelth = this.gameObject.GetComponent<Emerald_Animal_AI> ().currentHealth;
		TotalHealth = this.gameObject.GetComponent<Emerald_Animal_AI> ().startingHealth;
	}
	
	// Update is called once per frame
//	void Update () {
//		Healthbar.fillAmount = currentHelth / TotalHealth;	
//	}


	public void ChangeHealthBar(){
		Debug.Log ("Health Called");
		if (this.gameObject.name.Contains ("elephant")) {
			Debug.Log ("its elephant man ");
		}
		if (this.gameObject.name.Contains ("Hippo")) {
			Debug.Log ("its hippo man ");
		}
		Healthbar.transform.parent.gameObject.SetActive (true);
		currentHelth = this.gameObject.GetComponent<Emerald_Animal_AI> ().currentHealth;
		TotalHealth = this.gameObject.GetComponent<Emerald_Animal_AI> ().startingHealth;
		Healthbar.fillAmount = currentHelth / TotalHealth;	
		if (this.gameObject.name.Contains ("Deer")) {
			Debug.Log ("its deer man");
		}
		if (Healthbar.fillAmount == 0) {
			Healthbar.transform.parent.gameObject.SetActive (false);
		
		
		}
	}
}
