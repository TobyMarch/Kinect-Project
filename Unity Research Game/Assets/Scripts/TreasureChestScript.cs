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
		playerCharacter.GetComponent<BDGameScript>().coinCollected();
	}
	
	void OnTriggerEnter (Collider col) {
		vanish();	
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
