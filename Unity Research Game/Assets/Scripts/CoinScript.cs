using UnityEngine;
using System.Collections;

/// <summary>
/// Script controlling each coin, which the player collects automatically throughout the game
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class CoinScript : MonoBehaviour {
	
	#region GameObject Names
	/// <summary>
	/// Public string storing the name of the player character object. Used for GameObject.Find() calls
	/// </summary>
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region Public Variables
	/// <summary>
	/// Public float storing the amount that each coin rotates in every update.
	/// </summary>
	public float rotationAmount = 2.0f;
	#endregion
	#region Private Variables
	/// <summary>
	/// Private GameObject reference to the player character
	/// </summary>
	private GameObject playerCharacter;
	#endregion
	
	/// <summary>
	/// Destroys this instance, and increments the player's score
	/// </summary>
	void vanish () {
		Destroy(gameObject);
		playerCharacter.GetComponent<BDGameScript>().CoinCollected();
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerEnter (Collider col) {
		//Debug.Log("Coin" + gameObject.name + " was hit by " + col.name);
		vanish();	
	}
	/// <summary>
	/// Used for initialization.
	/// </summary>
	void Awake () {
		playerCharacter = GameObject.Find(PlayerCharacterName);
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		transform.Rotate(0,rotationAmount,0);
	}
}
