using UnityEngine;
using System.Collections;

public class BDAutoMove : MonoBehaviour {
	#region GameObject Reference Names
	public string intermediateObjectName = "Intermediate";
	#endregion
	
	#region GameObject References
	private GameObject intermediateObject;
	#endregion
	
	#region public variables
	//[SerializeField]
	public float moveSpeed = 10.0f;
	#endregion
	
	#region private variables
	private float currentSpeed = 0.0f;
	private Vector3 savedDestination;
	private string lastBCVisited;
	private bool directCameraFollow = true;
	
	#endregion
	
	/// <summary>
	///  generic movement function, called every frame
	/// </summary>
	void autoMove () {
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
	}
	
	#region Movement Control functions
	/// <summary>
	/// Used by other scripts to start the player character moving
	/// </summary>
	public void startMoving () {
		//Debug.Log("startMoving Called!");
		currentSpeed = moveSpeed;
	}
	
	/// <summary>
	/// Stops the player character
	/// </summary>
	public void stopMoving() {
		//Debug.Log("StopMoving Called!");
		currentSpeed = 0.0f;
	}
	
	/// <summary>
	/// Calculates the appropriate speed to get the PC to the treasure chest based on distance and time limit
	/// </summary>
	public void calculateCurrentSpeed (Vector3 transformIn) {
		//Calculates float value of distance between passed transform and own transform
		//Debug.Log("Position received: " + transformIn);
		float distance = Vector3.Distance(transformIn,transform.position);
		//Debug.Log("Distance calculated: " + distance);
		
		int remainingTime = intermediateObject.GetComponent<TranslationLayer>().countdownTimer.GetRemainingTime();
		
		currentSpeed = distance/remainingTime;
		//Debug.Log("Speed calculated: " + currentSpeed);
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
	
	public bool getDirectCameraFollow () {
		return directCameraFollow;
	}
	#endregion
	
	#region Object Reference functions
	public void setLastBCVisited(string nameIn) {
			lastBCVisited = nameIn;
	}
	
	public string getLastBCVisited () {
		return lastBCVisited;
	}
	
	public void setSavedDest (Vector3 destIn) {
		//Debug.Log("SavedDest called!");
		savedDestination = destIn;
	}
	
	public Vector3 getSavedDest () {
		return savedDestination;
	}
	#endregion
	// Use this for initialization
	void Start () {
		//Establish a reference to the game timer
		intermediateObject = GameObject.Find(intermediateObjectName);
		//startMoving();
	}
	
	// Update is called once per frame
	void Update () {
		autoMove();
		//Debug.Log("Current Heading:" + transform.forward);
	}
}
