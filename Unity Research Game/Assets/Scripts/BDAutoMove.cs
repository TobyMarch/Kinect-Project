using UnityEngine;
using System.Collections;

/// <summary>
/// Script controlling the movement of the player character
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class BDAutoMove : MonoBehaviour {
	#region GameObject Reference Names
	/// <summary>
	/// Public string storing the name of the intermediate object. Used for GameObject.Find() calls
	/// </summary>/
	public string intermediateObjectName = "Intermediate";
	/// <summary>
	/// Public string storing the name of the animated character model. Used for GameObject.Find() calls
	/// </summary>/
	public string characterModelName = "Alexis";
	#endregion
	
	#region GameObject References
	/// <summary>
	/// Private GaeObject that stores a reference to the intermediate object used to communicate between Zigfu and Unity.
	/// </summary>
	private GameObject intermediateObject;
	
	/// <summary>
	/// Private GameObject that stores a reference to the player character's model
	/// </summary>
	private GameObject characterModel;
	#endregion
	
	#region public variables
	/// <summary>
	/// Public float storing the starting movement speed of the player character
	/// </summary>
	public float moveSpeed = 5.0f;
	#endregion
	
	#region private variables
	/// <summary>
	/// Public float storing the movement speed of the player character when picking up a treasure chest
	/// </summary>
	private float superSlowSpeed = 1.0f;
	
	/// <summary>
	/// Private float storing the amount to which the player character's position is translated with every autoMove()
	/// </summary>
	private float currentSpeed = 0.0f;
	
	/// <summary>
	/// Private Vector3 storing the world tranform of the next CornerCollider
	/// </summary>
	private Vector3 nextCornerPosition;
	
	/// <summary>
	/// Private Vector3 storing the world transform of the next available treasure chest
	/// </summary>
	private Vector3	nextChestPosition;
	
	/// <summary>
	/// Private string storing the name of the last box collider (CornerCollider) visted
	/// </summary>
	private string lastBCVisited;
	
	/// <summary>
	/// Private bool the determines whether the camera always looks directly at the player character,
	/// or just says at a constant distance
	/// </summary>
	private bool directCameraFollow = true;
	#endregion
	
	
	#region Movement Control functions
	/// <summary>
	///  generic movement function, moves the player character a set amount every time it is called
	/// </summary>
	void AutoMove () {
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
	}
	
	/// <summary>
	/// Used by other scripts to start the player character moving
	/// </summary>
	public void StartMoving () {
		//Debug.Log("startMoving Called!");
		//Triggers character model's running animations
		characterModel.GetComponent<RootMotionCharacterControlACTION>().SetMovingAndRunning(true, true);
		//Sets the current movement speed to the starting speed
		currentSpeed = moveSpeed;
		
	}
	
	/// <summary>
	/// Used by other scripts to stop the player character
	/// </summary>
	public void StopMoving() {
		//Debug.Log("StopMoving Called!");
		//Stops character model's running animations
		characterModel.GetComponent<RootMotionCharacterControlACTION>().SetMovingAndRunning(false, false);
		//Stops the player character
		currentSpeed = 0.0f;
	}
	
	/// <summary>
	/// Used by other scripts to slow the player character's movement, in order to pick up treasure chests
	/// </summary>
	public void SlowMove () {
		//Debug.Log("SlowMove Called!");
		currentSpeed = superSlowSpeed;		
	}
	
	/// <summary>
	/// Calculates the appropriate speed to get the PC to the treasure chest based on distance and time limit
	/// </summary>
	/// <param name='transformIn'>
	/// Vector3 Transform of the treasure chest
	/// </param>
	public void CalculateCurrentSpeed (Vector3 transformIn) {
		//Debug.Log("Position received: " + transformIn);
		//Calculates float value of distance between passed transform and own transform
		float distance = Vector3.Distance(transformIn,transform.position);
		//Debug.Log("Distance calculated: " + distance);
		
		// Uses the game time to determine the player character's speed
		int remainingTime = intermediateObject.GetComponent<TranslationLayer>().countdownTimer.GetRemainingTime();
		currentSpeed = distance/remainingTime;
		//Debug.Log("Speed calculated: " + currentSpeed);
	}
	
	/// <summary>
	/// Changes the rotation of the gameObject to point toward another object.
	/// </summary>
	/// <param name='transformIn'>
	/// Vector3 Transform of new world position to point at.
	/// </param>
	public void PointAtNext(Vector3 transformIn) {
		gameObject.transform.LookAt(transformIn);
	}
	#endregion
	
	#region Camera Positioning functions
	/// <summary>
	/// Toggles the direct camera follow on or off
	/// When on, camera will point in the same direction as the player character
	/// When off, camera will stay fixed in same direction, but stay close to the player character
	/// </summary>
	public void setDirectCameraFollow (bool stateIn) {
		directCameraFollow = stateIn;	
	}
	
	/// <summary>
	/// Gets the state of the directCameraFollow flag.
	/// </summary>
	/// <returns>
	/// bool directCameraFollow
	/// </returns>
	public bool getDirectCameraFollow () {
		return directCameraFollow;
	}
	#endregion
	
	#region Object Reference functions
	/// <summary>
	/// Allows the player character object to remember the last CornerCollider it visited
	/// </summary>
	/// <param name='nameIn'>
	/// String name of the last CornerCollider visited
	/// </param>
	public void SetLastBCVisited(string nameIn) {
			lastBCVisited = nameIn;
	}
	
	/// <summary>
	/// Returns the name of the last CornerCollider visited.
	/// </summary>
	/// <returns>
	/// String name of the last CornerCollider visited.
	/// </returns>
	public string GetLastBCVisited () {
		return lastBCVisited;
	}
	
	/// <summary>
	/// Saves the position of the next CornerCollider that the player character will visit
	/// </summary>
	/// <param name='destIn'>
	/// Vector3 world transform of the next CornerCollider.
	/// </param>
	public void SetNextCornerPosition (Vector3 destIn) {
		nextCornerPosition = destIn;
	}
	
	/// <summary>
	/// Returns the position of the next CornerColiider the player character will visit
	/// </summary>
	/// <returns>
	/// Vector3 world transform of the next CornerCollider
	/// </returns>
	public Vector3 GetNextCornerPosition () {
		return nextCornerPosition;
	}
	
	/// <summary>
	/// Saves the position of the next treasure chest that the player character may earn.
	/// </summary>
	/// <param name='destIn'>
	/// Vector3 world transform of the next treasure chest.
	/// </param>
	public void SetNextChestPosition (Vector3 destIn) {
		nextChestPosition = destIn;
	}
	
	/// <summary>
	/// Returns the position of the next treasure chest that the player character may earn
	/// </summary>
	/// <returns>
	/// Vector3 world transform of the next treasure chest
	/// </returns>
	public Vector3 GetNextChestPosition () {
		return nextChestPosition;
	}
	#endregion
	
	/// <summary>
	/// Used for initialization
	/// </summary>
	void Awake () {
		//Establish a reference to the game timer and character model
		intermediateObject = GameObject.Find(intermediateObjectName);
		characterModel = GameObject.Find(characterModelName);
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
		AutoMove();
	}
}
