using UnityEngine;
using System.Collections;

public class CollectionBoxScript : MonoBehaviour {
	
	public GameObject coinPrefab;
	public int numCoinsLBound = 3;
	public int numCoinsHBound = 6;
	public float coinOffset = 10.0f;
	public float coinSpacing = 1.0f;
	
	float parentXPos;
	float parentYPos;
	float parentZPos;
	
	GameObject coin;
	
	float orignalXPos;
	/// <summary>
	/// Generates a single coin
	/// </summary>
	void instCoin () {
		Instantiate(coinPrefab, new Vector3(parentXPos,parentYPos,parentZPos + coinOffset),Quaternion.identity);
	}
	
	/// <summary>
	/// Populates the trigger box with coins. The number of coins may vary
	/// </summary>
	void generateCoins () {
		//Generates a random number of coins to create
		int numCoins = Random.Range(numCoinsLBound, numCoinsHBound + 1);
		//Generates position offset so that entire set of coins will be centered within box collider
		float instStartPoint = ((numCoins) + (numCoins-1 * coinSpacing)) / 2.0f;
		for (int i = 0; i < numCoins; i++) {
			//Instantiate new coins
			GameObject newCoin = Instantiate(coinPrefab, new Vector3(parentXPos,parentYPos,parentZPos), Quaternion.identity) as GameObject;
			//Set coins as children on the box collider, in line with the box's z-axis
			newCoin.transform.parent = transform;
			//
			newCoin.transform.position -= transform.forward * instStartPoint;
			newCoin.transform.position += transform.forward * (i * coinSpacing);
		}
		//InvokeRepeating("instCoin",1.0f,0.5f);
	}
	
	void repositionBox() {
		
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	void OnTriggerEnter () {
		
	}
	
	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	void OnTriggerExit () {
		Debug.Log("Exiting Collection Box");
		generateCoins();
	}
	
	// Use this for initialization
	void Start () {
		//coin = Resources.Load("coinPrefab") as GameObject;
		//Debug.Log("CCZ Position: "+transform.position.x+"," + transform.position.y + ", " + transform.position.z);
		parentXPos = transform.position.x;
		parentYPos = transform.position.y;
		parentZPos = transform.position.z;
		//originalXPos = transform.position.x;
		generateCoins();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
