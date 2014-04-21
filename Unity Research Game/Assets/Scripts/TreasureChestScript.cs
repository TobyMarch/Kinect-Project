using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	#region GameObject References
	private GameObject playerCharacter;
	private MeshRenderer myRenderer;
	#endregion
	
	void vanish () {
		//GetComponent<MeshRenderer>().enabled = false;
		myRenderer.enabled = false;
		Destroy(gameObject);
		playerCharacter.GetComponent<BDGameScript>().chestCollected();
	}
	
	void OnTriggerEnter (Collider col) {
		vanish();	
	}
	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<MeshRenderer>();
		playerCharacter = GameObject.Find(PlayerCharacterName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
