using UnityEngine;
using System.Collections;

public class TreasureChestCollectionBoxScript : MonoBehaviour {
	#region GameObject Reference Names
	
	#endregion
	
	
	void OnTriggerEnter () {
		
	}
	
	void OnTriggerStay () {
		
	}
	
	//Called when trigger zone no longer detects a gameObject with a RigidBody 
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Checks to see if the user has completed the required pose within the time limit
			if (col.GetComponent<BDGameScript>().GetPoseCompletion()) {
				Debug.Log("HEY LOOK Pose Held!");
			}
			//Redirect the player character toward the next corner collider
			Vector3 nextCornerPosition = col.GetComponent<BDAutoMove>().GetNextCornerPosition();
			col.transform.LookAt(nextCornerPosition);
		}
		
	}
	
	// Use this for initialization
	void Awake () {
		
	}
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
