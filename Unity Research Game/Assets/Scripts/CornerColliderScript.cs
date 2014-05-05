using UnityEngine;
using System.Collections;

public class CornerColliderScript : MonoBehaviour {
	#region gameObject reference names
	public static string mainCameraObjectName = "Main Camera";
	public static string playerCharacterObjectName = "Third Person PC";
	public string intermediateObjectName = "Intermediate";
	#endregion
	
	#region GameObject References
	public GameObject pointerA;
	public GameObject pointerB;
	public GameObject pointerC;
	public GameObject treasureChestPrefab;
	#endregion
	
	#region Public Variables
	public float chestOffset = 0.8f;
	#endregion
	
	//Private reference to an instance of TreasureChestPrefab
	private GameObject currentTreasureChest;
	//Private array to store non-null pointers to adjacent box colliders
	private GameObject[] pointers = new GameObject[3];
	private int numPointers = -1;
	//Private random number generator for determining next path
	private System.Random rand = new System.Random();
	//Private array and index counter for selecting next path
	private Vector3[] nextPosition = new Vector3[3];
	private int numPositions = -1;
	private int randomSelect;
	
	/// <summary>
	/// Generates a treasure chest in the path of the player, as a potential reward for mastering the next pose
	/// </summary>
	void GenerateTreasureChest (Vector3 positionIn, Quaternion rotationIn) {
		currentTreasureChest = Instantiate(treasureChestPrefab, positionIn, rotationIn) as GameObject;
	}
	
	/// <summary>
	/// Destroys the treasure chest once the user is past it
	/// </summary>
	public void DestroyTreasureChest () {
		Destroy(currentTreasureChest);
	}
	
	// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			numPositions = -1;
			//Debug.Log("Entering " + gameObject.name);
			//Debug.Log("HEY LOOK it's " + col.name);
			string lastVisited = col.GetComponent<BDAutoMove>().GetLastBCVisited();
			//Debug.Log("Last Visited:" + lastVisited);
		
			if (lastVisited != null) {
				GameObject.Find(lastVisited).GetComponent<CornerColliderScript>().DestroyTreasureChest();
			}
			
			
			//Loop through valid pointers to adjacent box colliders
			foreach (GameObject pointer in pointers) {
				//To avoid backtracking, only find positions of BCs the player hasn't just visited
				if (pointer.name != lastVisited) {
					Vector3 newPosition = pointer.transform.position;
					//add positions to nextPosition
					nextPosition[++numPositions] = newPosition;
				}
			}
			//use standard C# random object to select next node, from 0 [inclusive] to number of known positions + 1 [exclusive]
			randomSelect = rand.Next(0,numPositions+1);
			Debug.Log("option chosen: " + randomSelect);
		}
	}
	
	//Called when a trigger zone re-checks and finds the same gameObject
	void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
			float distanceFromCenter = Vector3.Distance(col.transform.position, gameObject.transform.position);
			//Debug.Log("Distance from center: " + distanceFromCenter);
			//As Player Character approaches center of the trigger zone, redirect it to the newly-selected point
			if (distanceFromCenter < 1.0f) {
				//col.GetComponent<Transform>().LookAt(nextPosition[randomSelect]);
				col.transform.LookAt(nextPosition[randomSelect]);
				col.GetComponent<BDAutoMove>().SetNextCornerPosition(nextPosition[randomSelect]);
				//Debug.Log("Sending next position: " + nextPosition[randomSelect]);
				//col.GetComponent<BDAutoMove>().calculateCurrentSpeed(nextPosition[randomSelect]);
			}
			//Debug checks to make sure that the collider knows where to send the player
			Debug.DrawLine(transform.position, pointerA.transform.position, Color.green);
			Debug.DrawLine(transform.position, pointerB.transform.position, Color.red);
			Debug.DrawLine(transform.position, pointerC.transform.position, Color.cyan);
		}
	}
	
	//Called when trigger zone no longer detects a gameObject with a RigidBody 
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Debug.Log("Exiting " + gameObject.name);
			//col.SendMessage("setLastBCVisited",gameObject.name);
			col.GetComponent<BDAutoMove>().SetLastBCVisited(gameObject.name);
			//Call directly to the translation layer (will be replaced)
			//GameObject.Find(intermediateObjectName).GetComponent<TranslationLayer>().ListenForNextWholeBodyGesture();
			//Call to BDGameScript
			col.GetComponent<BDGameScript>().TriggerNextPose();
			//Debug.Log("Sending next position: " + nextPosition[randomSelect]);
			Vector3 origin = gameObject.transform.position;
			Vector3 dest = nextPosition[randomSelect];
			//float chestFloat = 0.8f;
			Vector3 offsetPosition = Vector3.Lerp(origin, dest, chestOffset);
			col.GetComponent<BDAutoMove>().CalculateCurrentSpeed(offsetPosition);
			//GameObject newChest = Instantiate(treasureChestPrefab, offsetPosition, col.transform.rotation) as GameObject;
			GenerateTreasureChest(offsetPosition, col.transform.rotation);
			col.GetComponent<BDAutoMove>().SetNextChestPosition(offsetPosition);
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
			//Debug.Log("Center Box found.");
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
		
	}
}
