using UnityEngine;
using System.Collections;

/// <summary>
/// Script controlling each CornerCollider, used to direct the player character around the map
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class CornerColliderScript : MonoBehaviour {
	#region gameObject reference names
	/// <summary>
	/// Public string storing the name of the main camera. Used for GameObject.Find() calls
	/// </summary>
	public static string mainCameraObjectName = "Main Camera";
	/// <summary>
	/// Public string storing the name of the player character. Used for GameObject.Find() calls
	/// </summary>
	public static string playerCharacterObjectName = "Third Person PC";
	/// <summary>
	/// Public string storing the name of the intermediate object. Used for GameObject.Find() calls
	/// </summary>/
	public string intermediateObjectName = "Intermediate";
	#endregion
	
	#region GameObject References
	/// <summary>
	/// Public references to other CornerColliders
	/// </summary>
	public GameObject pointerA;
	public GameObject pointerB;
	public GameObject pointerC;
	/// <summary>
	/// Public reference to treasure chest prefab.
	/// </summary>
	public GameObject treasureChestPrefab;
	#endregion
	
	#region Public Variables
	/// <summary>
	/// Public float used in placing treasure chest between two CornerColliders
	/// 0.0 = at CurrentCollider, 1.0 = at next CornerCollider
	/// </summary>
	public float chestOffset = 0.8f;
	#endregion
	
	#region Private Variables
	/// <summary>
	/// Private reference to an instance of TreasureChestPrefab
	/// </summary>
	private GameObject currentTreasureChest;

	/// <summary>
	/// Private array to store non-null pointers to adjacent box colliders
	/// </summary>
	private GameObject[] pointers = new GameObject[3];
	
	/// <summary>
	/// Private int tracking the number of other CornerColliders to which the current gameObject has references
	/// </summary>
	private int numPointers = -1;

	/// <summary>
	/// Private random number generator for determining next path, excluding CornerColliders the player has just visited
	/// </summary>
	private System.Random rand = new System.Random();

	/// <summary>
	/// Private array and index counter for selecting next path
	/// </summary>
	private Vector3[] nextPosition = new Vector3[3];
	
	/// <summary>
	/// Private int used to redirect the player character toward a CornerCollider other than the one from which they just came
	/// </summary>
	private int numPositions = -1;
	
	/// <summary>
	/// Private int used to store random output from rand
	/// </summary>
	private int randomSelect;
	#endregion
	
	#region Treasure Chest Functions
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
	#endregion
	
	#region Collision Functions
	/// <summary>
	/// Raises the trigger enter event.
	/// Called when a trigger zone first detects a gameObject with a RigidBody within its bounds
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			numPositions = -1;
			//Get the name of the last visited CC from the player character
			string lastVisited = col.GetComponent<BDAutoMove>().GetLastBCVisited();
		
			//If the player missed the last presented treasure chest, remove it from the environment
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
			//Debug.Log("option chosen: " + randomSelect);
		}
	}
	
	/// <summary>
	/// Raises the trigger stay event.
	/// Called when a trigger zone re-checks and finds the same gameObject
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
			float distanceFromCenter = Vector3.Distance(col.transform.position, gameObject.transform.position);
			//As Player Character approaches center of the trigger zone, redirect it to the newly-selected point
			if (distanceFromCenter < 1.0f) {
				col.transform.LookAt(nextPosition[randomSelect]);
				//Saved the position of the next CornerCollider in the player character object
				col.GetComponent<BDAutoMove>().SetNextCornerPosition(nextPosition[randomSelect]);
				//Debug.Log("Sending next position: " + nextPosition[randomSelect]);
			}
			//Debug checks to make sure that the collider knows where to send the player
			/*Debug.DrawLine(transform.position, pointerA.transform.position, Color.green);
			Debug.DrawLine(transform.position, pointerB.transform.position, Color.red);
			Debug.DrawLine(transform.position, pointerC.transform.position, Color.cyan);
			*/
		}
	}
	
	/// <summary>
	/// Raises the trigger exit event.
	/// Called when trigger zone no longer detects a gameObject with a RigidBody
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Debug.Log("Exiting " + gameObject.name);
			//Store the name of the current CC in the player character object
			col.GetComponent<BDAutoMove>().SetLastBCVisited(gameObject.name);
			//Call to BDGameScript to trigger the next required pose
			col.GetComponent<BDGameScript>().TriggerNextPose();
			//Debug.Log("Sending next position: " + nextPosition[randomSelect]);
			
			//Calculates where to place the next treasure chest between the current and next CornerColliders
			Vector3 origin = gameObject.transform.position;
			Vector3 dest = nextPosition[randomSelect];
			Vector3 offsetPosition = Vector3.Lerp(origin, dest, chestOffset);
			//Adjusts the player character's movement speed to that they will reach the chest within the set time limit
			col.GetComponent<BDAutoMove>().CalculateCurrentSpeed(offsetPosition);
			//Instatntiates a new treasure chest at the calculated position
			GenerateTreasureChest(offsetPosition, col.transform.rotation);
			col.GetComponent<BDAutoMove>().SetNextChestPosition(offsetPosition);
		}
		
	}
	#endregion
	
	/// <summary>
	/// Used for initialization
	/// </summary>
	void Awake () {
		//Checks all references to other CornerColliders
		if (pointerA != null) {
			pointers[++numPointers] = pointerA;
		}
		if (pointerB != null) {
			pointers[++numPointers] = pointerB;
		}
		if (pointerC != null) {
			pointers[++numPointers] = pointerC;
		}
		
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	}
}
