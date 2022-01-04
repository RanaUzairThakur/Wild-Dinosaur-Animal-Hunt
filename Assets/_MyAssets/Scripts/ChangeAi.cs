using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAi : MonoBehaviour {
	
	private UnityEngine.AI.NavMeshAgent navAgent;
	float distance;
	public GameObject AggrasiveModel;
	public Transform Target;
	public Emerald_Animal_AI animalAI;
	public float max_followdistance=25f;
	void Start () {
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();	
		animalAI = GetComponent<Emerald_Animal_AI> ();
		Target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {

//		if (Renderer.isVisible) Debug.Log("Visible");
//		else Debug.Log("Not visible");

		Target = GameObject.FindGameObjectWithTag ("Player").transform;
		distance = Vector3.Distance (Target.transform.position, transform.position);
			if (distance < max_followdistance) {

				if (!navAgent.enabled)
					navAgent.enabled = true;

			InstiateAggrasiveAnimal ();

			}

		//if (animalAI.isFleeFromBullet) {
		//	InstiateAggrasiveAnimal ();
		//	Debug.Log ("its Ai");
		//}
		}


	public void OnBecameInvisible(){
		Debug.Log ("visible");
	}

	public void InstiateAggrasiveAnimal(){
		Debug.Log ("Its Working Change");
		GameObject obj = Instantiate (AggrasiveModel, this.gameObject.transform.position, this.gameObject.transform.rotation);
		Destroy (this.gameObject);
	}

}
