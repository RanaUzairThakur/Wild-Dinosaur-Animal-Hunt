using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAIWorking : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<Emerald_Animal_AI> () == null) {
			Destroy (this.gameObject);
			Debug.Log ("Destroy ForceFully");
		}
	}
}
