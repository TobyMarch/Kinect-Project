using UnityEngine;
using System.Collections;

public class TreasureChestScript : MonoBehaviour {
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	private GameObject playerCharacter;
	#endregion
	
	void vanish () {
		playerCharacter.GetComponent<BDGameScript>().chestCollected();
		Destroy(gameObject);
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
	
	}
}
