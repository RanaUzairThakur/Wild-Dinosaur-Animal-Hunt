using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerscript : MonoBehaviour
{
	public GameObject rotatingPlane,bridgeAnimator,downPlank,upPlank;
	public GameObject Hitparticle,Hitparticle1;
	public AudioSource checkpointClip,coinAudio;
	public GameObject mainCamera;
    public GameObject[] fireOpeners;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter(Collider other){
		
		if(other.CompareTag("Player")){

			if(gameObject.name.Contains("PlaneRotTrigger")){
				//rotatingPlane.GetComponent<lerpRotation> ().enabled = true;
			}
			if(gameObject.name.Contains("default")){
				coinAudio.Play ();
				Hitparticle.SetActive (true);
				gameObject.SetActive (false);
			}
			if(gameObject.name.Contains("Circle")){
				checkpointClip.Play ();
				Hitparticle1.SetActive (true);
				gameObject.SetActive (false);
			}
			if(gameObject.name.Contains("bridge_stick-triiger")){

				Invoke ("bridgeRigidBody1",0.2f);
			}
			if(gameObject.name.Contains("bridgeAnimator")){
				bridgeAnimator.GetComponentInParent<Animator> ().enabled = true;
//				gameObject.SetActive (false);
			}
			if(gameObject.name.Contains("GoDownPlank")){
				
				downPlank.GetComponentInParent<Animator> ().enabled = true;
				//gameObject.GetComponent<Animator> ().enabled = true;
			}
			if(gameObject.name.Contains("GoUpPlank")){

				downPlank.GetComponentInParent<Animator> ().enabled = true;
				upPlank.GetComponentInParent<Animator> ().enabled = true;
				//gameObject.GetComponent<Animator> ().enabled = true;
			}

			if(gameObject.name.Contains("OffsetTrue")){

				//mainCamera.GetComponent<CameraController> ().changeOffset = true;
				gameObject.SetActive (false);
			}
			if(gameObject.name.Contains("OffsetFalse")){

				//mainCamera.GetComponent<CameraController> ().changeOffset = false;
				gameObject.SetActive (false);
			}
			if(gameObject.name.Contains("WaterColorQuad")){
				print ("Hahahaha");
				//mainCamera.GetComponent<CameraController> ().followPlayer = followplayerState.Stop;
				//gameObject.SetActive (false);
			}
            if (gameObject.name.Contains("fireopener")) {
                foreach (GameObject g in fireOpeners) {

                    g.SetActive(true);
                }

            }
		}
	}


	void bridgeRigidBody1(){
		
	
		gameObject.GetComponentInParent<Rigidbody>().useGravity=true;
		gameObject.GetComponentInParent<Rigidbody>().isKinematic=false;
	}
}
