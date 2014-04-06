using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	
	public float rotationAmount = 2.0f;
	MeshRenderer myRenderer;
	
	
	void vanish () {
		//GetComponent<MeshRenderer>().enabled = false;
		myRenderer.enabled = false;
		Destroy(gameObject);
	}
	
	void OnTriggerEnter (Collider col) {
		vanish();	
	}
	
	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,rotationAmount,0);
	}
}
