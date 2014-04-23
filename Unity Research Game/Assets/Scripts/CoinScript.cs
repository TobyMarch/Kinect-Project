using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	
	public float rotationAmount = 2.0f;
	private GameObject playerCharacter;
	
	
	void vanish () {
		Destroy(gameObject);
		playerCharacter.GetComponent<BDGameScript>().coinCollected();
	}
	
	void OnTriggerEnter (Collider col) {
		vanish();	
	}
	
	// Use this for initialization
	void Start () {
		playerCharacter = GameObject.Find(PlayerCharacterName);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rotationAmount,0);
	}
}
