using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script controlling each CollectionBox, generating coins for the player character to pick up
/// Author: Tobias March
/// Date: March-May 2014
/// </summary>
public class CollectionBoxScript : MonoBehaviour {
	#region Public Variables
	/// <summary>
	/// The coin prefab.
	/// </summary>
	public GameObject coinPrefab;
	
	/// <summary>
	/// Lower bound (inclusive) for the number of coins generated
	/// </summary>
	public int numCoinsLBound = 3;
	
	/// <summary>
	/// Upper bound (inclusive) for the number of coins generated
	/// </summary>
	public int numCoinsHBound = 6;
	
	/// <summary>
	/// Distance from CB center by which coins may be offset
	/// </summary>
	public float coinXOffset = 10.0f;
	
	/// <summary>
	/// Distance between coins. DO NOT CHANGE FROM 2.0f
	/// </summary>
	public float coinZSpacing = 2.0f;
	#endregion
	#region Private Variables
	/// <summary>
	/// Private floats used to store the transform coordinates of the parent
	/// </summary>
	private float parentXPos;
	private float parentYPos;
	private float parentZPos;
	
	/// <summary>
	/// Private Vector3 storing the world transform of the first coin generated
	/// </summary>
	private Vector3 firstCoin;
	/// <summary>
	/// Private Vector3 storing the world transform of the last coin generated
	/// </summary>
	private Vector3 lastCoin;
	
	/// <summary>
	/// Private bool determining whether the player character is in line with the row of generated coins
	/// </summary>
	private bool inLine = false;
	
	/// <summary>
	/// Private list storing references to all coins generated
	/// </summary>
	private List<GameObject> coinList = new List<GameObject>();
	#endregion
	
	/// <summary>
	/// Populates the trigger box with coins. The number of coins may vary
	/// </summary>
	void generateCoins () {
		//Generates a random number of coins to create
		int numCoins = Random.Range(numCoinsLBound, numCoinsHBound + 1);
		//Generates position offset so that entire set of coins will be centered within box collider
		float instStartPoint = ((numCoins) + (numCoins-1 * coinZSpacing)) / 2.0f;
		float newOffset = Random.Range(-1,2) * coinXOffset;
		for (int i = 0; i < numCoins; i++) {
			//Instantiate new coins
			GameObject newCoin = Instantiate(coinPrefab, new Vector3(parentXPos,parentYPos,parentZPos), Quaternion.Euler(0,0,0)) as GameObject;
			//Set coins as children on the box collider, in line with the box's z-axis
			newCoin.transform.parent = transform;
			//Each coin is positioned relative to the axes of the collision box
			newCoin.transform.position -= transform.forward * instStartPoint;
			newCoin.transform.position += transform.forward * (i * coinZSpacing);
			newCoin.transform.position += transform.right * newOffset;
			coinList.Add(newCoin);
			
			//Save the positions of the first and last coins
			if (i == 0) {
				firstCoin = newCoin.transform.position;
			}
			else if (i == (numCoins-1)) {
				lastCoin = newCoin.transform.position;
			}
		}
	}
	
	/// <summary>
	/// Deletes all remaining coins within collection box
	/// </summary>
	void destroyCoins () {
		foreach (GameObject coin in coinList) {
			Destroy(coin);
		}
	}
	
	/// <summary>
	/// Raises the trigger stay event.
	/// Called when a trigger zone re-checks and finds the same gameObject
	/// </summary>
	/// <param name='col'>
	/// Colliding object. Usually the player character
	/// </param>
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			// Sets the main camera to not point directly at the player character
			col.GetComponent<BDAutoMove>().setDirectCameraFollow(false);
			Vector3 colPosition = col.transform.position;
			//Given that the player character may be approaching the box from either end, determine which coin is closest
			float distanceFromFirst = Vector3.Distance(colPosition, firstCoin);
			float distanceFromlast = Vector3.Distance(colPosition, lastCoin);
			//Direct the player character towards the closer of the two coins
			if (distanceFromFirst < distanceFromlast) {
				col.transform.LookAt(firstCoin);
			}
			else {
				col.transform.LookAt(lastCoin);
			}
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
			Vector3 colPosition = col.transform.position;
			//Calculate distances to both ends of the line of coins
			float distanceFromFirst = Vector3.Distance(colPosition, firstCoin);
			float distanceFromLast = Vector3.Distance(colPosition, lastCoin);
			//Once the player character hits one coin, direct it at the other one (hitting the rest in the process)
			if (distanceFromFirst < 0.5f && !inLine) {
				inLine = true;
				col.transform.LookAt(lastCoin);
			}
			else if (distanceFromLast < 0.5f && !inLine) {
				inLine = true;
				col.transform.LookAt(firstCoin);
			}
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
			//Debug.Log("Exiting Collection Box");
			inLine = false;
			//Remove any coins that the player character might have missed, and spawn new ones
			destroyCoins();
			generateCoins();
			//Point the player character at the next treasure chest
			Vector3 nextDestination = col.GetComponent<BDAutoMove>().GetNextChestPosition();
			col.transform.LookAt(nextDestination);
			col.GetComponent<BDAutoMove>().setDirectCameraFollow(true);
		}
	}
	
	/// <summary>
	/// Used for Initialization
	/// </summary>
	void Awake () {
		//Store the transform coordinates for use in coin generation
		parentXPos = transform.position.x;
		parentYPos = transform.position.y;
		parentZPos = transform.position.z;
		//fill box with coins
		generateCoins();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
