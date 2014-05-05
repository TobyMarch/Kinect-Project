using UnityEngine;
using System.Collections;

public class BDAutoMove : MonoBehaviour {
	#region GameObject Reference Names
	public string intermediateObjectName = "Intermediate";
	public string characterModelName = "Alexis";
	#endregion
	
	#region GameObject References
	private GameObject intermediateObject;
	private GameObject characterModel;
	#endregion
	
	#region public variables
	//[SerializeField]
	public float moveSpeed = 10.0f;
	#endregion
	
	#region private variables
	private float superSlowSpeed = 1.0f;
	private float currentSpeed = 0.0f;
	private Vector3 nextCornerPosition;
	private Vector3	nextChestPosition;
	private string lastBCVisited;
	private bool directCameraFollow = true;
	
	
	#endregion
	
	/// <summary>
	///  generic movement function, called every frame
	/// </summary>
	void AutoMove () {
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
	}
	
	#region Movement Control functions
	/// <summary>
	/// Used by other scripts to start the player character moving
	/// </summary>
	public void StartMoving () {
		//Debug.Log("startMoving Called!");
		characterModel.GetComponent<RootMotionCharacterControlACTION>().SetMovingAndRunning(true, true);
		currentSpeed = moveSpeed;
		
	}
	
	/// <summary>
	/// Stops the player character
	/// </summary>
	public void StopMoving() {
		//Debug.Log("StopMoving Called!");
		characterModel.GetComponent<RootMotionCharacterControlACTION>().SetMovingAndRunning(false, false);
		currentSpeed = 0.0f;
	}
	
	public void SlowMove () {
		Debug.Log("SlowMove Called!");
		currentSpeed = superSlowSpeed;		
	}
	
	/// <summary>
	/// Calculates the appropriate speed to get the PC to the treasure chest based on distance and time limit
	/// </summary>
	public void CalculateCurrentSpeed (Vector3 transformIn) {
		//Calculates float value of distance between passed transform and own transform
		//Debug.Log("Position received: " + transformIn);
		float distance = Vector3.Distance(transformIn,transform.position);
		//Debug.Log("Distance calculated: " + distance);
		
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
	
	public bool getDirectCameraFollow () {
		return directCameraFollow;
	}
	#endregion
	
	#region Object Reference functions
	public void SetLastBCVisited(string nameIn) {
			lastBCVisited = nameIn;
	}
	
	public string GetLastBCVisited () {
		return lastBCVisited;
	}
	
	public void SetNextCornerPosition (Vector3 destIn) {
		//Debug.Log("SavedDest called!");
		nextCornerPosition = destIn;
	}
	
	public Vector3 GetNextCornerPosition () {
		return nextCornerPosition;
	}
	public void SetNextChestPosition (Vector3 destIn) {
		//Debug.Log("SavedDest called!");
		nextChestPosition = destIn;
	}
	
	public Vector3 GetNextChestPosition () {
		return nextChestPosition;
	}
	#endregion
	void Awake () {
		//Establish a reference to the game timer
		intermediateObject = GameObject.Find(intermediateObjectName);
		characterModel = GameObject.Find(characterModelName);
	}
	
	// Use this for initialization
	void Start () {
		//Establish a reference to the game timer
		//intermediateObject = GameObject.Find(intermediateObjectName);
		//startMoving();
	}
	
	// Update is called once per frame
	void Update () {
		AutoMove();
		//Debug.Log("Current Heading:" + transform.forward);
	}
}
