using UnityEngine;
using System.Collections;

public class TreasureChestCollectionBoxScript : MonoBehaviour {
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	public string characterModelName = "Alexis";
	#endregion
	#region GameObject References
	private GameObject playerCharacter;
	private GameObject characterModel;
	#endregion
	
	
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			Debug.Log(gameObject.name + " was hit by " + col.name);
			if (col.GetComponent<BDGameScript>().GetPoseCompletion()) {
				col.GetComponent<BDAutoMove>().SlowMove();
				characterModel.GetComponent<RootMotionCharacterControlACTION>().SetJumping();
			}
		}
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
			col.GetComponent<BDAutoMove>().StartMoving();
		}
		
	}
	
	// Use this for initialization
	void Awake () {
		playerCharacter = GameObject.Find(PlayerCharacterName);
		characterModel = GameObject.Find(characterModelName);
	}
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
