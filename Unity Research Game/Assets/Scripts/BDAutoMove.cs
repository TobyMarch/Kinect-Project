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
