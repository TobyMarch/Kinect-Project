using UnityEngine;
using System.Collections;

public class TriggerBoxScript : MonoBehaviour {
	
	void OnTriggerEnter () {
		
	}
	
	void OnTriggerStay () {
		
	}
	
	//Called when trigger zone no longer detects a gameObject with a RigidBody 
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Call directly to the translation layer (will be replaced)
			//GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().ListenForNextWholeBodyGesture();
			//Call to BDGameScript
			col.GetComponent<BDGameScript>().triggerNextPose();
		}
		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
