using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	
	#region GameObject Names
	public static string PlayerCharacterName = "Third Person PC";
	#endregion
	
	public float rotationAmount = 2.0f;
	MeshRenderer myRenderer;
	private GameObject playerCharacter;
	
	
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
		myRenderer = GetComponent<MeshRenderer>();
		playerCharacter = GameObject.Find(PlayerCharacterName);
		//transform.Rotate(0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rotationAmount,0);
	}
}
