using UnityEngine;
using System.Collections;

public class CornerColliderScript : MonoBehaviour {
	

	public GameObject pointerA;
	public GameObject pointerB;
	public GameObject pointerC;
	
	//Private array to store non-null pointers to adjacent box colliders
	private GameObject[] pointers = new GameObject[3];
	private int numPointers = -1;
	//Private random number generator for determining next path
	private System.Random rand = new System.Random();
	//Private array and index counter for selecting next path
	private Vector3[] nextPosition = new Vector3[3];
	private int numPositions = -1;
	private int randomSelect;
	
	// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			Debug.Log("Entering " + gameObject.name);
			string lastVisited = col.GetComponent<PCAutoMove>().lastBCVisited;
			Debug.Log("Last Visited:" + lastVisited);
			
			numPositions = -1;
			//Loop through valid pointers to adjacent box colliders
			foreach (GameObject pointer in pointers) {
				//To avoid backtracking, only find positions of BCs the player hasn't just visited
				if (pointer.name != lastVisited) {
					Vector3 newPosition = pointer.transform.position;
					//add positions to nextPosition
					nextPosition[++numPositions] = newPosition;
				}
			}
			//use standard C# random random object to select next node, from 0 [inclusive] to number of known positions + 1 [exclusive]
			randomSelect = rand.Next(0,numPositions+1);
			Debug.Log("option chosen: " + randomSelect);
		}
	}
	
	//Called when a trigger zone re-checks and finds the same gameObject
	void OnTriggerStay (Collider col) {
		float distanceFromCenter = Vector3.Distance(col.transform.position, gameObject.transform.position);
		//Debug.Log("Distance from center: " + distanceFromCenter);
		//As Player Character approaches center of the trigger zone, redirect it to the newly-selected point
		if (distanceFromCenter < 1.0f) {
			col.GetComponent<Transform>().LookAt(nextPosition[randomSelect]);
		}
	}
	
	//Called when trigger zone no longer detects a gameObject with a RigidBody 
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			Debug.Log("Exiting " + gameObject.name);
			col.SendMessage("setLastBCVisited",gameObject.name);
		}
		
	}
	// Use this for initialization
	void Start () {
		if (pointerA != null) {
			//Debug.Log("Left Box found.");
			//Vector3 centerA = pointerA.transform.position;
			//nextPosition[++numPointers] = centerA;
			pointers[++numPointers] = pointerA;
		}
		if (pointerB != null) {
			//Debug.Log("Right Box found.");
			//Vector3 centerB = pointerB.transform.position;
			//nextPosition[++numPointers] = centerB;
			pointers[++numPointers] = pointerB;
		}
		if (pointerC != null) {
			//Debug.Log("Right Box found.");
			//Vector3 centerC = pointerC.transform.position;
			//nextPosition[++numPointers] = centerC;
			pointers[++numPointers] = pointerC;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, pointerA.transform.position, Color.green);
		Debug.DrawLine(transform.position, pointerB.transform.position, Color.red);
		Debug.DrawLine(transform.position, pointerC.transform.position, Color.cyan);
	}
}
