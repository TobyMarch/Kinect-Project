using UnityEngine;
using System.Collections;

/// <summary>
/// Script controlling each TreasureChestCollectionBox, which hold chests that the player may collect
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class TreasureChestCollectionBoxScript : MonoBehaviour {
	#region GameObject Names
	/// <summary>
	/// Public string storing the name of the player character. Used for GameObject.Find() calls
	/// </summary>
	public static string PlayerCharacterName = "Third Person PC";
	
	/// <summary>
	/// Public string storing the name of the animated character model. Used for GameObject.Find() calls
	/// </summary>/
	public string characterModelName = "Alexis";
	#endregion
	#region GameObject References
	/// <summary>
	/// Private GameObject that stores a reference to the player character object
	/// </summary>
	private GameObject playerCharacter;
	
	/// <summary>
	/// Private GameObject that stores a reference to the player character's model
	/// </summary>
	private GameObject characterModel;
	#endregion
	
	/// <summary>
	/// Raises the trigger enter event.
	/// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			//Debug.Log(gameObject.name + " was hit by " + col.name);
			if (col.GetComponent<BDGameScript>().GetPoseCompletion()) {
				//
				col.GetComponent<BDAutoMove>().SlowMove();
				characterModel.GetComponent<RootMotionCharacterControlACTION>().SetJumping();
			}
		}
	} 
	
	/// <summary>
	/// Raises the trigger exit event.
	/// Called when trigger zone no longer detects a gameObject with a RigidBody
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param> 
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Redirect the player character toward the next corner collider
			Vector3 nextCornerPosition = col.GetComponent<BDAutoMove>().GetNextCornerPosition();
			col.transform.LookAt(nextCornerPosition);
			//Return the player to previous move speed
			col.GetComponent<BDAutoMove>().StartMoving();
		}
		
	}
	
	/// <summary>
	/// Used for initialization
	/// </summary>
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
