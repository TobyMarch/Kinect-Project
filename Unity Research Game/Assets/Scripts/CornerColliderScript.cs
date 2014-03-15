using UnityEngine;
using System.Collections;

public class CornerColliderScript : MonoBehaviour {
	
	public GameObject pointerA;
	public GameObject pointerB;
	public GameObject pointerC;
	
	//Private random number generator for determining next path
	private System.Random rand = new System.Random();
	//Private array and index counter for selecting next path
	private Vector3[] nextPosition = new Vector3[3];
	private int numPositions = -1;
	private int randomSelect;
	// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	void OnTriggerEnter (Collider col) {
		Debug.Log("Entering " + gameObject.name);
		randomSelect = rand.Next(0,numPositions);
	}
	
	//Called when a trigger zone re-checks and finds the same gameObject
	void OnTriggerStay (Collider col) {
		//Debug.Log("Stay");
		float distanceFromCenter = Vector3.Distance(col.transform.position, gameObject.transform.position);
		Debug.Log("Distance from center: " + distanceFromCenter);
		//As Player Character approaches center of the trigger zone, redirect it to the newly-selected point
		if (distanceFromCenter < 1.0f) {
			col.GetComponent<Transform>().LookAt(nextPosition[randomSelect]);
		}
		//col.GetComponent<Transform>().LookAt(leftPointer);
	}
	
	//Called when trigger zone no longer detects a gameObject with a RigidBody 
	void OnTriggerExit (Collider col) {
		Debug.Log("Exiting " + gameObject.name);
		
	}
	// Use this for initialization
	void Start () {
		if (pointerA != null) {
			//Debug.Log("Left Box found.");
			Vector3 centerA = pointerA.transform.position;
			nextPosition[++numPositions] = centerA;
			//Debug.Log("Positions found: " + numPositions);
			//numPositions++;
		}
		if (pointerB != null) {
			//Debug.Log("Right Box found.");
			Vector3 centerB = pointerB.transform.position;
			nextPosition[++numPositions] = centerB;
			//numPositions++;
			//Debug.Log("Positions found: " + numPositions);
		}
		if (pointerC != null) {
			//Debug.Log("Right Box found.");
			Vector3 centerC = pointerC.transform.position;
			nextPosition[++numPositions] = centerC;
			//numPositions++;
			//Debug.Log("Positions found: " + numPositions);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, pointerA.transform.position, Color.green);
		Debug.DrawLine(transform.position, pointerB.transform.position, Color.red);
	}
}
