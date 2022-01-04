using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonDogcontroller : MonoBehaviour {
	Animator animator;
	private UnityEngine.AI.NavMeshAgent navAgent;
	AudioSource audio;
	float distance;

	public GameObject Target;
	public bool dead;
	public float navstopdistance=2f;
	bool canPunch;
	bool punch;
	public float punchRate;
	float rate;
	float YPrevAngle;
	float m_Yaw;
	public float max_followdistance=25f;
	public CapsuleCollider enemycol;
	public AudioClip barksound, runningsound, attacksound;
	// Use this for initialization
	void Start () {
		
		navAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();	
		audio = GetComponent<AudioSource> ();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Target = GameObject.FindGameObjectWithTag ("Player");
		distance = Vector3.Distance (Target.transform.position,transform.position);
		if (!dead) {
			if (distance < max_followdistance) {

				if (!navAgent.enabled)
					navAgent.enabled = true;


				if (navAgent) {
					navAgent.stoppingDistance = navstopdistance;
//					this.gameObject.GetComponent<Emerald_Animal_AI> ().enabled = false;
					navAgent.SetDestination (Target.transform.position);
					animator.SetFloat ("speed", (navAgent.velocity.magnitude / navAgent.speed));

				}
			}

			if (distance <= navAgent.stoppingDistance) {
				UpdateRotation ();
				canPunch = true;

			} else {

				canPunch = false;
			}
//			animator.SetBool ("canPunch", canPunch);
			m_Yaw = Mathf.DeltaAngle (YPrevAngle, transform.eulerAngles.y);
//			animator.SetFloat ("yaw", m_Yaw);
			if (canPunch) {

				if (rate > punchRate) {
					
					rate = 0;
					punch = true;

				} else {
					rate += Time.deltaTime;

				}
			} else {
				punch = false;
			}	

		
			if (punch) {
				punch = false;
				audio.clip = null;
				audio.loop = false;
				audio.Stop ();
				animator.SetTrigger ("Attack");
				Invoke ("re_offcollider", 1.5f);
				Invoke ("re_oncollider", 0.5f);
				audio.PlayOneShot (attacksound,0.6f);
//				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Firststate")) {
//					int temphitnumber = Random.Range (1, 4);
//					animator.SetInteger ("hitnumber", temphitnumber);
//					//					animator.SetInteger ("hitnumber",1);
//
//					enemycol .enabled = true;
//
//					Invoke ("numbertozero", 0.4f);
//					Invoke ("re_offcollider", 1.5f);
//					//					Target.GetComponent<DamageManager> ().ApplyDamage(20);
//					punch = false;
//				}
			}

		} else {
			if (navAgent.enabled) {
				navAgent.Stop ();
				navAgent.enabled = false;
			}

		}
		YPrevAngle = transform.eulerAngles.y;
	}

	void UpdateRotation()
	{
		var eulerRotation = transform.eulerAngles;

		Quaternion a = Quaternion.LookRotation (Target.transform.position-transform.position);

		eulerRotation.y = a.eulerAngles.y;
		eulerRotation.y = Mathf.LerpAngle(transform.eulerAngles.y, eulerRotation.y, 6 * Time.deltaTime);
		var rotation = Quaternion.Euler(eulerRotation);
		transform.rotation = rotation;
	}
	public void re_oncollider(){
		enemycol.enabled = true;
	}

	public void re_offcollider(){
		enemycol.enabled = false;
		audio.clip = barksound;
		audio.loop = true;
		audio.Play ();
	}
}
