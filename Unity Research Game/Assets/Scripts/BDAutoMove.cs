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
	
	void setLastBCVisited(string nameIn) {
			lastBCVisited = nameIn;
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
