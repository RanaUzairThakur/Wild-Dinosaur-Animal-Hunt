using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {
	public Animation anim;
	public float animationSpeed;
	// Use this for initialization
	void Start () {

		anim=this.gameObject.GetComponent<Animation> ();
		if (anim != null) {
			anim ["deer_died"].speed = animationSpeed;
		}

		Destroy (this.gameObject,5f);
	}
	
	// Update is called once per frame

}
