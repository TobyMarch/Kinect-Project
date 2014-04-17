using UnityEngine;
using System.Collections;

public class BDAutoMove : MonoBehaviour {
	#region public variables
	//[SerializeField]
	public float moveSpeed = 10.0f;
	public string lastBCVisited;
	#endregion
	
	#region private variables
	private float currentSpeed = 0.0f;
	private Vector3 savedDestination;
	private bool directCameraFollow = true;
	#endregion
	
	/// <summary>
	///  generic movement function, called every frame
	/// </summary>
	void autoMove () {
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
	}
	
	/// <summary>
	/// Used by other scripts to start the player character moving
	/// </summary>
	public void startMoving () {
		Debug.Log("startMoving Called!");
		currentSpeed = moveSpeed;
	}
	
	/// <summary>
	/// Stops the player character
	/// </summary>
	public void stopMoving() {
		Debug.Log("StopMoving Called!");
		currentSpeed = 0.0f;
	}
	
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
	
	public void setLastBCVisited(string nameIn) {
			lastBCVisited = nameIn;
	}
	
	public void setSavedDest (Vector3 destIn) {
		//Debug.Log("SavedDest called!");
		savedDestination = destIn;
	}
	
	public Vector3 getSavedDest () {
		return savedDestination;
	}
	
	// Use this for initialization
	void Start () {
		//startMoving();
	}
	
	// Update is called once per frame
	void Update () {
		autoMove();
		//Debug.Log("Current Heading:" + transform.forward);
	}
}
