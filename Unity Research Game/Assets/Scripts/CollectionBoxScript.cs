using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionBoxScript : MonoBehaviour {
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
	
	float parentXPos;
	float parentYPos;
	float parentZPos;
	
	Vector3 firstCoin;
	Vector3 lastCoin;
	bool inLine = false;
	
	List<GameObject> coinList = new List<GameObject>();
	
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
			GameObject newCoin = Instantiate(coinPrefab, new Vector3(parentXPos,parentYPos,parentZPos), Quaternion.identity) as GameObject;
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
		//gameObject.transform.childCount;
		foreach (GameObject coin in coinList) {
			Destroy(coin);
		}
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			Vector3 colPosition = col.transform.position;
			float distanceFromFirst = Vector3.Distance(colPosition, firstCoin);
			float distanceFromlast = Vector3.Distance(colPosition, lastCoin);
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
	/// </summary>
	void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
			Vector3 colPosition = col.transform.position;
			float distanceFromFirst = Vector3.Distance(colPosition, firstCoin);
			float distanceFromLast = Vector3.Distance(colPosition, lastCoin);
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
	/// </summary>
	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			//Debug.Log("Exiting Collection Box");
			inLine = false;
			destroyCoins();
			generateCoins();
			Vector3 nextDestination = col.GetComponent<BDAutoMove>().getSavedDest();
			col.transform.LookAt(nextDestination);
		}
	}
	
	// Use this for initialization
	void Start () {
		//coin = Resources.Load("coinPrefab") as GameObject;
		//Debug.Log("CCZ Position: "+transform.position.x+"," + transform.position.y + ", " + transform.position.z);
		parentXPos = transform.position.x;
		parentYPos = transform.position.y;
		parentZPos = transform.position.z;
		generateCoins();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
