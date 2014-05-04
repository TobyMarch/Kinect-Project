using UnityEngine;
using System.Collections;

public class TreasureChestCollectionBoxScript : MonoBehaviour {
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	private GameObject playerCharacter;
	#endregion
	
	
	void OnTriggerEnter (Collider col) {
		/*if (col.tag == "Player") {
			col.GetComponent<BDAutoMove>().slowMove();
		}*/
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
			col.GetComponent<BDAutoMove>().startMoving();
		}
		
	}
	
	// Use this for initialization
	void Awake () {
		playerCharacter = GameObject.Find(PlayerCharacterName);
	}
	
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
