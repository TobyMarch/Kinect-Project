using UnityEngine;
using System.Collections;

/// <summary>
/// Script controlling each treasure chest, which the player collects as a reward for completing poses
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class TreasureChestScript : MonoBehaviour {
	#region GameObject Names
	/// <summary>
	/// Public string storing the name of the player character. Used for GameObject.Find() calls
	/// </summary>
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	/// <summary>
	/// Public GameObject reference to the player character object
	/// </summary>
	private GameObject playerCharacter;
	#endregion
	
	/// <summary>
	/// Destroys this instance, and increments the player's score
	/// </summary>
	void vanish () {
		playerCharacter.GetComponent<BDGameScript>().ChestCollected();
		Destroy(gameObject);
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerEnter (Collider col) {
		//Debug.Log(gameObject.name + " was hit by " + col.name);
		vanish();
		
	}
	
	/// <summary>
	/// Used for initialization.
	/// </summary>
	void Awake () {
		playerCharacter = GameObject.Find(PlayerCharacterName);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
